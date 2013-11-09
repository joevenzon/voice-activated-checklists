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
        private StatusWindow StatusWin = new StatusWindow();
        private ChecklistList CLListForm;

        private string ActiveCLFilename;
        private string AudioPath;
        private decimal ConfidenceThreshold;
        private string Culture;
        private string ProgressCLKeyBind;
        private string ShowCLKeyBind;
        private bool DisableSpeechRecogEng;
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
            (new Splash()).ShowSplash(3000);
            InitializeComponent();
            HookManager.KeyUp += HookManager_KeyUp;
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
                    CLListForm.LoadChecklistFile(ActiveCLFilename);
                }
                else if ((CLForm != null) && (CLForm.Visible))
                    result = CLForm.ProcessPossibleChecklistCommand(VoiceCommand);
            }
            else if ((CLForm != null) && (CLForm.Visible))
                result = CLForm.ProcessPossibleChecklistCommand(VoiceCommand);

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
            AddGrammar("Thankyou");
            AddGrammar("Thanks");

            foreach (XElement checklist in XElement.Load(ActiveCLFilename).Elements("checklist"))
                AddGrammar(checklist.Attribute("name").Value + " Checklist");

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
                if ((!String.IsNullOrEmpty(ActiveCLFilename)) || (System.IO.File.Exists(ActiveCLFilename)))
                {
                    ListenMenuItem.Checked = true;
                    btnListen.Tag = 1;
                    btnListen.Text = "Stop Listening";
                    InitSpeechEngine();
                    StartListenening();
                    ShowChecklistsMenuItem.Enabled = true;
                }
                else
                    MessageBox.Show("No checklist defined", "Error loading checklist", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AllowExit)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void OptionsMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            (new About()).Show();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
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
            else if ((CLForm != null) && (CLForm.Visible))
            {
                if (e.KeyCode.ToString().Equals(ProgressCLKeyBind))
                    CLForm.ProgressChecklist();
            }
        }
        #endregion

        #region Options Procs
        private void LoadOptionsSettings()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            ActiveCLFilename = Settings.Default.ChecklistFilename;
            ConfidenceThreshold = Settings.Default.ConfidenceThreshold;
            AudioPath = Settings.Default.AudioPath;
            ProgressCLKeyBind = Settings.Default.ProgressCLKeyBind;
            ShowCLKeyBind = Settings.Default.ShowCLKeyBind;
            DisableSpeechRecogEng = Settings.Default.DisableSpeechRecogEng;

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

            tbChecklistFilename.Text = ActiveCLFilename;
            tbAudioPath.Text = AudioPath;
            nudConfTHold.Value = ConfidenceThreshold;
            cbxCulture.SelectedIndex = cbxCulture.Items.IndexOf(Culture);
            if (!String.IsNullOrEmpty(ProgressCLKeyBind))
                tbProgressCLKeyBind.Text = string.Format("Key: {0}", ProgressCLKeyBind);
            if (!String.IsNullOrEmpty(ShowCLKeyBind))
                tbShowCLKeyBind.Text = string.Format("Key: {0}", ShowCLKeyBind);
            xbDisableSpeechRecogEng.Checked = DisableSpeechRecogEng;
        }

        private bool SaveOptionSettings()
        {
            Settings.Default.ChecklistFilename = tbChecklistFilename.Text;
            Settings.Default.AudioPath = tbAudioPath.Text;
            Settings.Default.ConfidenceThreshold = nudConfTHold.Value;
            Settings.Default.CultureInfo = cbxCulture.Text;
            Settings.Default.ProgressCLKeyBind = tbProgressCLKeyBind.Text.Replace("Key: ", "");
            Settings.Default.ShowCLKeyBind = tbShowCLKeyBind.Text.Replace("Key: ", "");
            Settings.Default.DisableSpeechRecogEng = xbDisableSpeechRecogEng.Checked;

            Settings.Default.Save();

            DialogResult drApply = MessageBox.Show("The application requires a restart for the new settings to take effect. \n\nRestart now?",
                                                   "Options changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (drApply == DialogResult.Yes);
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
        }
        #endregion

        private void OpenChecklists()
        {
            if ((CLForm == null) || (!CLForm.Visible))
            {
                CLForm = new frmChecklist();
                CLForm.AcceptedChecklistCmds = AcceptedChecklistCmds;
                CLForm.Show();
                if (CLForm.LoadChecklistFile(ActiveCLFilename))
                {
                    CLForm.SetAudioPath(AudioPath);
                    CLForm.ShowNoActiveChecklist();
                }
                else
                    Console.Beep();
            }
        }
    }
}