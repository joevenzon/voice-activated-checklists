namespace SimVoiceChecklists
{
    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Voice");
            this.btnListen = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ListenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcStatus = new System.Windows.Forms.TabControl();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlOptionDetails = new System.Windows.Forms.Panel();
            this.pnlGeneral = new System.Windows.Forms.Panel();
            this.btnAudioPath = new System.Windows.Forms.Button();
            this.tbAudioPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnChecklistFilename = new System.Windows.Forms.Button();
            this.lblChecklistFilename = new System.Windows.Forms.Label();
            this.tbChecklistFilename = new System.Windows.Forms.TextBox();
            this.tvOptions = new System.Windows.Forms.TreeView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabDebug = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvHeardDebug = new System.Windows.Forms.ListView();
            this.timeHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HeardHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.confidenceHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbxAcceptedCmds = new System.Windows.Forms.ListBox();
            this.lblAcceptedCommands = new System.Windows.Forms.Label();
            this.ofdChecklistFile = new System.Windows.Forms.OpenFileDialog();
            this.fbdAudioPath = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlVoice = new System.Windows.Forms.Panel();
            this.cbxCulture = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudConfTHold = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.tcStatus.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlOptionDetails.SuspendLayout();
            this.pnlGeneral.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabDebug.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlVoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudConfTHold)).BeginInit();
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
            this.OptionsMenuItem,
            this.toolStripSeparator2,
            this.AboutMenuItem,
            this.toolStripSeparator1,
            this.ExitMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 104);
            // 
            // ListenMenuItem
            // 
            this.ListenMenuItem.Name = "ListenMenuItem";
            this.ListenMenuItem.Size = new System.Drawing.Size(116, 22);
            this.ListenMenuItem.Text = "Listen";
            this.ListenMenuItem.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // OptionsMenuItem
            // 
            this.OptionsMenuItem.Name = "OptionsMenuItem";
            this.OptionsMenuItem.Size = new System.Drawing.Size(116, 22);
            this.OptionsMenuItem.Text = "Options";
            this.OptionsMenuItem.Click += new System.EventHandler(this.OptionsMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(113, 6);
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(116, 22);
            this.AboutMenuItem.Text = "About";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(116, 22);
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
            this.tpOptions.Controls.Add(this.panel3);
            this.tpOptions.Controls.Add(this.panel4);
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptions.Size = new System.Drawing.Size(591, 372);
            this.tpOptions.TabIndex = 0;
            this.tpOptions.Text = "Options";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlOptionDetails);
            this.panel3.Controls.Add(this.tvOptions);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(585, 331);
            this.panel3.TabIndex = 2;
            // 
            // pnlOptionDetails
            // 
            this.pnlOptionDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOptionDetails.Controls.Add(this.pnlVoice);
            this.pnlOptionDetails.Controls.Add(this.pnlGeneral);
            this.pnlOptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOptionDetails.Location = new System.Drawing.Point(121, 0);
            this.pnlOptionDetails.Name = "pnlOptionDetails";
            this.pnlOptionDetails.Size = new System.Drawing.Size(464, 331);
            this.pnlOptionDetails.TabIndex = 13;
            // 
            // pnlGeneral
            // 
            this.pnlGeneral.Controls.Add(this.btnAudioPath);
            this.pnlGeneral.Controls.Add(this.tbAudioPath);
            this.pnlGeneral.Controls.Add(this.label3);
            this.pnlGeneral.Controls.Add(this.btnChecklistFilename);
            this.pnlGeneral.Controls.Add(this.lblChecklistFilename);
            this.pnlGeneral.Controls.Add(this.tbChecklistFilename);
            this.pnlGeneral.Location = new System.Drawing.Point(16, 15);
            this.pnlGeneral.Name = "pnlGeneral";
            this.pnlGeneral.Size = new System.Drawing.Size(417, 160);
            this.pnlGeneral.TabIndex = 1;
            // 
            // btnAudioPath
            // 
            this.btnAudioPath.Location = new System.Drawing.Point(251, 88);
            this.btnAudioPath.Name = "btnAudioPath";
            this.btnAudioPath.Size = new System.Drawing.Size(25, 22);
            this.btnAudioPath.TabIndex = 20;
            this.btnAudioPath.TabStop = false;
            this.btnAudioPath.Text = "...";
            this.btnAudioPath.UseVisualStyleBackColor = true;
            this.btnAudioPath.Click += new System.EventHandler(this.btnAudioPath_Click);
            // 
            // tbAudioPath
            // 
            this.tbAudioPath.Location = new System.Drawing.Point(21, 90);
            this.tbAudioPath.Name = "tbAudioPath";
            this.tbAudioPath.Size = new System.Drawing.Size(224, 20);
            this.tbAudioPath.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Checklist audio path";
            // 
            // btnChecklistFilename
            // 
            this.btnChecklistFilename.Location = new System.Drawing.Point(251, 41);
            this.btnChecklistFilename.Name = "btnChecklistFilename";
            this.btnChecklistFilename.Size = new System.Drawing.Size(25, 23);
            this.btnChecklistFilename.TabIndex = 16;
            this.btnChecklistFilename.TabStop = false;
            this.btnChecklistFilename.Text = "...";
            this.btnChecklistFilename.UseVisualStyleBackColor = true;
            this.btnChecklistFilename.Click += new System.EventHandler(this.btnChecklistFilename_Click);
            // 
            // lblChecklistFilename
            // 
            this.lblChecklistFilename.AutoSize = true;
            this.lblChecklistFilename.Location = new System.Drawing.Point(18, 27);
            this.lblChecklistFilename.Name = "lblChecklistFilename";
            this.lblChecklistFilename.Size = new System.Drawing.Size(95, 13);
            this.lblChecklistFilename.TabIndex = 15;
            this.lblChecklistFilename.Text = "Checklist Filename";
            // 
            // tbChecklistFilename
            // 
            this.tbChecklistFilename.Location = new System.Drawing.Point(21, 43);
            this.tbChecklistFilename.Name = "tbChecklistFilename";
            this.tbChecklistFilename.Size = new System.Drawing.Size(224, 20);
            this.tbChecklistFilename.TabIndex = 14;
            // 
            // tvOptions
            // 
            this.tvOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvOptions.HideSelection = false;
            this.tvOptions.Location = new System.Drawing.Point(0, 0);
            this.tvOptions.Name = "tvOptions";
            treeNode3.Name = "nodeGeneral";
            treeNode3.Text = "General";
            treeNode4.Name = "nodeVoice";
            treeNode4.Text = "Voice";
            this.tvOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.tvOptions.Size = new System.Drawing.Size(121, 331);
            this.tvOptions.TabIndex = 12;
            this.tvOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvOptions_AfterSelect);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnApply);
            this.panel4.Controls.Add(this.btnCancel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 334);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(585, 35);
            this.panel4.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(424, 6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(505, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // ofdChecklistFile
            // 
            this.ofdChecklistFile.DefaultExt = "*.checklist";
            this.ofdChecklistFile.FileName = "openFileDialog1";
            this.ofdChecklistFile.Filter = "Checklist files|*.checklist|All files|*.*";
            // 
            // pnlVoice
            // 
            this.pnlVoice.Controls.Add(this.cbxCulture);
            this.pnlVoice.Controls.Add(this.label2);
            this.pnlVoice.Controls.Add(this.nudConfTHold);
            this.pnlVoice.Controls.Add(this.label1);
            this.pnlVoice.Location = new System.Drawing.Point(112, 49);
            this.pnlVoice.Name = "pnlVoice";
            this.pnlVoice.Size = new System.Drawing.Size(239, 231);
            this.pnlVoice.TabIndex = 7;
            // 
            // cbxCulture
            // 
            this.cbxCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCulture.FormattingEnabled = true;
            this.cbxCulture.Location = new System.Drawing.Point(21, 90);
            this.cbxCulture.Name = "cbxCulture";
            this.cbxCulture.Size = new System.Drawing.Size(101, 21);
            this.cbxCulture.Sorted = true;
            this.cbxCulture.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Confidence Threshold";
            // 
            // nudConfTHold
            // 
            this.nudConfTHold.DecimalPlaces = 2;
            this.nudConfTHold.Location = new System.Drawing.Point(21, 43);
            this.nudConfTHold.Name = "nudConfTHold";
            this.nudConfTHold.Size = new System.Drawing.Size(68, 20);
            this.nudConfTHold.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Confidence Threshold";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 398);
            this.Controls.Add(this.tcStatus);
            this.Controls.Add(this.btnListen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sim Voice Checklist Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.Form1_VisibleChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tcStatus.ResumeLayout(false);
            this.tpOptions.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlOptionDetails.ResumeLayout(false);
            this.pnlGeneral.ResumeLayout(false);
            this.pnlGeneral.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tabDebug.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlVoice.ResumeLayout(false);
            this.pnlVoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudConfTHold)).EndInit();
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
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlOptionDetails;
        private System.Windows.Forms.Panel pnlGeneral;
        private System.Windows.Forms.Button btnChecklistFilename;
        private System.Windows.Forms.Label lblChecklistFilename;
        private System.Windows.Forms.TextBox tbChecklistFilename;
        private System.Windows.Forms.TreeView tvOptions;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStripMenuItem OptionsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAudioPath;
        private System.Windows.Forms.TextBox tbAudioPath;
        private System.Windows.Forms.FolderBrowserDialog fbdAudioPath;
        private System.Windows.Forms.Panel pnlVoice;
        private System.Windows.Forms.ComboBox cbxCulture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudConfTHold;
        private System.Windows.Forms.Label label1;
    }
}

