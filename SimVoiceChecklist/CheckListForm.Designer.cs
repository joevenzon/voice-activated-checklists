namespace SimVoiceChecklists
{
    partial class frmChecklist
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvAvailableChecklists = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvChecklistItems = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblChecklistHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvAvailableChecklists
            // 
            this.lvAvailableChecklists.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvAvailableChecklists.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.lvAvailableChecklists.FullRowSelect = true;
            this.lvAvailableChecklists.HideSelection = false;
            this.lvAvailableChecklists.Location = new System.Drawing.Point(393, 41);
            this.lvAvailableChecklists.Name = "lvAvailableChecklists";
            this.lvAvailableChecklists.Size = new System.Drawing.Size(187, 159);
            this.lvAvailableChecklists.TabIndex = 14;
            this.lvAvailableChecklists.UseCompatibleStateImageBehavior = false;
            this.lvAvailableChecklists.View = System.Windows.Forms.View.Details;
            this.lvAvailableChecklists.SelectedIndexChanged += new System.EventHandler(this.lvAvailableChecklists_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ID";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Checklist Name";
            this.columnHeader5.Width = 300;
            // 
            // lvChecklistItems
            // 
            this.lvChecklistItems.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvChecklistItems.CheckBoxes = true;
            this.lvChecklistItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader6});
            this.lvChecklistItems.HideSelection = false;
            this.lvChecklistItems.Location = new System.Drawing.Point(163, 41);
            this.lvChecklistItems.Name = "lvChecklistItems";
            this.lvChecklistItems.Size = new System.Drawing.Size(224, 176);
            this.lvChecklistItems.TabIndex = 13;
            this.lvChecklistItems.UseCompatibleStateImageBehavior = false;
            this.lvChecklistItems.View = System.Windows.Forms.View.Details;
            this.lvChecklistItems.SelectedIndexChanged += new System.EventHandler(this.lvChecklistItems_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Instruction";
            this.columnHeader2.Width = 0;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Command";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "WAV File";
            this.columnHeader6.Width = 0;
            // 
            // lblChecklistHeader
            // 
            this.lblChecklistHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChecklistHeader.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChecklistHeader.ForeColor = System.Drawing.Color.White;
            this.lblChecklistHeader.Location = new System.Drawing.Point(0, 0);
            this.lblChecklistHeader.Name = "lblChecklistHeader";
            this.lblChecklistHeader.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblChecklistHeader.Size = new System.Drawing.Size(308, 28);
            this.lblChecklistHeader.TabIndex = 15;
            this.lblChecklistHeader.Text = "Checklist: <Name>";
            this.lblChecklistHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblChecklistHeader.Click += new System.EventHandler(this.lblChecklistHeader_Click);
            // 
            // frmChecklist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(308, 28);
            this.Controls.Add(this.lblChecklistHeader);
            this.Controls.Add(this.lvAvailableChecklists);
            this.Controls.Add(this.lvChecklistItems);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 50);
            this.Name = "frmChecklist";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form2";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvAvailableChecklists;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ListView lvChecklistItems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label lblChecklistHeader;

    }
}