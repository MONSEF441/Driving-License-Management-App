using DVLD_BusinessAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications
{
    public partial class frmEditTestTypes : Form
    {
        private clsTestType _TestType;
        public event Action<clsTestType> TestTypeSaved;

        public frmEditTestTypes(clsTestType TestType)
        {
            InitializeComponent();
            _TestType = TestType;
        }

        private void frmEditTestTypes_Load(object sender, EventArgs e)
        {
            LoadTestType();
        }
   
        public void LoadTestType()
        {

            if (_TestType != null)
            {
                lblID.Text = _TestType.TestTypeID.ToString();
                tbTitle.Text = _TestType.TestTypeTitle.ToString();
                tbDescription.Text = _TestType.TestTypeDescription.ToString();
                tbFees.Text = _TestType.TestTypeFees.ToString();

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
                errorProvider.SetError(tbTitle, "Description of Test Type is required.");
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

            _TestType.TestTypeTitle = tbTitle.Text;
            _TestType.TestTypeDescription = tbTitle.Text;
            _TestType.TestTypeFees = Convert.ToDecimal(tbFees.Text);

            if (_TestType.Save())
            {
                TestTypeSaved?.Invoke(_TestType);

                MessageBox.Show("_ApplicationType Updated Successfully !!", "Update", MessageBoxButtons.OK);
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    }

}
