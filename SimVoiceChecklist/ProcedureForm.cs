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
                    if (procedure.Attribute("name").Value == procname)
                    {
                        ShowProcedure(procname);
                        activeProcedure.Clear();

                        foreach (XElement items in procedure.Elements("item"))
                        {
                            string name = "";
                            int time = 2; // default to 2 seconds per item
                            string fsuipc = "";

                            if (items.Attribute("name") != null)
                                name = items.Attribute("name").Value;
                            if (items.Attribute("time") != null)
                                time = Convert.ToInt32(items.Attribute("time").Value);
                            if (items.Attribute("fsuipc") != null)
                                fsuipc = items.Attribute("fsuipc").Value;

                            ProcedureItem item = new ProcedureItem();
                            item.name = name;
                            item.time = time;
                            item.fsuipc = fsuipc;

                            activeProcedure.Add(item);
                        }

                        nextProcedureItem = activeProcedure.GetEnumerator();
                        if (nextProcedureItem.MoveNext())
                        {
                            tmrNextItem.Interval = Math.Max(1, nextProcedureItem.Current.time) * 1000;
                            tmrNextItem.Start();
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
            if (nextProcedureItem != null)
            {
                Debug.Print(nextProcedureItem.Current.name);
                fsExecute(nextProcedureItem.Current.fsuipc);

                if (nextProcedureItem.MoveNext())
                {
                    tmrNextItem.Interval = Math.Max(1, nextProcedureItem.Current.time) * 1000;
                    tmrNextItem.Start();
                }
                else
                    EndProcedure();
            }
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
            if (checklistForm != null)
                checklistForm.Say(procname);
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

                    procedures.Add(procedure.Attribute("name").Value);
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

        private void fsExecute(string commandlist)
        {
            string[] commands = commandlist.Split(new string[]{","}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string command in commands)
            {
                fsExecuteCommand(command.Trim());
            }
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
            catch (Exception ex)
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
