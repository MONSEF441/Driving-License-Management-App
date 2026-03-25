using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class frmShowLocalLicense : Form
    {
        private int _licenseId;

        public frmShowLocalLicense(int licenseId)
        {
            _licenseId = licenseId;

            InitializeComponent();

            ucLicenseInfo1.LoadProfile(_licenseId);
            label1.Text = $"Driving License Info : #{_licenseId}";
            
         }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
