namespace SimVoiceChecklists
{
    partial class frmProcedure
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
            this.components = new System.ComponentModel.Container();
            this.lblProcedureHeader = new System.Windows.Forms.Label();
            this.tmrNextItem = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblProcedureHeader
            // 
            this.lblProcedureHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProcedureHeader.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcedureHeader.ForeColor = System.Drawing.Color.White;
            this.lblProcedureHeader.Location = new System.Drawing.Point(0, 0);
            this.lblProcedureHeader.Name = "lblProcedureHeader";
            this.lblProcedureHeader.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblProcedureHeader.Size = new System.Drawing.Size(308, 28);
            this.lblProcedureHeader.TabIndex = 16;
            this.lblProcedureHeader.Text = "Procedure: <Name>";
            this.lblProcedureHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProcedureHeader.Click += new System.EventHandler(this.lblProcedureHeader_Click);
            // 
            // tmrNextItem
            // 
            this.tmrNextItem.Interval = 2000;
            this.tmrNextItem.Tick += new System.EventHandler(this.tmrShow_Tick);
            // 
            // frmProcedure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(308, 28);
            this.Controls.Add(this.lblProcedureHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 50);
            this.Name = "frmProcedure";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmProcedure";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblProcedureHeader;
        private System.Windows.Forms.Timer tmrNextItem;
    }
}