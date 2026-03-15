using DVLD_BusinessAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications
{
    public partial class frmEditApplicationTypes : Form
    {
    
        private clsApplicationType _ApplicationType;

        public event Action<clsApplicationType> ApplicationTypeSaved;
        public frmEditApplicationTypes(clsApplicationType ApplicationType)
        {
            InitializeComponent();
            _ApplicationType = ApplicationType;
        }

        private void frmEditApplicationTypes_Load(object sender, EventArgs e)
        {
            LoadApplicationType();
        }
    
        public void LoadApplicationType()
        {

            if (_ApplicationType != null)
            {
                lblID.Text = _ApplicationType.ApplicationTypeID.ToString();
                tbTitle.Text = _ApplicationType.ApplicationTypeTitle.ToString();
                tbFees.Text =  _ApplicationType.ApplicationTypeFees.ToString();

            }
        }
        
        private bool ValidateFields()
        {
            bool valid = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                errorProvider.SetError(tbTitle, "Title is required.");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(tbTitle.Text))
            {
                errorProvider.SetError(tbTitle, "Fees shouldn't be null .");
                valid = false;
            }

            return valid;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            _ApplicationType.ApplicationTypeTitle = tbTitle.Text;
            _ApplicationType.ApplicationTypeFees = Convert.ToDecimal(tbFees.Text);

            if (_ApplicationType.Save())
            {
                ApplicationTypeSaved?.Invoke(_ApplicationType);

                MessageBox.Show("_ApplicationType Updated Successfully !!", "Update",MessageBoxButtons.OK);
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

     
    }

}
