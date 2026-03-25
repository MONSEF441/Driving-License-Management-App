namespace DVLD_PresentationAccess
{
    partial class frmDetainLicense
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
            this.label3 = new System.Windows.Forms.Label();
            this.lblShowNewLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.lblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.btnDetain = new Guna.UI2.WinForms.Guna2GradientCircleButton();
            this.btnClose = new Guna.UI2.WinForms.Guna2GradientCircleButton();
            this.ucSearchLicense1 = new DVLD_PresentationAccess.ucSearchLicense();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ucDetain1 = new DVLD_PresentationAccess.ucDetain();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label3.Location = new System.Drawing.Point(82, 564);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 20);
            this.label3.TabIndex = 84;
            this.label3.Text = "Application Info :";
            // 
            // lblShowNewLicenseInfo
            // 
            this.lblShowNewLicenseInfo.AutoSize = true;
            this.lblShowNewLicenseInfo.Enabled = false;
            this.lblShowNewLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblShowNewLicenseInfo.LinkColor = System.Drawing.Color.MediumSlateBlue;
            this.lblShowNewLicenseInfo.Location = new System.Drawing.Point(306, 749);
            this.lblShowNewLicenseInfo.Name = "lblShowNewLicenseInfo";
            this.lblShowNewLicenseInfo.Size = new System.Drawing.Size(140, 20);
            this.lblShowNewLicenseInfo.TabIndex = 88;
            this.lblShowNewLicenseInfo.TabStop = true;
            this.lblShowNewLicenseInfo.Text = "Show License Info";
            this.lblShowNewLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowNewLicenseInfo_LinkClicked);
            // 
            // lblShowLicenseHistory
            // 
            this.lblShowLicenseHistory.AutoSize = true;
            this.lblShowLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblShowLicenseHistory.LinkColor = System.Drawing.Color.MediumSlateBlue;
            this.lblShowLicenseHistory.Location = new System.Drawing.Point(82, 749);
            this.lblShowLicenseHistory.Name = "lblShowLicenseHistory";
            this.lblShowLicenseHistory.Size = new System.Drawing.Size(169, 20);
            this.lblShowLicenseHistory.TabIndex = 87;
            this.lblShowLicenseHistory.TabStop = true;
            this.lblShowLicenseHistory.Text = "Show Licenses History";
            this.lblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowLicenseHistory_LinkClicked);
            // 
            // btnDetain
            // 
            this.btnDetain.BackColor = System.Drawing.Color.Transparent;
            this.btnDetain.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDetain.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDetain.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDetain.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDetain.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDetain.Enabled = false;
            this.btnDetain.FillColor = System.Drawing.Color.DarkBlue;
            this.btnDetain.FillColor2 = System.Drawing.Color.BlueViolet;
            this.btnDetain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetain.ForeColor = System.Drawing.Color.White;
            this.btnDetain.Location = new System.Drawing.Point(858, 738);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnDetain.Size = new System.Drawing.Size(87, 48);
            this.btnDetain.TabIndex = 86;
            this.btnDetain.Text = "Detain";
            this.btnDetain.UseTransparentBackground = true;
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.DarkBlue;
            this.btnClose.FillColor2 = System.Drawing.Color.BlueViolet;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(720, 738);
            this.btnClose.Name = "btnClose";
            this.btnClose.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnClose.Size = new System.Drawing.Size(87, 48);
            this.btnClose.TabIndex = 85;
            this.btnClose.Text = "Close";
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucSearchLicense1
            // 
            this.ucSearchLicense1.BackColor = System.Drawing.Color.Transparent;
            this.ucSearchLicense1.Location = new System.Drawing.Point(35, 81);
            this.ucSearchLicense1.Name = "ucSearchLicense1";
            this.ucSearchLicense1.Size = new System.Drawing.Size(940, 461);
            this.ucSearchLicense1.TabIndex = 83;
            this.ucSearchLicense1.LicenseSearched += new System.Action<int>(this.UcSearchLicense1_LicenseSearched);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(248, 29);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(513, 29);
            this.lblTitle.TabIndex = 82;
            this.lblTitle.Text = "Detain License";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ucDetain1
            // 
            this.ucDetain1.BackColor = System.Drawing.Color.Transparent;
            this.ucDetain1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucDetain1.Location = new System.Drawing.Point(52, 577);
            this.ucDetain1.Name = "ucDetain1";
            this.ucDetain1.Size = new System.Drawing.Size(893, 133);
            this.ucDetain1.TabIndex = 89;
            // 
            // frmDetainLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(19)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1011, 809);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblShowNewLicenseInfo);
            this.Controls.Add(this.lblShowLicenseHistory);
            this.Controls.Add(this.btnDetain);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ucSearchLicense1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ucDetain1);
            this.Name = "frmDetainLicense";
            this.Text = "frmDetainLicense";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lblShowNewLicenseInfo;
        private System.Windows.Forms.LinkLabel lblShowLicenseHistory;
        private Guna.UI2.WinForms.Guna2GradientCircleButton btnDetain;
        private Guna.UI2.WinForms.Guna2GradientCircleButton btnClose;
        private ucSearchLicense ucSearchLicense1;
        private System.Windows.Forms.Label lblTitle;
        private ucDetain ucDetain1;
    }
}