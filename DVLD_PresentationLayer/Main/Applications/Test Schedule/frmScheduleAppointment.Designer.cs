namespace DVLD_PresentationAccess.Main.Applications
{
    partial class frmScheduleAppointment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScheduleAppointment));
            this.lblAppointmentTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbAppointmentPicture = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.dgvAppointments = new Guna.UI2.WinForms.Guna2DataGridView();
            this.cmAppointment = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.cmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmTakeTest = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddAppointment = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnClose = new Guna.UI2.WinForms.Guna2GradientCircleButton();
            this.ucDLApplicationInfo1 = new DVLD_PresentationAccess.Main.Applications.ucDLApplicationInfo();
            this.ucApplicationBasicInfo1 = new DVLD_PresentationAccess.Main.Applications.ucApplicationBasicInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pbAppointmentPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.cmAppointment.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAppointmentTitle
            // 
            this.lblAppointmentTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAppointmentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppointmentTitle.ForeColor = System.Drawing.Color.Transparent;
            this.lblAppointmentTitle.Location = new System.Drawing.Point(51, 150);
            this.lblAppointmentTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAppointmentTitle.Name = "lblAppointmentTitle";
            this.lblAppointmentTitle.Size = new System.Drawing.Size(795, 42);
            this.lblAppointmentTitle.TabIndex = 3;
            this.lblAppointmentTitle.Text = "Test Title";
            this.lblAppointmentTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(70, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 20);
            this.label2.TabIndex = 48;
            this.label2.Text = "Application Basic Info :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(70, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "Driving License Application Info :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(70, 696);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 20);
            this.label3.TabIndex = 51;
            this.label3.Text = "Appointments :";
            // 
            // pbAppointmentPicture
            // 
            this.pbAppointmentPicture.BackColor = System.Drawing.Color.Transparent;
            this.pbAppointmentPicture.ImageRotate = 0F;
            this.pbAppointmentPicture.Location = new System.Drawing.Point(386, 41);
            this.pbAppointmentPicture.Margin = new System.Windows.Forms.Padding(2);
            this.pbAppointmentPicture.Name = "pbAppointmentPicture";
            this.pbAppointmentPicture.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.pbAppointmentPicture.Size = new System.Drawing.Size(103, 93);
            this.pbAppointmentPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAppointmentPicture.TabIndex = 2;
            this.pbAppointmentPicture.TabStop = false;
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            this.dgvAppointments.AllowUserToResizeColumns = false;
            this.dgvAppointments.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvAppointments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAppointments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(19)))), ((int)(((byte)(60)))));
            this.dgvAppointments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvAppointments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAppointments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAppointments.ColumnHeadersHeight = 4;
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvAppointments.ContextMenuStrip = this.cmAppointment;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAppointments.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAppointments.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvAppointments.GridColor = System.Drawing.Color.MediumSlateBlue;
            this.dgvAppointments.Location = new System.Drawing.Point(51, 734);
            this.dgvAppointments.MultiSelect = false;
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.RowHeadersVisible = false;
            this.dgvAppointments.Size = new System.Drawing.Size(795, 161);
            this.dgvAppointments.TabIndex = 52;
            this.dgvAppointments.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvAppointments.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvAppointments.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvAppointments.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvAppointments.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvAppointments.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(19)))), ((int)(((byte)(60)))));
            this.dgvAppointments.ThemeStyle.GridColor = System.Drawing.Color.MediumSlateBlue;
            this.dgvAppointments.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvAppointments.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvAppointments.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAppointments.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvAppointments.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvAppointments.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvAppointments.ThemeStyle.ReadOnly = true;
            this.dgvAppointments.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvAppointments.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAppointments.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAppointments.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvAppointments.ThemeStyle.RowsStyle.Height = 22;
            this.dgvAppointments.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvAppointments.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // cmAppointment
            // 
            this.cmAppointment.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmEdit,
            this.cmTakeTest});
            this.cmAppointment.Name = "cmAppointment";
            this.cmAppointment.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.cmAppointment.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.cmAppointment.RenderStyle.ColorTable = null;
            this.cmAppointment.RenderStyle.RoundedEdges = true;
            this.cmAppointment.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.cmAppointment.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmAppointment.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.cmAppointment.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.cmAppointment.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.cmAppointment.Size = new System.Drawing.Size(123, 48);
            // 
            // cmEdit
            // 
            this.cmEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmEdit.Image")));
            this.cmEdit.Name = "cmEdit";
            this.cmEdit.Size = new System.Drawing.Size(122, 22);
            this.cmEdit.Text = "Edit";
            this.cmEdit.Click += new System.EventHandler(this.cmEdit_Click);
            // 
            // cmTakeTest
            // 
            this.cmTakeTest.Image = ((System.Drawing.Image)(resources.GetObject("cmTakeTest.Image")));
            this.cmTakeTest.Name = "cmTakeTest";
            this.cmTakeTest.Size = new System.Drawing.Size(122, 22);
            this.cmTakeTest.Text = "Take Test";
            this.cmTakeTest.Click += new System.EventHandler(this.cmTakeTest_Click);
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddAppointment.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnAddAppointment.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnAddAppointment.Image = ((System.Drawing.Image)(resources.GetObject("btnAddAppointment.Image")));
            this.btnAddAppointment.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnAddAppointment.ImageRotate = 0F;
            this.btnAddAppointment.ImageSize = new System.Drawing.Size(32, 32);
            this.btnAddAppointment.Location = new System.Drawing.Point(799, 685);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnAddAppointment.Size = new System.Drawing.Size(47, 45);
            this.btnAddAppointment.TabIndex = 53;
            this.btnAddAppointment.Click += new System.EventHandler(this.btnAddAppointment_Click);
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
            this.btnClose.Location = new System.Drawing.Point(759, 908);
            this.btnClose.Name = "btnClose";
            this.btnClose.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnClose.Size = new System.Drawing.Size(87, 48);
            this.btnClose.TabIndex = 54;
            this.btnClose.Text = "Close";
            this.btnClose.UseTransparentBackground = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucDLApplicationInfo1
            // 
            this.ucDLApplicationInfo1.BackColor = System.Drawing.Color.Transparent;
            this.ucDLApplicationInfo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucDLApplicationInfo1.Location = new System.Drawing.Point(51, 226);
            this.ucDLApplicationInfo1.Name = "ucDLApplicationInfo1";
            this.ucDLApplicationInfo1.Size = new System.Drawing.Size(795, 142);
            this.ucDLApplicationInfo1.TabIndex = 55;
            // 
            // ucApplicationBasicInfo1
            // 
            this.ucApplicationBasicInfo1.BackColor = System.Drawing.Color.Transparent;
            this.ucApplicationBasicInfo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucApplicationBasicInfo1.Location = new System.Drawing.Point(51, 405);
            this.ucApplicationBasicInfo1.Name = "ucApplicationBasicInfo1";
            this.ucApplicationBasicInfo1.Size = new System.Drawing.Size(795, 277);
            this.ucApplicationBasicInfo1.TabIndex = 56;
            // 
            // frmScheduleAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(19)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(889, 968);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucApplicationBasicInfo1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucDLApplicationInfo1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddAppointment);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblAppointmentTitle);
            this.Controls.Add(this.pbAppointmentPicture);
            this.Name = "frmScheduleAppointment";
            this.Text = "frmScheduleTest";
            ((System.ComponentModel.ISupportInitialize)(this.pbAppointmentPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.cmAppointment.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAppointmentTitle;
        private Guna.UI2.WinForms.Guna2CirclePictureBox pbAppointmentPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2DataGridView dgvAppointments;
        private Guna.UI2.WinForms.Guna2ImageButton btnAddAppointment;
        private Guna.UI2.WinForms.Guna2GradientCircleButton btnClose;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip cmAppointment;
        private System.Windows.Forms.ToolStripMenuItem cmEdit;
        private System.Windows.Forms.ToolStripMenuItem cmTakeTest;
        private ucDLApplicationInfo ucDLApplicationInfo1;
        private ucApplicationBasicInfo ucApplicationBasicInfo1;
    }
}