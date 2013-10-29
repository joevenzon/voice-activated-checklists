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
#endregion

namespace SimVoiceChecklists
{
    public partial class Form1 : Form
    {
        #region Vars
        private SpeechRecognitionEngine recognizer = null;
        private SpeechProcessing speechProc = new SpeechProcessing();
        private frmChecklist CLForm = null;
        private StatusWindow StatusWin = new StatusWindow();
        private Choices grammarChoices = new Choices();
        private string ActiveCLFilename = "C:\\Dev\\SimVoiceChecklist\\SimVoiceChecklist\\A2A C172.checklist";
        private List<string> AcceptedChecklistCmds = new List<string>();
        private float ConfidenceThreshold = 98;
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(isDisposing);
        }

        public Form1()
        {
            InitializeComponent();
            InitSpeechEngine();
        }

        #region Speech Engine Procs
        private void LoadAcceptedChecklistCmds()
        {
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

        private void AddGrammar(string Grammar)
        {
            grammarChoices.Add(Grammar);
            lbxAcceptedCmds.Items.Add(Grammar);
        }

        private void InitSpeechEngine()
        {
            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //// Add an Application Setting.
            //config.AppSettings.Settings.Add("ModificationDate", DateTime.Now.ToLongTimeString() + " ");

            //// Save the changes in App.config file.
            //config.Save(ConfigurationSaveMode.Modified);

            //// Force a reload of a changed section.
            //ConfigurationManager.RefreshSection("appSettings");


            //List<string> list = new List<string>();
            //foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            //{
            //    string specName = "(none)";
            //    try { specName = CultureInfo.CreateSpecificCulture(ci.Name).Name; }
            //    catch { }
            //    list.Add(String.Format("{0,-12}{1,-12}{2}", ci.Name, specName, ci.EnglishName));
            //}

            //list.Sort();  // sort by name

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

            LoadAcceptedChecklistCmds();
            foreach (string checklistCmd in AcceptedChecklistCmds)
                AddGrammar(checklistCmd);

            AddGrammar("Open Checklist");
            AddGrammar("Close Checklist");
            AddGrammar("Abort Checklist");

            AddGrammar("Cabin Checklist");
            AddGrammar("Before Starting Engine Checklist");
            AddGrammar("Starting Engine Checklist");
            AddGrammar("Before Takeoff Checklist");

            GrammarBuilder gbCurrent = new GrammarBuilder(grammarChoices);

            recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-GB"));
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
            float confidence = (e.Result.Confidence * 100);

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

        #region Form Event Handlers
        private void btnListen_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(btnListen.Tag) == 0)
            {
                ListenMenuItem.Checked = true;
                btnListen.Tag = 1;
                btnListen.Text = "Stop Listening";
                StartListenening();
            }
            else
            {
                ListenMenuItem.Checked = false;
                btnListen.Tag = 0;
                btnListen.Text = "Start Listening";
                StopListening();
            }
        }
        #endregion

        private bool ProcessTopLevelVoiceCommand(string VoiceCommand)
        {
            bool result = false;
            if (VoiceCommand.ToLower().Contains("checklist"))
            {
                if (VoiceCommand.ToLower().Contains("open"))
                {
                    result = true;
                    OpenChecklists();
                }
                else if ((CLForm != null) && (CLForm.Visible))
                    result = CLForm.ProcessPossibleChecklistCommand(VoiceCommand);
            }
            else if ((CLForm != null) && (CLForm.Visible))
                result = CLForm.ProcessPossibleChecklistCommand(VoiceCommand);

            return result;
        }

        private void OpenChecklists()
        {
            if ((CLForm == null) || (!CLForm.Visible))
            {
                CLForm = new frmChecklist();
                CLForm.AcceptedChecklistCmds = AcceptedChecklistCmds;
                CLForm.Show();
                if (CLForm.LoadChecklistFile(ActiveCLFilename))
                  CLForm.ShowActiveChecklist();
                else
                  Console.Beep();
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}