using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml.Linq;
using FSUIPC;

namespace SimVoiceChecklists
{
    struct ProcedureItem
    {
        public string name;
        public string say;
        public int time;
        public string fsuipc;
    }

    public partial class frmProcedure : Form
    {
        public frmChecklist checklistForm = null;
        string CheckListFilename;
        List<ProcedureItem> activeProcedure = new List<ProcedureItem>();
        List<string> procedures = new List<string>();
        IEnumerator<ProcedureItem> nextProcedureItem = null;
        public string fsInitMessage = "uninitialized";
        public string xpInitMessage = "uninitialized";
        public bool singleStepMode = false;

        public frmProcedure()
        {
            InitializeComponent();

            Top = 45+45;
            Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 13;
            this.ClientSize = new System.Drawing.Size(310, 24);
            this.MinimumSize = this.ClientSize;

            InitFlightsim();
        }

        private bool InitFS()
        {
            bool success = true;

            fsInitMessage = "connected";

            try
            {
                // Attempt to open a connection to FSUIPC (running on any version of Flight Sim)
                FSUIPCConnection.Open();
            }
            catch (Exception ex)
            {
                // Badness occurred - show the error message
                FSUIPCConnection.Close();
                Debug.Print(ex.Message);
                fsInitMessage = ex.Message;
                success = false;
            }

            return success;
        }

        private bool InitXP()
        {
            return false;
        }

        private bool InitFlightsim()
        {
            bool fs = InitFS();
            bool xp = InitXP();
            return fs || xp;
        }

        public bool StartProcedure(string procname)
        {
            bool result = false;

            if (procedures.Contains(procname) &&
                System.IO.File.Exists(CheckListFilename))
            {    
                foreach (XElement procedure in XElement.Load(CheckListFilename).Elements("procedure"))
                {
                    if (procedure.Attribute("name").Value.ToLower() == procname)
                    {
                        ShowProcedure(procname);
                        activeProcedure.Clear();

                        if (checklistForm != null)
                        {
                            if (procedure.Attribute("say") != null)
                                checklistForm.Say(procedure.Attribute("say").Value);
                            else
                                checklistForm.Say(procname + " procedure");
                        }

                        foreach (XElement items in procedure.Elements("item"))
                        {
                            string name = "";
                            int time = 1; // default to 1 second per item
                            string fsuipc = "";
                            string say = "";

                            if (items.Attribute("name") != null)
                                name = items.Attribute("name").Value;
                            if (items.Attribute("time") != null)
                                time = Convert.ToInt32(items.Attribute("time").Value);
                            if (items.Attribute("fsuipc") != null)
                                fsuipc = items.Attribute("fsuipc").Value;
                            if (items.Attribute("say") != null)
                                say = items.Attribute("say").Value;

                            ProcedureItem item = new ProcedureItem();
                            item.name = name;
                            item.time = time;
                            item.fsuipc = fsuipc;
                            item.say = say;

                            activeProcedure.Add(item);
                        }

                        nextProcedureItem = activeProcedure.GetEnumerator();
                        if (nextProcedureItem.MoveNext())
                        {
                            if (!singleStepMode)
                            {
                                tmrNextItem.Interval = Math.Max(1, nextProcedureItem.Current.time) * 1000;
                                tmrNextItem.Start();
                            }
                            else
                                tmrNextItem.Stop();
                            result = true;
                        }
                        else
                            EndProcedure();

                        break;
                    }
                }
            }

            return result;
        }

        public void OnTimedEvent(Object source, EventArgs e)
        {
            DoProcedureItem(true);
        }

        public void DoProcedureItem(bool advance)
        {
            if (nextProcedureItem != null)
            {
                Debug.Print(nextProcedureItem.Current.name);
                fsExecute(nextProcedureItem.Current.fsuipc);

                if (checklistForm != null && nextProcedureItem.Current.say != "")
                {
                    checklistForm.Say(nextProcedureItem.Current.say);
                }

                if (advance)
                {
                    if (nextProcedureItem.MoveNext())
                    {
                        if (!singleStepMode)
                        {
                            tmrNextItem.Interval = Math.Max(1, nextProcedureItem.Current.time) * 1000;
                            tmrNextItem.Start();
                        }
                        else
                            tmrNextItem.Stop();
                    }
                    else
                        EndProcedure();
                }
            }
        }

        public void OnDebugKeyEvent(string key)
        {
            /*if (key == "D9")
            {
                singleStepMode = true;
            }
            else if (key == "D0")
            {
                singleStepMode = true;
                EndProcedure();
            }
            else if (key == "D1")
            {
                singleStepMode = true;
                DoProcedureItem(false);
            }
            else if (key == "D2")
            {
                singleStepMode = true;

                if (nextProcedureItem != null)
                {
                    if (nextProcedureItem.MoveNext())
                    {
                        tmrNextItem.Stop();
                    }
                    else
                        EndProcedure();
                }
                DoProcedureItem(false);
            }*/
        }

        public void EndProcedure()
        {
            tmrNextItem.Stop();
            nextProcedureItem = null;
            activeProcedure.Clear();
            ShowNoActiveProcedure();
        }

        public void ShowProcedure(string procname)
        {
            lblProcedureHeader.Text = "Active Procedure: " + procname;
        }

        public void ShowNoActiveProcedure()
        {
            lblProcedureHeader.Text = "No procedure active";
        }

        public bool LoadChecklistFile(string CheckListFilename)
        {
            bool result = false;
            int clID = 1;
            activeProcedure.Clear();
            procedures.Clear();
            if (System.IO.File.Exists(CheckListFilename))
            {
                foreach (XElement procedure in XElement.Load(CheckListFilename).Elements("procedure"))
                {
                    this.CheckListFilename = CheckListFilename;
                    clID += 1;

                    procedures.Add(procedure.Attribute("name").Value.ToLower());
                }
                result = true;
            }
            return result;
        }

        private void lblProcedureHeader_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tmrShow_Tick(object sender, EventArgs e)
        {
            OnTimedEvent(sender, e);
        }

        private bool fsExecute(string commandlist)
        {
            bool success = true;

            string[] commands = commandlist.Split(new string[]{","}, StringSplitOptions.RemoveEmptyEntries);
            List<Offset<int>> offsets = new List<Offset<int>>();

            //process conditionals
            bool conditional = true;
            foreach (string command in commands)
            {
                if (command.Contains("?"))
                {
                    string[] symbols = command.Trim(new char[] { '?' }).Split(new string[] { "=" }, StringSplitOptions.None);
                    if (symbols.Length == 2)
                    {
                        bool invert = false;
                        bool lt = false;
                        bool gt = false;

                        if (symbols[0].Contains('!'))
                        {
                            invert = true;
                            symbols[0] = symbols[0].Replace("!", "");
                        }

                        if (symbols[0].Contains('<'))
                        {
                            lt = true;
                            symbols[0] = symbols[0].Replace("<", "");
                        }

                        if (symbols[0].Contains('>'))
                        {
                            gt = true;
                            symbols[0] = symbols[0].Replace(">", "");
                        }

                        Debug.Print(symbols[0]);
                        Debug.Print(symbols[1]);

                        int address = stringToInt(symbols[0]);
                        int value = stringToInt(symbols[1]);

                        Offset<Byte> offset = new Offset<Byte>(address);
                        
                        try
                        {
                            FSUIPCConnection.Process();
                        }
                        catch (Exception ex)
                        {
                            fsInitMessage = ex.Message;
                            success = false;
                            conditional = false;
                        }

                        if (success)
                        {
                            conditional = (offset.Value == value);

                            if (lt)
                                conditional = (offset.Value <= value);

                            if (gt)
                                conditional = (offset.Value >= value);

                            if (invert)
                                conditional = !conditional;
                        }

                        offset.Disconnect();
                    }
                }
            }

            // process commands
            if (conditional)
            {
                foreach (string command in commands)
                {
                    if (!command.Contains("?"))
                    {
                        string[] symbols = command.Split(new string[] { "=" }, StringSplitOptions.None);
                        if (symbols.Length == 2)
                        {
                            int address = stringToInt(symbols[0]);
                            int value = stringToInt(symbols[1]);

                            Offset<int> offset = new Offset<int>(address, true);
                            offset.Value = value;
                            offsets.Add(offset);
                        }
                    }
                }
            }
            else
            {
                Debug.Print("skipping previous step because conditional evaluated to false");
            }

            try
            {
                FSUIPCConnection.Process();
            }
            catch (Exception ex)
            {
                fsInitMessage = ex.Message;
                success = false;
            }

            foreach (Offset<int> offset in offsets)
            {
                offset.Disconnect();
            }

            return success;
        }

        int stringToInt(string str)
        {
            int result = 0;

            try
            {
                if (str.StartsWith("0x"))
                {
                    string numerals = str.Substring(2);
                    result = Convert.ToInt32(numerals, 16);
                }
                else
                {
                    result = Convert.ToInt32(str);
                }
            }
            catch (Exception)
            {
                result = 0;
            }

            return result;
        }

        private bool fsExecuteCommand(string command)
        {
            bool success = false;

            string[] symbols = command.Split(new string[] { "=" }, StringSplitOptions.None);
            if (symbols.Length == 2)
            {
                int offset = stringToInt(symbols[0]);
                int value = stringToInt(symbols[1]);
                success = fsSetInt(offset, value);
            }

            return success;
        }

        private bool fsSetInt(int address, int value)
        {
            bool success = true;

            Offset<int> offset = new Offset<int>(address, true);
            offset.Value = value;
            try
            {
                FSUIPCConnection.Process();
            }
            catch (Exception ex)
            {
                fsInitMessage = ex.Message;
                success = false;
            }
            offset.Disconnect();

            return success;
        }
    }
}
