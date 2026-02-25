namespace DVLD_PresentationAccess.Main.Users
{
    partial class ucProfile
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pcLoginInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.ucUserCard1 = new DVLD_PresentationAccess.Editors.ucUserCard();
            this.pcPersonInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.ucPersonCard1 = new DVLD_PresentationAccess.ucPersonCard();
            this.pcLoginInfo.SuspendLayout();
            this.pcPersonInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(61, 407);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 24);
            this.label3.TabIndex = 44;
            this.label3.Text = "Login Information :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(61, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 24);
            this.label2.TabIndex = 43;
            this.label2.Text = "Person Information :";
            // 
            // pcLoginInfo
            // 
            this.pcLoginInfo.BackColor = System.Drawing.Color.Transparent;
            this.pcLoginInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pcLoginInfo.Controls.Add(this.ucUserCard1);
            this.pcLoginInfo.Location = new System.Drawing.Point(49, 421);
            this.pcLoginInfo.Name = "pcLoginInfo";
            this.pcLoginInfo.Size = new System.Drawing.Size(872, 153);
            this.pcLoginInfo.TabIndex = 46;
            // 
            // ucUserCard1
            // 
            this.ucUserCard1.BackColor = System.Drawing.Color.Transparent;
            this.ucUserCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucUserCard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUserCard1.Location = new System.Drawing.Point(0, 0);
            this.ucUserCard1.Name = "ucUserCard1";
            this.ucUserCard1.Size = new System.Drawing.Size(872, 153);
            this.ucUserCard1.TabIndex = 0;
            // 
            // pcPersonInfo
            // 
            this.pcPersonInfo.BackColor = System.Drawing.Color.Transparent;
            this.pcPersonInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pcPersonInfo.Controls.Add(this.ucPersonCard1);
            this.pcPersonInfo.Location = new System.Drawing.Point(49, 46);
            this.pcPersonInfo.Name = "pcPersonInfo";
            this.pcPersonInfo.Size = new System.Drawing.Size(872, 325);
            this.pcPersonInfo.TabIndex = 45;
            // 
            // ucPersonCard1
            // 
            this.ucPersonCard1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ucPersonCard1.BackColor = System.Drawing.Color.Transparent;
            this.ucPersonCard1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ucPersonCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPersonCard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPersonCard1.Location = new System.Drawing.Point(0, 0);
            this.ucPersonCard1.Name = "ucPersonCard1";
            this.ucPersonCard1.Size = new System.Drawing.Size(872, 325);
            this.ucPersonCard1.TabIndex = 0;
            // 
            // ucProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pcLoginInfo);
            this.Controls.Add(this.pcPersonInfo);
            this.Name = "ucProfile";
            this.Size = new System.Drawing.Size(981, 625);
            this.pcLoginInfo.ResumeLayout(false);
            this.pcPersonInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Panel pcLoginInfo;
        private Editors.ucUserCard ucUserCard1;
        private Guna.UI2.WinForms.Guna2Panel pcPersonInfo;
        private ucPersonCard ucPersonCard1;
    }
}
