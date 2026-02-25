namespace DVLD_PresentationAccess.Managers
{
    partial class ucEntityManager
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucEntityManager));
            this.ucManageTitle = new System.Windows.Forms.Label();
            this.ucManagePicture = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.dgvDVLD_Table = new Guna.UI2.WinForms.Guna2DataGridView();
            this.cmTable = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            this.cmShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.cmAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCall = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd = new Guna.UI2.WinForms.Guna2ImageButton();
            this.pcFilter = new Guna.UI2.WinForms.Guna2Panel();
            this.ucEntityFilter1 = new DVLD_PresentationAccess.Main.Managers.ucEntityFilter();
            this.guna2ContainerControl1 = new Guna.UI2.WinForms.Guna2ContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.ucManagePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDVLD_Table)).BeginInit();
            this.cmTable.SuspendLayout();
            this.pcFilter.SuspendLayout();
            this.guna2ContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucManageTitle
            // 
            this.ucManageTitle.AutoSize = true;
            this.ucManageTitle.BackColor = System.Drawing.Color.Transparent;
            this.ucManageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucManageTitle.ForeColor = System.Drawing.Color.Transparent;
            this.ucManageTitle.Location = new System.Drawing.Point(618, 167);
            this.ucManageTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ucManageTitle.Name = "ucManageTitle";
            this.ucManageTitle.Size = new System.Drawing.Size(213, 37);
            this.ucManageTitle.TabIndex = 1;
            this.ucManageTitle.Text = "Manage Title";
            // 
            // ucManagePicture
            // 
            this.ucManagePicture.BackColor = System.Drawing.Color.Transparent;
            this.ucManagePicture.ImageRotate = 0F;
            this.ucManagePicture.Location = new System.Drawing.Point(677, 67);
            this.ucManagePicture.Margin = new System.Windows.Forms.Padding(2);
            this.ucManagePicture.Name = "ucManagePicture";
            this.ucManagePicture.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.ucManagePicture.Size = new System.Drawing.Size(87, 81);
            this.ucManagePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ucManagePicture.TabIndex = 0;
            this.ucManagePicture.TabStop = false;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // dgvDVLD_Table
            // 
            this.dgvDVLD_Table.AllowDrop = true;
            this.dgvDVLD_Table.AllowUserToAddRows = false;
            this.dgvDVLD_Table.AllowUserToDeleteRows = false;
            this.dgvDVLD_Table.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDVLD_Table.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDVLD_Table.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDVLD_Table.BackgroundColor = System.Drawing.Color.SlateBlue;
            this.dgvDVLD_Table.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvDVLD_Table.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDVLD_Table.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDVLD_Table.ColumnHeadersHeight = 35;
            this.dgvDVLD_Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDVLD_Table.ContextMenuStrip = this.cmTable;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDVLD_Table.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDVLD_Table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDVLD_Table.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDVLD_Table.GridColor = System.Drawing.Color.BlueViolet;
            this.dgvDVLD_Table.Location = new System.Drawing.Point(0, 0);
            this.dgvDVLD_Table.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDVLD_Table.MultiSelect = false;
            this.dgvDVLD_Table.Name = "dgvDVLD_Table";
            this.dgvDVLD_Table.ReadOnly = true;
            this.dgvDVLD_Table.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDVLD_Table.RowHeadersVisible = false;
            this.dgvDVLD_Table.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvDVLD_Table.RowTemplate.Height = 20;
            this.dgvDVLD_Table.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvDVLD_Table.Size = new System.Drawing.Size(1028, 219);
            this.dgvDVLD_Table.TabIndex = 6;
            this.dgvDVLD_Table.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDVLD_Table.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDVLD_Table.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDVLD_Table.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDVLD_Table.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDVLD_Table.ThemeStyle.BackColor = System.Drawing.Color.SlateBlue;
            this.dgvDVLD_Table.ThemeStyle.GridColor = System.Drawing.Color.BlueViolet;
            this.dgvDVLD_Table.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvDVLD_Table.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDVLD_Table.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDVLD_Table.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Lime;
            this.dgvDVLD_Table.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDVLD_Table.ThemeStyle.HeaderStyle.Height = 35;
            this.dgvDVLD_Table.ThemeStyle.ReadOnly = true;
            this.dgvDVLD_Table.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDVLD_Table.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDVLD_Table.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDVLD_Table.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvDVLD_Table.ThemeStyle.RowsStyle.Height = 20;
            this.dgvDVLD_Table.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDVLD_Table.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // cmTable
            // 
            this.cmTable.Font = new System.Drawing.Font("Constantia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmShowDetails,
            this.cmAdd,
            this.cmEdit,
            this.cmDelete,
            this.toolStripMenuItem1,
            this.cmSendEmail,
            this.cmCall});
            this.cmTable.Name = "cmTable";
            this.cmTable.RenderStyle.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.cmTable.RenderStyle.BorderColor = System.Drawing.Color.Gainsboro;
            this.cmTable.RenderStyle.ColorTable = null;
            this.cmTable.RenderStyle.RoundedEdges = true;
            this.cmTable.RenderStyle.SelectionArrowColor = System.Drawing.Color.White;
            this.cmTable.RenderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.cmTable.RenderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.cmTable.RenderStyle.SeparatorColor = System.Drawing.Color.Gainsboro;
            this.cmTable.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.cmTable.Size = new System.Drawing.Size(142, 142);
            // 
            // cmShowDetails
            // 
            this.cmShowDetails.Image = ((System.Drawing.Image)(resources.GetObject("cmShowDetails.Image")));
            this.cmShowDetails.Name = "cmShowDetails";
            this.cmShowDetails.Size = new System.Drawing.Size(141, 22);
            this.cmShowDetails.Text = "Show Details";
            this.cmShowDetails.Click += new System.EventHandler(this.cm_Show_Click);
            // 
            // cmAdd
            // 
            this.cmAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmAdd.Image")));
            this.cmAdd.Name = "cmAdd";
            this.cmAdd.Size = new System.Drawing.Size(141, 22);
            this.cmAdd.Text = "Add ";
            this.cmAdd.Click += new System.EventHandler(this.cmAdd_Click);
            // 
            // cmEdit
            // 
            this.cmEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmEdit.Image")));
            this.cmEdit.Name = "cmEdit";
            this.cmEdit.Size = new System.Drawing.Size(141, 22);
            this.cmEdit.Text = "Edit";
            this.cmEdit.Click += new System.EventHandler(this.cm_Edit_Click);
            // 
            // cmDelete
            // 
            this.cmDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmDelete.Image")));
            this.cmDelete.Name = "cmDelete";
            this.cmDelete.Size = new System.Drawing.Size(141, 22);
            this.cmDelete.Text = "Delete";
            this.cmDelete.Click += new System.EventHandler(this.cm_Delete_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 6);
            // 
            // cmSendEmail
            // 
            this.cmSendEmail.Image = ((System.Drawing.Image)(resources.GetObject("cmSendEmail.Image")));
            this.cmSendEmail.Name = "cmSendEmail";
            this.cmSendEmail.Size = new System.Drawing.Size(141, 22);
            this.cmSendEmail.Text = "Send Email";
            // 
            // cmCall
            // 
            this.cmCall.Image = ((System.Drawing.Image)(resources.GetObject("cmCall.Image")));
            this.cmCall.Name = "cmCall";
            this.cmCall.Size = new System.Drawing.Size(141, 22);
            this.cmCall.Text = "Call";
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
            this.btnAdd.Location = new System.Drawing.Point(1200, 285);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnAdd.Size = new System.Drawing.Size(42, 36);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Click += new System.EventHandler(this.cmAdd_Click);
            // 
            // pcFilter
            // 
            this.pcFilter.BackColor = System.Drawing.Color.Transparent;
            this.pcFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pcFilter.Controls.Add(this.ucEntityFilter1);
            this.pcFilter.Location = new System.Drawing.Point(347, 232);
            this.pcFilter.Name = "pcFilter";
            this.pcFilter.Size = new System.Drawing.Size(636, 67);
            this.pcFilter.TabIndex = 9;
            // 
            // ucEntityFilter1
            // 
            this.ucEntityFilter1.BackColor = System.Drawing.Color.Transparent;
            this.ucEntityFilter1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ucEntityFilter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucEntityFilter1.Location = new System.Drawing.Point(0, 0);
            this.ucEntityFilter1.Name = "ucEntityFilter1";
            this.ucEntityFilter1.Size = new System.Drawing.Size(636, 67);
            this.ucEntityFilter1.TabIndex = 10;
            // 
            // guna2ContainerControl1
            // 
            this.guna2ContainerControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("guna2ContainerControl1.BackgroundImage")));
            this.guna2ContainerControl1.Controls.Add(this.dgvDVLD_Table);
            this.guna2ContainerControl1.Location = new System.Drawing.Point(214, 326);
            this.guna2ContainerControl1.Name = "guna2ContainerControl1";
            this.guna2ContainerControl1.Size = new System.Drawing.Size(1028, 219);
            this.guna2ContainerControl1.TabIndex = 10;
            this.guna2ContainerControl1.Text = "guna2ContainerControl1";
            // 
            // ucEntityManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.guna2ContainerControl1);
            this.Controls.Add(this.pcFilter);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.ucManageTitle);
            this.Controls.Add(this.ucManagePicture);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(1407, 671);
            this.Name = "ucEntityManager";
            this.Size = new System.Drawing.Size(1407, 671);
            this.Load += new System.EventHandler(this.ucEntityManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ucManagePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDVLD_Table)).EndInit();
            this.cmTable.ResumeLayout(false);
            this.pcFilter.ResumeLayout(false);
            this.guna2ContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox ucManagePicture;
        private System.Windows.Forms.Label ucManageTitle;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDVLD_Table;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip cmTable;
        private System.Windows.Forms.ToolStripMenuItem cmShowDetails;
        private System.Windows.Forms.ToolStripMenuItem cmAdd;
        private System.Windows.Forms.ToolStripMenuItem cmEdit;
        private System.Windows.Forms.ToolStripMenuItem cmDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cmSendEmail;
        private System.Windows.Forms.ToolStripMenuItem cmCall;
        private Guna.UI2.WinForms.Guna2ImageButton btnAdd;
        private Guna.UI2.WinForms.Guna2Panel pcFilter;
        private Main.Managers.ucEntityFilter ucEntityFilter1;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl1;
    }
}
