using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;

namespace SimVoiceChecklists
{
    public partial class Splash : Form
    {
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

        public Splash()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            lblVersion.Parent = pictureBox1;
            lblVersion.BackColor = Color.Transparent;
            lblMessage.Parent = pictureBox1;
            lblMessage.BackColor = Color.Transparent;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            lblVersion.Text = "v" + fileVersionInfo.ProductVersion;
            if (lblVersion.Right > this.Width)
                lblVersion.Left = this.Width - lblVersion.Width - 5;
        }

        public void ShowSplash(int Interval)
        {
            tmrShow.Stop();
            tmrShow.Interval = Interval;
            Show();
            tmrShow.Start();
        }

        private void tmrShow_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
