namespace VMSRXMifare1K
{
    partial class frmMain
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
            this.txtPort1 = new System.Windows.Forms.TextBox();
            this.lblPort1 = new System.Windows.Forms.Label();
            this.lblReader1Status = new System.Windows.Forms.Label();
            this.tmrReaderScan = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtPort1
            // 
            this.txtPort1.Location = new System.Drawing.Point(185, 20);
            this.txtPort1.Name = "txtPort1";
            this.txtPort1.ReadOnly = true;
            this.txtPort1.Size = new System.Drawing.Size(39, 20);
            this.txtPort1.TabIndex = 0;
            // 
            // lblPort1
            // 
            this.lblPort1.AutoSize = true;
            this.lblPort1.Location = new System.Drawing.Point(37, 23);
            this.lblPort1.Name = "lblPort1";
            this.lblPort1.Size = new System.Drawing.Size(142, 13);
            this.lblPort1.TabIndex = 1;
            this.lblPort1.Text = "High Frequency Reader Port";
            // 
            // lblReader1Status
            // 
            this.lblReader1Status.AutoSize = true;
            this.lblReader1Status.Location = new System.Drawing.Point(415, 23);
            this.lblReader1Status.Name = "lblReader1Status";
            this.lblReader1Status.Size = new System.Drawing.Size(110, 13);
            this.lblReader1Status.TabIndex = 2;
            this.lblReader1Status.Text = "Reader 1 Staus Label";
            // 
            // tmrReaderScan
            // 
            this.tmrReaderScan.Tick += new System.EventHandler(this.tmrReaderScan_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 261);
            this.Controls.Add(this.lblReader1Status);
            this.Controls.Add(this.lblPort1);
            this.Controls.Add(this.txtPort1);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPort1;
        private System.Windows.Forms.Label lblPort1;
        private System.Windows.Forms.Label lblReader1Status;
        private System.Windows.Forms.Timer tmrReaderScan;
    }
}