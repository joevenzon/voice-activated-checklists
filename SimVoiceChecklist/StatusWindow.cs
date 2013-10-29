using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimVoiceChecklists
{
    public partial class StatusWindow : Form
    {
        public StatusWindow()
        {
            int leftBuffer = 13;

            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width - (leftBuffer*2), 20);
            this.MinimumSize = this.ClientSize;
            Left = leftBuffer;
            Top = 75;
        }

        public void ShowText(string Text, int Duration)
        {
            timer1.Stop();
            timer1.Interval = Duration;

            lblText.Text = Text;
            Visible = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}
