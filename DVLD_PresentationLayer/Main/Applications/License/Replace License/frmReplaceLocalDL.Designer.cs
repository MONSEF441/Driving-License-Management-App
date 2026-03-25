namespace DVLD_PresentationAccess
{
    partial class frmReplaceLocalDL
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
            this.btnRenew = new Guna.UI2.WinForms.Guna2GradientCircleButton();
            this.btnClose = new Guna.UI2.WinForms.Guna2GradientCircleButton();
            this.ucSearchLicense1 = new DVLD_PresentationAccess.ucSearchLicense();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ucReplaceLocalDL1 = new DVLD_PresentationAccess.ucReplaceLocalDL();
            this.label1 = new System.Windows.Forms.Label();
            this.rbLost = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbDamaged = new Guna.UI2.WinForms.Guna2RadioButton();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label3.Location = new System.Drawing.Point(86, 620);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(304, 20);
            this.label3.TabIndex = 76;
            this.label3.Text = "Application Info for License Replacement \r\n";
            // 
            // lblShowNewLicenseInfo
            // 
            this.lblShowNewLicenseInfo.AutoSize = true;
            this.lblShowNewLicenseInfo.Enabled = false;
            this.lblShowNewLicenseInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblShowNewLicenseInfo.LinkColor = System.Drawing.Color.MediumSlateBlue;
            this.lblShowNewLicenseInfo.Location = new System.Drawing.Point(280, 796);
            this.lblShowNewLicenseInfo.Name = "lblShowNewLicenseInfo";
            this.lblShowNewLicenseInfo.Size = new System.Drawing.Size(175, 20);
            this.lblShowNewLicenseInfo.TabIndex = 80;
            this.lblShowNewLicenseInfo.TabStop = true;
            this.lblShowNewLicenseInfo.Text = "Show New License Info";
            this.lblShowNewLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowNewLicenseInfo_LinkClicked);
            // 
            // lblShowLicenseHistory
            // 
            this.lblShowLicenseHistory.AutoSize = true;
            this.lblShowLicenseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblShowLicenseHistory.LinkColor = System.Drawing.Color.MediumSlateBlue;
            this.lblShowLicenseHistory.Location = new System.Drawing.Point(60, 796);
            this.lblShowLicenseHistory.Name = "lblShowLicenseHistory";
            this.lblShowLicenseHistory.Size = new System.Drawing.Size(169, 20);
            this.lblShowLicenseHistory.TabIndex = 79;
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
            this.btnRenew.Location = new System.Drawing.Point(828, 796);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnRenew.Size = new System.Drawing.Size(126, 48);
            this.btnRenew.TabIndex = 78;
            this.btnRenew.Text = "Replace";
            this.btnRenew.UseTransparentBackground = true;
            this.btnRenew.Click += new System.EventHandler(this.btnReplace_Click);
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
            this.btnClose.Location = new System.Drawing.Point(674, 796);
            this.btnClose.Name = "btnClose";
            this.btnClose.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnClose.Size = new System.Drawing.Size(126, 48);
            this.btnClose.TabIndex = 77;
            this.btnClose.Text = "Close";
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucSearchLicense1
            // 
            this.ucSearchLicense1.BackColor = System.Drawing.Color.Transparent;
            this.ucSearchLicense1.Location = new System.Drawing.Point(35, 81);
            this.ucSearchLicense1.Name = "ucSearchLicense1";
            this.ucSearchLicense1.Size = new System.Drawing.Size(933, 461);
            this.ucSearchLicense1.TabIndex = 75;
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
            this.lblTitle.TabIndex = 74;
            this.lblTitle.Text = "Replace Damaged / Lost License";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ucReplaceLocalDL1
            // 
            this.ucReplaceLocalDL1.BackColor = System.Drawing.Color.Transparent;
            this.ucReplaceLocalDL1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucReplaceLocalDL1.Location = new System.Drawing.Point(60, 632);
            this.ucReplaceLocalDL1.Name = "ucReplaceLocalDL1";
            this.ucReplaceLocalDL1.Size = new System.Drawing.Size(894, 133);
            this.ucReplaceLocalDL1.TabIndex = 81;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label1.Location = new System.Drawing.Point(88, 570);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 82;
            this.label1.Text = "Replacement For :";
            // 
            // rbLost
            // 
            this.rbLost.AutoSize = true;
            this.rbLost.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbLost.CheckedState.BorderThickness = 0;
            this.rbLost.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.rbLost.CheckedState.InnerColor = System.Drawing.Color.MediumSlateBlue;
            this.rbLost.CheckedState.InnerOffset = -4;
            this.rbLost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rbLost.ForeColor = System.Drawing.Color.White;
            this.rbLost.Location = new System.Drawing.Point(408, 570);
            this.rbLost.Name = "rbLost";
            this.rbLost.Size = new System.Drawing.Size(106, 21);
            this.rbLost.TabIndex = 83;
            this.rbLost.Text = "Lost License";
            this.rbLost.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rbLost.UncheckedState.BorderThickness = 2;
            this.rbLost.UncheckedState.FillColor = System.Drawing.Color.White;
            this.rbLost.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.rbLost.CheckedChanged += new System.EventHandler(this.ReplacementOptionChanged);
            // 
            // rbDamaged
            // 
            this.rbDamaged.AutoSize = true;
            this.rbDamaged.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbDamaged.CheckedState.BorderThickness = 0;
            this.rbDamaged.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.rbDamaged.CheckedState.InnerColor = System.Drawing.Color.MediumSlateBlue;
            this.rbDamaged.CheckedState.InnerOffset = -4;
            this.rbDamaged.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rbDamaged.ForeColor = System.Drawing.Color.White;
            this.rbDamaged.Location = new System.Drawing.Point(250, 570);
            this.rbDamaged.Name = "rbDamaged";
            this.rbDamaged.Size = new System.Drawing.Size(140, 21);
            this.rbDamaged.TabIndex = 84;
            this.rbDamaged.Text = "Damaged License";
            this.rbDamaged.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rbDamaged.UncheckedState.BorderThickness = 2;
            this.rbDamaged.UncheckedState.FillColor = System.Drawing.Color.White;
            this.rbDamaged.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.rbDamaged.CheckedChanged += new System.EventHandler(this.ReplacementOptionChanged);
            // 
            // frmReplaceLocalDL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(19)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1007, 860);
            this.Controls.Add(this.rbDamaged);
            this.Controls.Add(this.rbLost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblShowNewLicenseInfo);
            this.Controls.Add(this.lblShowLicenseHistory);
            this.Controls.Add(this.btnRenew);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ucSearchLicense1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ucReplaceLocalDL1);
            this.Name = "frmReplaceLocalDL";
            this.Text = "frmReplacementLocalDL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel lblShowNewLicenseInfo;
        private System.Windows.Forms.LinkLabel lblShowLicenseHistory;
        private Guna.UI2.WinForms.Guna2GradientCircleButton btnRenew;
        private Guna.UI2.WinForms.Guna2GradientCircleButton btnClose;
        private ucSearchLicense ucSearchLicense1;
        private System.Windows.Forms.Label lblTitle;
        private ucReplaceLocalDL ucReplaceLocalDL1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2RadioButton rbLost;
        private Guna.UI2.WinForms.Guna2RadioButton rbDamaged;
    }
}