namespace DVLD_PresentationAccess.Main.Users
{
    partial class ucSearchPerson
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSearchPerson));
            this.btnSearch = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnAdd = new Guna.UI2.WinForms.Guna2ImageButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ucPersonCard = new DVLD_PresentationAccess.ucPersonCard();
            this.ucEntityFilter = new DVLD_PresentationAccess.Main.Managers.ucEntityFilter();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSearch.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnSearch.ImageRotate = 0F;
            this.btnSearch.ImageSize = new System.Drawing.Size(32, 32);
            this.btnSearch.Location = new System.Drawing.Point(608, 37);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSearch.Size = new System.Drawing.Size(42, 40);
            this.btnSearch.TabIndex = 54;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnAdd.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnAdd.ImageRotate = 0F;
            this.btnAdd.ImageSize = new System.Drawing.Size(32, 32);
            this.btnAdd.Location = new System.Drawing.Point(701, 41);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnAdd.Size = new System.Drawing.Size(42, 36);
            this.btnAdd.TabIndex = 53;
            this.btnAdd.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(44, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 24);
            this.label6.TabIndex = 56;
            this.label6.Text = "Filter :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(44, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 24);
            this.label5.TabIndex = 55;
            this.label5.Text = "Person Information :";
            // 
            // ucPersonCard
            // 
            this.ucPersonCard.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ucPersonCard.BackColor = System.Drawing.Color.Transparent;
            this.ucPersonCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ucPersonCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPersonCard.Location = new System.Drawing.Point(29, 148);
            this.ucPersonCard.Name = "ucPersonCard";
            this.ucPersonCard.Size = new System.Drawing.Size(878, 300);
            this.ucPersonCard.TabIndex = 58;
            // 
            // ucEntityFilter
            // 
            this.ucEntityFilter.BackColor = System.Drawing.Color.Transparent;
            this.ucEntityFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucEntityFilter.Location = new System.Drawing.Point(29, 25);
            this.ucEntityFilter.Name = "ucEntityFilter";
            this.ucEntityFilter.Size = new System.Drawing.Size(878, 64);
            this.ucEntityFilter.TabIndex = 59;
            // 
            // ucSearchPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ucPersonCard);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.ucEntityFilter);
            this.Name = "ucSearchPerson";
            this.Size = new System.Drawing.Size(932, 515);
            this.Load += new System.EventHandler(this.ucSearchPerson_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private Guna.UI2.WinForms.Guna2ImageButton btnSearch;
        private Guna.UI2.WinForms.Guna2ImageButton btnAdd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private ucPersonCard ucPersonCard;
        private Managers.ucEntityFilter ucEntityFilter;
    }
}
