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
using System.Runtime.InteropServices;

namespace SimVoiceChecklists
{
    public partial class ChecklistList : Form
    {
        public frmChecklist CLForm;
        private const int CS_DROPSHADOW = 0x00020000;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );

        // Override the CreateParams property
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public ChecklistList()
        {
            InitializeComponent();
        }

        public void LoadChecklistFile(string CheckListFilename)
        {
            string LongestText = "";
            int LabelHeight = 0;
            int ItemCount = 0;

            if (System.IO.File.Exists(CheckListFilename))
                foreach (XElement checklist in XElement.Load(CheckListFilename).Elements("checklist"))
                {
                    ItemCount += 1;
                    Label lblNew = new Label();
                    if (checklist.Attribute("name").Value.Trim().Length > 0)
                        lblNew.Text = checklist.Attribute("name").Value;
                    else
                        lblNew.Text = "∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞∞";
                    lblNew.TextAlign = ContentAlignment.MiddleCenter;
                    lblNew.Dock = DockStyle.Bottom;
                    lblNew.Click += lblNew_Click;
                    pnlHost.Controls.Add(lblNew);

                    LabelHeight = lblNew.Height;
                    if (lblNew.Text.Length > LongestText.Length)
                        LongestText = lblNew.Text;
                }

            SizeF sizeText = CreateGraphics().MeasureString(LongestText, Font);
            this.Size = new Size((int)sizeText.Width + 50, (LabelHeight * ItemCount) + (pnlHost.Padding.All*2));
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            this.Location = new Point((screenWidth / 2) - (this.Width / 2), (screenHeight / 2) - (this.Height / 2));

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            tmrShow.Stop();
            tmrShow.Interval = 3500;
            Show();
            tmrShow.Start();
        }

        void lblNew_Click(object sender, EventArgs e)
        {
            CLForm.ProcessPossibleChecklistCommand(((Label)sender).Text + " checklist");
            //throw new NotImplementedException();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

    }
}
