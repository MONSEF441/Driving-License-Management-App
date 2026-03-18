using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications.License
{
    public partial class frmShowLicense : Form
    {
        private int _licenseId;

        public frmShowLicense(int licenseId)
        {
            InitializeComponent();
            LoadProfile(licenseId);
        }

        public void LoadProfile(int licenseId)
        {
            _licenseId = licenseId;

            if (_licenseId <= 0)
            {
                MessageBox.Show("Invalid license ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ucLicenseInfo1.LoadProfile(_licenseId);
            label1.Text = $"Driving License Info : #{_licenseId}";
        }
    }
}
