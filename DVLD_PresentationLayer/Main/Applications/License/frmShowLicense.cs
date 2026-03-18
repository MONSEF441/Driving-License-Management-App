using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications.License
{
    public partial class frmShowLicense : Form
    {
        private int _licenseId;

        public frmShowLicense(int licenseId)
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
