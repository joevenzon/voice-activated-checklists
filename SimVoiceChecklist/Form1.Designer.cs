namespace SimVoiceChecklists
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnListen = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ListenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcStatus = new System.Windows.Forms.TabControl();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvHeardDebug = new System.Windows.Forms.ListView();
            this.timeHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeardHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.confidenceHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblAcceptedCommands = new System.Windows.Forms.Label();
            this.lbxAcceptedCmds = new System.Windows.Forms.ListBox();
            this.ofdChecklistFile = new System.Windows.Forms.OpenFileDialog();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnChecklistFilename = new System.Windows.Forms.Button();
            this.lblChecklistFilename = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblConfidenceThreshold = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.tcStatus.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.tabDebug.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnListen
            // 
            this.btnListen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnListen.Location = new System.Drawing.Point(493, 363);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(94, 23);
            this.btnListen.TabIndex = 2;
            this.btnListen.Tag = "0";
            this.btnListen.Text = "Start Listening";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Visible = false;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Sim Voice Checklist";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ListenMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(106, 54);
            // 
            // ListenMenuItem
            // 
            this.ListenMenuItem.Name = "ListenMenuItem";
            this.ListenMenuItem.Size = new System.Drawing.Size(105, 22);
            this.ListenMenuItem.Text = "Listen";
            this.ListenMenuItem.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(102, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(105, 22);
            this.ExitMenuItem.Text = "Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // tcStatus
            // 
            this.tcStatus.Controls.Add(this.tpOptions);
            this.tcStatus.Controls.Add(this.tabDebug);
            this.tcStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStatus.Location = new System.Drawing.Point(0, 0);
            this.tcStatus.Name = "tcStatus";
            this.tcStatus.SelectedIndex = 0;
            this.tcStatus.Size = new System.Drawing.Size(599, 398);
            this.tcStatus.TabIndex = 7;
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.tabControl1);
            this.tpOptions.Controls.Add(this.treeView1);
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptions.Size = new System.Drawing.Size(591, 372);
            this.tpOptions.TabIndex = 0;
            this.tpOptions.Text = "Options";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // tabDebug
            // 
            this.tabDebug.Controls.Add(this.panel1);
            this.tabDebug.Controls.Add(this.panel2);
            this.tabDebug.Location = new System.Drawing.Point(4, 22);
            this.tabDebug.Name = "tabDebug";
            this.tabDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabDebug.Size = new System.Drawing.Size(591, 372);
            this.tabDebug.TabIndex = 1;
            this.tabDebug.Text = "Debug";
            this.tabDebug.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbxAcceptedCmds);
            this.panel2.Controls.Add(this.lblAcceptedCommands);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.panel2.Size = new System.Drawing.Size(136, 366);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvHeardDebug);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(139, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 366);
            this.panel1.TabIndex = 2;
            // 
            // lvHeardDebug
            // 
            this.lvHeardDebug.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timeHdr,
            this.HeardHdr,
            this.confidenceHdr});
            this.lvHeardDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvHeardDebug.Location = new System.Drawing.Point(0, 10);
            this.lvHeardDebug.Name = "lvHeardDebug";
            this.lvHeardDebug.Size = new System.Drawing.Size(449, 356);
            this.lvHeardDebug.TabIndex = 10;
            this.lvHeardDebug.UseCompatibleStateImageBehavior = false;
            this.lvHeardDebug.View = System.Windows.Forms.View.Details;
            // 
            // timeHdr
            // 
            this.timeHdr.Text = "Time";
            this.timeHdr.Width = 75;
            // 
            // HeardHdr
            // 
            this.HeardHdr.Text = "Heard";
            this.HeardHdr.Width = 125;
            // 
            // confidenceHdr
            // 
            this.confidenceHdr.Text = "Confidence (%)";
            this.confidenceHdr.Width = 100;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Maximum = 1;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(449, 10);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 9;
            // 
            // lblAcceptedCommands
            // 
            this.lblAcceptedCommands.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAcceptedCommands.Location = new System.Drawing.Point(0, 0);
            this.lblAcceptedCommands.Name = "lblAcceptedCommands";
            this.lblAcceptedCommands.Size = new System.Drawing.Size(133, 23);
            this.lblAcceptedCommands.TabIndex = 4;
            this.lblAcceptedCommands.Text = "Accepted Commands";
            this.lblAcceptedCommands.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbxAcceptedCmds
            // 
            this.lbxAcceptedCmds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxAcceptedCmds.FormattingEnabled = true;
            this.lbxAcceptedCmds.IntegralHeight = false;
            this.lbxAcceptedCmds.Location = new System.Drawing.Point(0, 23);
            this.lbxAcceptedCmds.Name = "lbxAcceptedCmds";
            this.lbxAcceptedCmds.Size = new System.Drawing.Size(133, 343);
            this.lbxAcceptedCmds.Sorted = true;
            this.lbxAcceptedCmds.TabIndex = 13;
            // 
            // ofdChecklistFile
            // 
            this.ofdChecklistFile.DefaultExt = "*.checklist";
            this.ofdChecklistFile.FileName = "openFileDialog1";
            this.ofdChecklistFile.Filter = "Checklist files|*.checklist|All files|*.*";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(121, 366);
            this.treeView1.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(124, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(464, 366);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnChecklistFilename);
            this.tabPage1.Controls.Add(this.lblChecklistFilename);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(456, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblConfidenceThreshold);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(456, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnChecklistFilename
            // 
            this.btnChecklistFilename.Location = new System.Drawing.Point(351, 21);
            this.btnChecklistFilename.Name = "btnChecklistFilename";
            this.btnChecklistFilename.Size = new System.Drawing.Size(25, 23);
            this.btnChecklistFilename.TabIndex = 7;
            this.btnChecklistFilename.TabStop = false;
            this.btnChecklistFilename.Text = "...";
            this.btnChecklistFilename.UseVisualStyleBackColor = true;
            // 
            // lblChecklistFilename
            // 
            this.lblChecklistFilename.AutoSize = true;
            this.lblChecklistFilename.Location = new System.Drawing.Point(20, 26);
            this.lblChecklistFilename.Name = "lblChecklistFilename";
            this.lblChecklistFilename.Size = new System.Drawing.Size(95, 13);
            this.lblChecklistFilename.TabIndex = 6;
            this.lblChecklistFilename.Text = "Checklist Filename";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(121, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(224, 20);
            this.textBox1.TabIndex = 5;
            // 
            // lblConfidenceThreshold
            // 
            this.lblConfidenceThreshold.AutoSize = true;
            this.lblConfidenceThreshold.Location = new System.Drawing.Point(14, 22);
            this.lblConfidenceThreshold.Name = "lblConfidenceThreshold";
            this.lblConfidenceThreshold.Size = new System.Drawing.Size(111, 13);
            this.lblConfidenceThreshold.TabIndex = 11;
            this.lblConfidenceThreshold.Text = "Confidence Threshold";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(131, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(224, 20);
            this.textBox2.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 398);
            this.Controls.Add(this.tcStatus);
            this.Controls.Add(this.btnListen);
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.tcStatus.ResumeLayout(false);
            this.tpOptions.ResumeLayout(false);
            this.tabDebug.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ListenMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.TabControl tcStatus;
        private System.Windows.Forms.TabPage tpOptions;
        private System.Windows.Forms.TabPage tabDebug;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvHeardDebug;
        private System.Windows.Forms.ColumnHeader timeHdr;
        private System.Windows.Forms.ColumnHeader HeardHdr;
        private System.Windows.Forms.ColumnHeader confidenceHdr;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblAcceptedCommands;
        private System.Windows.Forms.ListBox lbxAcceptedCmds;
        private System.Windows.Forms.OpenFileDialog ofdChecklistFile;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnChecklistFilename;
        private System.Windows.Forms.Label lblChecklistFilename;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblConfidenceThreshold;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TreeView treeView1;
    }
}

