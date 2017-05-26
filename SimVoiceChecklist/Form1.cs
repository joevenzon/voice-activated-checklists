#region using directives
using System;
using System.Speech.Recognition;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Xml.Linq;
using System.Configuration;
using SimVoiceChecklists.Properties;
using Gma.UserActivityMonitor;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
#endregion

namespace SimVoiceChecklists
{
    public partial class MainFrm : Form
    {
        #region Vars
        private bool AllowExit = false;
        private TextBox ActiveKeyBindTextBox = null;
        
        private SpeechRecognitionEngine recognizer = null;
        private Choices grammarChoices;
        private List<string> AcceptedChecklistCmds = new List<string>();

        private frmChecklist CLForm = null;
        private frmProcedure ProcForm = null;
        private StatusWindow StatusWin = new StatusWindow();
        private ChecklistList CLListForm;

        private string ActiveCLFilename;
        private string AudioPath;
        private decimal ConfidenceThreshold;
        private string Culture;
        private string ProgressCLKeyBind;
        private string ShowCLKeyBind;
        private string RepeatCLIKeyBind;
        private string ShowProcKeyBind;
        private bool DisableSpeechRecogEng;
        private int AudioDeviceID;
        private bool HideGUI;
        #endregion

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(isDisposing);
        }
        
        public MainFrm()
        {
            (new Splash()).ShowSplash(6000);
            InitializeComponent();
            try
            {
                HookManager.KeyUp += HookManager_KeyUp;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.Source);

                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
                    System.Environment.Exit(1);
                }
            }
            LoadOptionsSettings();
        }

        #region Speech Engine Procs
        private bool ProcessTopLevelVoiceCommand(string VoiceCommand)
        {
            bool result = false;

            OpenChecklists();

            if (VoiceCommand.ToLower().Contains("checklist"))
            {
                if (VoiceCommand.ToLower().Contains("open"))
                {
                    result = true;
                    OpenChecklists();
                }
                else if (VoiceCommand.ToLower().Contains("show"))
                {
                    CLListForm = new ChecklistList();
                    CLListForm.CLForm = CLForm;
                    CLListForm.LoadChecklistFile(ActiveCLFilename, "checklist");
                    result = true;
                }
            }
            else if (VoiceCommand.ToLower().Contains("procedure"))
            {
                if (VoiceCommand.ToLower().Contains("show"))
                {
                    CLListForm = new ChecklistList();
                    CLListForm.ProcForm = ProcForm;
                    CLListForm.LoadChecklistFile(ActiveCLFilename, "procedure");
                    result = true;
                }
                else
                {
                    string procedure = VoiceCommand.ToLower().Replace("procedure", "").Trim();
                    ProcForm.StartProcedure(procedure);
                }
            }

            if (!result && CLForm != null && (CLForm.Visible || HideGUI))
                result = CLForm.ProcessPossibleChecklistCommand(VoiceCommand);

            if (!result)
            {
                // it could be a procedure that doesn't have the word procedure in it (gear up, etc)
                string procedure = VoiceCommand.ToLower();
                if (procedure.Contains("gear") || procedure.Contains("flaps"))
                {
                    result = ProcForm.StartProcedure(procedure);
                }
            }

            return result;
        }

        void CLListForm_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadAcceptedChecklistCmds()
        {
            AcceptedChecklistCmds.Clear();

            foreach (XElement checklist in XElement.Load(ActiveCLFilename).Elements("checklist"))
            {
                foreach (XElement items in checklist.Elements("item"))
                {
                    string vcommand = "";

                    if (items.Attribute("vcommand") != null)
                        vcommand = items.Attribute("vcommand").Value;

                    if (!String.IsNullOrEmpty(vcommand))
                    {
                        List<string> validCommands = vcommand.Split(',').ToList<string>();
                        foreach (string validCmd in validCommands)
                        {
                            if (AcceptedChecklistCmds.IndexOf(validCmd.Trim().ToLower()) <= -1)
                                AcceptedChecklistCmds.Add(validCmd.Trim().ToLower());
                        }
                    }
                }
            }
        }

        private void HideAllOptionPanels()
        {
            foreach(Control Ctrl in pnlOptionDetails.Controls)
                if (Ctrl is Panel)
                {
                    ((Panel)Ctrl).Visible = false;
                    ((Panel)Ctrl).Dock = DockStyle.None;
                }
        }

        private void AddGrammar(string Grammar)
        {
            grammarChoices.Add(Grammar);
            lbxAcceptedCmds.Items.Add(Grammar);
        }

        private void InitSpeechEngine()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Culture);
            grammarChoices = new Choices();

            LoadAcceptedChecklistCmds();
            foreach (string checklistCmd in AcceptedChecklistCmds)
                AddGrammar(checklistCmd);

            AddGrammar("Open Checklist");
            AddGrammar("Close Checklist");
            AddGrammar("Abort Checklist");
            AddGrammar("Show Checklists");
            AddGrammar("Show Procedures");
            //AddGrammar("Thankyou");
            //AddGrammar("Thanks");
            AddGrammar("Repeat");

            foreach (XElement checklist in XElement.Load(ActiveCLFilename).Elements("checklist"))
                AddGrammar(checklist.Attribute("name").Value + " Checklist");

            foreach (XElement procedure in XElement.Load(ActiveCLFilename).Elements("procedure"))
            {
                string grammar = procedure.Attribute("name").Value;
                if (!grammar.ToLower().Contains("gear") && !grammar.ToLower().Contains("flaps"))
                    grammar += " Procedure";
                AddGrammar(grammar);
            }

            GrammarBuilder gbCurrent = new GrammarBuilder(grammarChoices);

            recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo(Culture));
            // Create and load a dictation grammar.
            //recognizer.LoadGrammar(new DictationGrammar());
            recognizer.LoadGrammar(new Grammar(gbCurrent));

            // Add a handler for the speech recognized event.
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            recognizer.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(sre_AudioLevelUpdated);

            // Configure input to the speech recognizer.
            recognizer.SetInputToDefaultAudioDevice();
        }

        private void StartListenening()
        {
            // Start asynchronous, continuous speech recognition.
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void StopListening()
        {
            // Start asynchronous, continuous speech recognition.
            recognizer.RecognizeAsyncStop();
        }
        #endregion

        #region Speech Engine Events
        // Create a simple handler for the SpeechRecognized event.
        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string voiceCommand = e.Result.Text;
            decimal confidence = Convert.ToDecimal(e.Result.Confidence * 100);

            ListViewItem lviNew = lvHeardDebug.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss"));
            lviNew.SubItems.Add(voiceCommand);
            lviNew.SubItems.Add(confidence.ToString());

            if (confidence > ConfidenceThreshold)
            {
                if (!HideGUI)
                    StatusWin.ShowText(voiceCommand, 2000);

                if (!ProcessTopLevelVoiceCommand(voiceCommand))
                {
                    foreach (RecognizedPhrase phrase in e.Result.Alternates)
                    {
                        voiceCommand = phrase.Text;
                        if (ProcessTopLevelVoiceCommand(voiceCommand))
                            break;
                    }
                }
            }
        }

        void sre_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            if (e.AudioLevel > progressBar1.Maximum)
                progressBar1.Maximum = e.AudioLevel;

            progressBar1.Value = e.AudioLevel;
        }
        #endregion

        #region Event Handlers
        private void btnListen_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnListen.Tag) == 0)
            {
                bool success = false;

                try
                {
                    if ((!String.IsNullOrEmpty(ActiveCLFilename)) || (System.IO.File.Exists(ActiveCLFilename)))
                    {
                        InitSpeechEngine();
                        StartListenening();
                        success = true;
                    }
                    else
                        success = false;
                }
                catch (Exception)
                {
                    success = false;
                }

                if (success)
                {
                    ListenMenuItem.Checked = true;
                    btnListen.Tag = 1;
                    btnListen.Text = "Stop Listening";
                    ShowChecklistsMenuItem.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Unable to open checklist file", "Error loading checklist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ListenMenuItem.Checked = false;
                btnListen.Tag = 0;
                btnListen.Text = "Start Listening";
                StopListening();
                ShowChecklistsMenuItem.Enabled = false;
            }
        }

        private void tvOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowOptionsPage(e.Node);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (SaveOptionSettings())
            {
                AllowExit = true;
                notifyIcon1.Icon = null;
                Application.Restart();
            }
            else
            {
                ReloadOptionsSettings();
            }

            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowExit)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void OptionsMenuItem_Click(object sender, EventArgs e)
        {
            UpdateConnectionsLabel();
            Show();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            (new About()).Show();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            UpdateConnectionsLabel();
            Show();
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            TreeNode tnSelected = tvOptions.Nodes.Find("nodeGeneral", true)[0];
            tvOptions.SelectedNode = tnSelected;
            ShowOptionsPage(tnSelected);
        }

        private void OnExit(object sender, EventArgs e)
        {
            AllowExit = true;
            notifyIcon1.Icon = null;
            Application.Exit();
        }

        private void btnChecklistFilename_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbChecklistFilename.Text))
            {
                ofdChecklistFile.InitialDirectory = System.IO.Path.GetDirectoryName(tbChecklistFilename.Text);
                ofdChecklistFile.FileName = System.IO.Path.GetFileName(tbChecklistFilename.Text);
            }

            if (ofdChecklistFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbChecklistFilename.Text = ofdChecklistFile.FileName;
        }

        private void btnAudioPath_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbAudioPath.Text))
            {
                fbdAudioPath.SelectedPath = System.IO.Path.GetDirectoryName(tbChecklistFilename.Text);
                //ofdChecklistFile.FileName = System.IO.Path.GetFileName(tbChecklistFilename.Text);
            }

            if (fbdAudioPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbAudioPath.Text = fbdAudioPath.SelectedPath;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbProgressCLKeyBind.Text = "";
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            ActiveKeyBindTextBox = tbProgressCLKeyBind;
        }

        private void ShowChecklistsMenuItem_Click(object sender, EventArgs e)
        {
            ProcessTopLevelVoiceCommand("show checklist");
        }

        private void btnSetShowCLKeyBind_Click(object sender, EventArgs e)
        {
            ActiveKeyBindTextBox = tbShowCLKeyBind;
        }

        private void btnClearShowCLKeyBind_Click(object sender, EventArgs e)
        {
            tbShowCLKeyBind.Text = "";
        }

        void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            if (ActiveKeyBindTextBox != null)
            {
                ActiveKeyBindTextBox.Text = string.Format("Key: {0}", e.KeyCode);
                ActiveKeyBindTextBox = null;
            }
            else if (e.KeyCode.ToString().Equals(ShowCLKeyBind))
                ShowChecklistsMenuItem.PerformClick();
            else if (e.KeyCode.ToString().Equals(ShowProcKeyBind))
                ProcessTopLevelVoiceCommand("show procedures");
            else if ((CLForm != null) && (CLForm.Visible))
            {
                if (e.KeyCode.ToString().Equals(ProgressCLKeyBind))
                    CLForm.ProgressChecklist();
                else if (e.KeyCode.ToString().Equals(RepeatCLIKeyBind))
                    CLForm.ReadActiveChecklistItem();
            }
        }
        #endregion

        #region Options Procs
        private void LoadOptionsSettings()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            ActiveCLFilename = Settings.Default.ChecklistFilename;
            AudioPath = Settings.Default.AudioPath;
            ProgressCLKeyBind = Settings.Default.ProgressCLKeyBind;
            ShowCLKeyBind = Settings.Default.ShowCLKeyBind;
            ShowProcKeyBind = Settings.Default.ShowProcKeyBind;
            RepeatCLIKeyBind = Settings.Default.RepeatCLIKeyBind;
            DisableSpeechRecogEng = Settings.Default.DisableSpeechRecogEng;
            AudioDeviceID = Settings.Default.AudioDeviceID;
            HideGUI = Settings.Default.HideGUI;

            Culture = Settings.Default.CultureInfo;

            if (String.IsNullOrEmpty(ActiveCLFilename))
                ActiveCLFilename = baseDir + "A2A C172.checklist";
            if (String.IsNullOrEmpty(AudioPath))
                AudioPath = baseDir + "Checklist audio files";
            if (DisableSpeechRecogEng)
            {
                nudConfTHold.Enabled = false;
                cbxCulture.Enabled = false;
                tcStatus.TabPages.Remove(tabDebug);
                ListenMenuItem.Visible = false;
                ShowChecklistsMenuItem.Enabled = true;
            }

            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                string specName = "(none)";
                try { specName = CultureInfo.CreateSpecificCulture(ci.Name).Name; }
                catch { }
                cbxCulture.Items.Add(ci.Name);
            }

            for (int deviceId = 0; deviceId < WaveOut.DeviceCount; deviceId++)
            {
                var capabilities = WaveOut.GetCapabilities(deviceId);
                cbAudioOPDevice.Items.Add(String.Format("{0})", capabilities.ProductName));
            }

            tbChecklistFilename.Text = ActiveCLFilename;
            tbAudioPath.Text = AudioPath;
            cbxCulture.SelectedIndex = cbxCulture.Items.IndexOf(Culture);
            if (!String.IsNullOrEmpty(ProgressCLKeyBind))
                tbProgressCLKeyBind.Text = string.Format("Key: {0}", ProgressCLKeyBind);
            if (!String.IsNullOrEmpty(ShowCLKeyBind))
                tbShowCLKeyBind.Text = string.Format("Key: {0}", ShowCLKeyBind);
            if (!String.IsNullOrEmpty(ShowProcKeyBind))
                tbShowProcKeyBind.Text = string.Format("Key: {0}", ShowProcKeyBind);
            if (!String.IsNullOrEmpty(RepeatCLIKeyBind))
                tbRepeatCLIKeyBind.Text = string.Format("Key: {0}", RepeatCLIKeyBind);
            xbDisableSpeechRecogEng.Checked = DisableSpeechRecogEng;
            if (cbAudioOPDevice.Items.Count > 0)
                cbAudioOPDevice.SelectedIndex = AudioDeviceID;
            xbHideGUI.Checked = HideGUI;

            ReloadOptionsSettings();
        }

        // reload only options/settings that can be adjusted without restarting
        private void ReloadOptionsSettings()
        {
            ConfidenceThreshold = Settings.Default.ConfidenceThreshold;
            nudConfTHold.Value = ConfidenceThreshold;
        }

        // returns true if a restart is required
        private bool SaveOptionSettings()
        {
            bool restartRequired = false;

            if (Settings.Default.ChecklistFilename != tbChecklistFilename.Text)
            {
                Settings.Default.ChecklistFilename = tbChecklistFilename.Text;
                restartRequired = true;
            }
            if (Settings.Default.AudioPath != tbAudioPath.Text)
            {
                Settings.Default.AudioPath = tbAudioPath.Text;
                restartRequired = true;
            }
            if (Settings.Default.ConfidenceThreshold != nudConfTHold.Value)
            {
                Settings.Default.ConfidenceThreshold = nudConfTHold.Value;
            }
            if (Settings.Default.CultureInfo != cbxCulture.Text)
            {
                Settings.Default.CultureInfo = cbxCulture.Text;
                restartRequired = true;
            }
            if (Settings.Default.ProgressCLKeyBind != tbProgressCLKeyBind.Text.Replace("Key: ", ""))
            {
                Settings.Default.ProgressCLKeyBind = tbProgressCLKeyBind.Text.Replace("Key: ", "");
                restartRequired = true;
            }
            if (Settings.Default.ShowCLKeyBind != tbShowCLKeyBind.Text.Replace("Key: ", ""))
            {
                Settings.Default.ShowCLKeyBind = tbShowCLKeyBind.Text.Replace("Key: ", "");
                restartRequired = true;
            }
            if (Settings.Default.ShowProcKeyBind != tbShowProcKeyBind.Text.Replace("Key: ", ""))
            {
                Settings.Default.ShowProcKeyBind = tbShowProcKeyBind.Text.Replace("Key: ", "");
                restartRequired = true;
            }
            if (Settings.Default.RepeatCLIKeyBind != tbRepeatCLIKeyBind.Text.Replace("Key: ", ""))
            {
                Settings.Default.RepeatCLIKeyBind = tbRepeatCLIKeyBind.Text.Replace("Key: ", "");
                restartRequired = true;
            }
            if (Settings.Default.DisableSpeechRecogEng != xbDisableSpeechRecogEng.Checked)
            {
                Settings.Default.DisableSpeechRecogEng = xbDisableSpeechRecogEng.Checked;
                restartRequired = true;
            }
            if (Settings.Default.AudioDeviceID != cbAudioOPDevice.SelectedIndex)
            {
                Settings.Default.AudioDeviceID = cbAudioOPDevice.SelectedIndex;
                restartRequired = true;
            }
            if (Settings.Default.HideGUI != xbHideGUI.Checked)
            {
                Settings.Default.HideGUI = xbHideGUI.Checked;
                restartRequired = true;
            }

            Settings.Default.Save();

            if (restartRequired)
            {
                DialogResult drApply = MessageBox.Show("The application requires a restart for the new settings to take effect. \n\nRestart now?",
                                   "Options changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return (drApply == DialogResult.Yes);
            }
            else
                return false;
        }

        private void ShowOptionsPage(TreeNode SelectedNode)
        {
            HideAllOptionPanels();
            if (SelectedNode.Name.Equals("nodeGeneral"))
            {
                pnlGeneral.Visible = true;
                pnlGeneral.Dock = DockStyle.Fill;
            }
            else if (SelectedNode.Name.Equals("nodeVoice"))
            {
                pnlVoice.Visible = true;
                pnlVoice.Dock = DockStyle.Fill;
            }
            else if (SelectedNode.Name.Equals("nodeAudio"))
            {
                pnlAudio.Visible = true;
                pnlAudio.Dock = DockStyle.Fill;
            }
        }
        #endregion

        private void OpenChecklists()
        {
            if ((CLForm == null) || ((!CLForm.Visible) && (!HideGUI)))
            {
                CLForm = new frmChecklist();
                CLForm.AcceptedChecklistCmds = AcceptedChecklistCmds;
                if (!HideGUI)
                    CLForm.Show();

                if (CLForm.LoadChecklistFile(ActiveCLFilename))
                {
                    CLForm.SetAudioPath(AudioPath);
                    CLForm.ShowNoActiveChecklist();
                }
                else
                    Console.Beep();
            }
            if ((ProcForm == null) || ((!ProcForm.Visible) && (!HideGUI)))
            {
                ProcForm = new frmProcedure();
                UpdateConnectionsLabel();
                ProcForm.checklistForm = CLForm;
                if (!HideGUI)
                    ProcForm.Show();
                ProcForm.ShowNoActiveProcedure();
                if (ProcForm.LoadChecklistFile(ActiveCLFilename))
                {
                    ProcForm.ShowNoActiveProcedure();
                }
                else
                    Console.Beep();
            }
        }

        private void UpdateConnectionsLabel()
        {
            if (ProcForm != null)
            {
                lblConnectionFS.Text = "XPUIPC connection: " + ProcForm.fsInitMessage;
                lblConnectionXP.Text = "XPLM connection: " + ProcForm.xpInitMessage;
            }
        }

        private void btnSetRepeatCLIKeyBind_Click(object sender, EventArgs e)
        {
            ActiveKeyBindTextBox = tbRepeatCLIKeyBind;
        }

        private void btnClearRepeatCLIKeyBind_Click(object sender, EventArgs e)
        {
            tbRepeatCLIKeyBind.Text = "";
        }

        private void btnSetShowProcKeyBind_Click(object sender, EventArgs e)
        {
            ActiveKeyBindTextBox = tbShowProcKeyBind;
        }

        private void btnClearShowProcKeyBind_Click(object sender, EventArgs e)
        {
            tbShowProcKeyBind.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
