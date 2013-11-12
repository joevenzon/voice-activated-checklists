using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Media;
using SimVoiceChecklists.Properties;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SimVoiceChecklists
{
    public partial class frmChecklist : Form
    {
        private System.Speech.Synthesis.SpeechSynthesizer synth = new System.Speech.Synthesis.SpeechSynthesizer();
        private string CheckListFilename;
        private string AudioPath;
        private int ActiveChecklistIndex = -1;
        private int ChecklistIndex = -1;
        private IWavePlayer waveOut;
        private WaveStream fileWaveStream;

        public List<string> AcceptedChecklistCmds;

        public frmChecklist()
        {
            InitializeComponent();
            Top = 45;
            Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 13;
            this.ClientSize = new System.Drawing.Size(310, 24);
            this.MinimumSize = this.ClientSize;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        public bool LoadChecklistFile(string CheckListFilename)
        {
            bool result = false;
            int clID = 1;
            if (System.IO.File.Exists(CheckListFilename))
            {
                foreach (XElement checklist in XElement.Load(CheckListFilename).Elements("checklist"))
                {
                    this.CheckListFilename = CheckListFilename;
                    lvAvailableChecklists.Items.Add(new ListViewItem(new[] {clID.ToString(),
                                                                        checklist.Attribute("name").Value}));
                    clID += 1;
                }
                result = true;
            }
            return result;
        }

        public void SetAudioPath(string AudioPath)
        {
            this.AudioPath = AudioPath;
        }

        public void ProgressChecklist()
        {
            if (ChecklistIndex > -1)
                AcceptChecklistCommand();
        }

        public bool ProcessPossibleChecklistCommand(string VoiceCommand)
        {
            bool result = false;
            string checklistCmd;
            string voiceCmd = VoiceCommand.ToLower();
            bool acceptedChecklistCmd = AcceptedChecklistCmds.Any(s => s.Equals(voiceCmd.ToLower(), StringComparison.OrdinalIgnoreCase));

            if ((ChecklistIndex > -1) && (acceptedChecklistCmd))
            {
                if (voiceCmd.Equals("repeat"))
                {
                    ReadActiveChecklistItem();
                    result = true;
                }
                else
                {
                    string commandText = lvChecklistItems.Items[ChecklistIndex].SubItems[2].Text.ToLower();
                    string[] validCommands = commandText.Split(',').Select(sValue => sValue.Trim()).ToArray();

                    if (validCommands.Contains(voiceCmd))
                    {
                        AcceptChecklistCommand();
                        result = true;
                    }
                }
            }
            else if (VoiceCommand.ToLower().Contains("checklist"))
            {
                checklistCmd = VoiceCommand.ToLower().Replace("checklist", "").Trim();

                if (checklistCmd == "abort")
                {
                    CloseActiveChecklist();
                    ShowNoActiveChecklist();
                    result = true;
                }
                else if (checklistCmd == "close")
                {
                    Close();
                    result = true;
                }
                else
                {
                    foreach (ListViewItem item in lvAvailableChecklists.Items)
                    {
                        if (item.SubItems[1].Text.ToLower().Equals(checklistCmd))
                        {
                            ActivateChecklist(item.Index);
                            result = true;
                            break;
                        }
                    }
                }
            }
            else if ((VoiceCommand.ToLower().Contains("thanks")) || (VoiceCommand.ToLower().Contains("thankyou")))
            {
                Say("You're welcome!");
                result = true;
            }
            
            return result;
        }

        public void ActivateChecklist(int ChecklistNumber)
        {
            if (ChecklistNumber <= lvAvailableChecklists.Items.Count)
                if (LoadChecklist(ChecklistNumber))
                {
                    ActiveChecklistIndex = ChecklistNumber;
                    ShowActiveChecklist();
                    StartLoadedChecklist();
                }
        }

        public void AcceptChecklistCommand()
        {
            lvChecklistItems.Items[ChecklistIndex].Checked = true;
            ChecklistIndex += 1;
            if (ChecklistIndex < lvChecklistItems.Items.Count)
                ReadActiveChecklistItem();
            else
                DoChecklistComplete();
        }

        public void CloseActiveChecklist()
        {
            ChecklistIndex = -1;
            lvChecklistItems.Items.Clear();
        }

        public void ShowNoActiveChecklist()
        {
            ActiveChecklistIndex = -1;
            lblChecklistHeader.Text = "No checklist active";
        }

        public void ShowActiveChecklist()
        {
            if (ActiveChecklistIndex >= lvAvailableChecklists.Items.Count)
                ActiveChecklistIndex = 0;

            lblChecklistHeader.Text = "Active: " + lvAvailableChecklists.Items[ActiveChecklistIndex].SubItems[1].Text + " Checklist";
        }

        private void DoChecklistComplete()
        {
            Say("Checklist complete");
            CloseActiveChecklist();
            ActiveChecklistIndex += 1;
            ShowActiveChecklist();
        }

        private IWavePlayer CreateDevice(int latency)
        {
            IWavePlayer device;

            var waveOut = new WaveOutEvent();
            waveOut.DeviceNumber = Settings.Default.AudioDeviceID;
            waveOut.DesiredLatency = latency;
            device = waveOut;

            return device;
        }

        private void CreateWaveOut()
        {
            CloseWaveOut();
            this.waveOut = CreateDevice(200);
        }

        private void CloseWaveOut()
        {
            if (waveOut != null)
                waveOut.Stop();

            if (fileWaveStream != null)
                fileWaveStream.Dispose();

            if (waveOut != null)
            {
                waveOut.Dispose();
                waveOut = null;
            }
        }

        private void PlayWavFile(string WAVFilename)
        {
            if (waveOut != null)
                if (waveOut.PlaybackState == PlaybackState.Playing)
                    waveOut.Stop();

            try
            {
                CreateWaveOut();
            }
            catch (Exception driverCreateException)
            {
                MessageBox.Show(String.Format("{0}", driverCreateException.Message));
                return;
            }

            fileWaveStream = new WaveChannel32(new WaveFileReader(WAVFilename));

            try
            {
                waveOut.Init(fileWaveStream);
            }
            catch (Exception initException)
            {
                MessageBox.Show(String.Format("{0}", initException.Message), "Error Initializing Output");
                return;
            }

            waveOut.Play();
        }

        private void Say(string Statement)
        {
            string wavFile = GetWavFileForText(Statement);
            if ((wavFile != String.Empty) && (System.IO.File.Exists(wavFile)))
            {
                PlayWavFile(wavFile);
//                using (SoundPlayer player = new SoundPlayer(wavFile))
//                    player.Play();
            }
            else
                synth.SpeakAsync(Statement);
        }

        private bool LoadChecklist(int ChecklistIndex)
        {
            bool result = false;
            if (ChecklistIndex < lvAvailableChecklists.Items.Count)
            {
                lvChecklistItems.Items.Clear();
                string checklistName = lvAvailableChecklists.Items[ChecklistIndex].SubItems[1].Text.ToLower();

                foreach (XElement checklist in XElement.Load(CheckListFilename).Elements("checklist"))
                {
                    if (checklist.Attribute("name").Value.ToLower() == checklistName)
                    {
                        foreach (XElement items in checklist.Elements("item"))
                        {
                            string name = "";
                            string instruction = "";
                            string vcommand = "";
                            string wavfile = "";
                            
                            if (items.Attribute("name") != null)
                                name = items.Attribute("name").Value;
                            if (items.Attribute("instruction") != null)
                                instruction = items.Attribute("instruction").Value;
                            if (items.Attribute("vcommand") != null)
                                vcommand = items.Attribute("vcommand").Value;
                            if (items.Attribute("wavfile") != null)
                                wavfile = items.Attribute("wavfile").Value;

                            lvChecklistItems.Items.Add(new ListViewItem(new[] { name, instruction, vcommand, wavfile }));
                        }
                        result = true;
                    }
                }
            }
            return result;
        }

        private void StartLoadedChecklist()
        {
            BeginActiveChecklist();
        }

        private void BeginActiveChecklist()
        {

            ChecklistIndex = 0;
            ReadActiveChecklistItem();
        }

        private void ReadActiveChecklistItem()
        {
            Say(lvChecklistItems.Items[ChecklistIndex].Text);
        }

        private string GetWavFileForText(string ChecklistText)
        {
            string textToCheck = ChecklistText;
            if (char.IsPunctuation(textToCheck[textToCheck.Length -1]))
                textToCheck = textToCheck.TrimEnd(textToCheck[textToCheck.Length - 1]);

            string wavFilename = AudioPath + "\\" + textToCheck + ".wav";

            if (System.IO.File.Exists(wavFilename))
                return wavFilename;
            else
                return "";
        }

        private void lblChecklistHeader_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
