namespace DVLD_PresentationAccess
{
    partial class frmRenewLocalDL
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
            this.ucSearchLicense1 = new DVLD_PresentationAccess.ucSearchLicense();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.btnRenew = new Guna.UI2.WinForms.Guna2GradientCircleButton();
            this.btnClose = new Guna.UI2.WinForms.Guna2GradientCircleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblShowNewLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.ucRenewLocalDL1 = new DVLD_PresentationAccess.ucRenewLocalDL();
            this.SuspendLayout();
            // 
            // ucSearchLicense1
            // 
            this.ucSearchLicense1.BackColor = System.Drawing.Color.Transparent;
            this.ucSearchLicense1.Location = new System.Drawing.Point(41, 94);
            this.ucSearchLicense1.Name = "ucSearchLicense1";
            this.ucSearchLicense1.Size = new System.Drawing.Size(940, 461);
            this.ucSearchLicense1.TabIndex = 64;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(254, 42);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(513, 29);
            this.lblTitle.TabIndex = 63;
            this.lblTitle.Text = "Renew License Application ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblShowLicenseHistory
            // 
            this.lblShowLicenseHistory.AutoSize = true;
            this.lblShowLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblShowLicenseHistory.LinkColor = System.Drawing.Color.MediumSlateBlue;
            this.lblShowLicenseHistory.Location = new System.Drawing.Point(92, 862);
            this.lblShowLicenseHistory.Name = "lblShowLicenseHistory";
            this.lblShowLicenseHistory.Size = new System.Drawing.Size(169, 20);
            this.lblShowLicenseHistory.TabIndex = 71;
            this.lblShowLicenseHistory.TabStop = true;
            this.lblShowLicenseHistory.Text = "Show Licenses History";
            this.lblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowLicenseHistory_LinkClicked);
            // 
            // btnRenew
            // 
            this.btnRenew.BackColor = System.Drawing.Color.Transparent;
            this.btnRenew.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRenew.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRenew.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRenew.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRenew.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRenew.FillColor = System.Drawing.Color.DarkBlue;
            this.btnRenew.FillColor2 = System.Drawing.Color.BlueViolet;
            this.btnRenew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRenew.ForeColor = System.Drawing.Color.White;
            this.btnRenew.Location = new System.Drawing.Point(864, 851);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnRenew.Size = new System.Drawing.Size(87, 48);
            this.btnRenew.TabIndex = 70;
            this.btnRenew.Text = "Renew";
            this.btnRenew.UseTransparentBackground = true;
            this.btnRenew.Click += new System.EventHandler(this.btnIssue_Click);
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
            this.btnClose.Location = new System.Drawing.Point(726, 851);
            this.btnClose.Name = "btnClose";
            this.btnClose.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnClose.Size = new System.Drawing.Size(87, 48);
            this.btnClose.TabIndex = 69;
            this.btnClose.Text = "Close";
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label3.Location = new System.Drawing.Point(88, 577);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 20);
            this.label3.TabIndex = 68;
            this.label3.Text = "Application Info :";
            // 
            // lblShowNewLicenseInfo
            // 
            this.lblShowNewLicenseInfo.AutoSize = true;
            this.lblShowNewLicenseInfo.Enabled = false;
            this.lblShowNewLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblShowNewLicenseInfo.LinkColor = System.Drawing.Color.MediumSlateBlue;
            this.lblShowNewLicenseInfo.Location = new System.Drawing.Point(312, 862);
            this.lblShowNewLicenseInfo.Name = "lblShowNewLicenseInfo";
            this.lblShowNewLicenseInfo.Size = new System.Drawing.Size(175, 20);
            this.lblShowNewLicenseInfo.TabIndex = 72;
            this.lblShowNewLicenseInfo.TabStop = true;
            this.lblShowNewLicenseInfo.Text = "Show New License Info";
            this.lblShowNewLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowNewLicenseInfo_LinkClicked);
            // 
            // ucRenewLocalDL1
            // 
            this.ucRenewLocalDL1.BackColor = System.Drawing.Color.Transparent;
            this.ucRenewLocalDL1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucRenewLocalDL1.Enabled = false;
            this.ucRenewLocalDL1.Location = new System.Drawing.Point(59, 589);
            this.ucRenewLocalDL1.Name = "ucRenewLocalDL1";
            this.ucRenewLocalDL1.Size = new System.Drawing.Size(897, 242);
            this.ucRenewLocalDL1.TabIndex = 73;
            // 
            // frmRenewLocalDL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(19)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1011, 915);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ucRenewLocalDL1);
            this.Controls.Add(this.lblShowNewLicenseInfo);
            this.Controls.Add(this.lblShowLicenseHistory);
            this.Controls.Add(this.btnRenew);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ucSearchLicense1);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmRenewLocalDL";
            this.Text = "frmRenewLocalDL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucSearchLicense ucSearchLicense1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel lblShowLicenseHistory;
        private Guna.UI2.WinForms.Guna2GradientCircleButton btnRenew;
        private Guna.UI2.WinForms.Guna2GradientCircleButton btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lblShowNewLicenseInfo;
        private ucRenewLocalDL ucRenewLocalDL1;
    }
}