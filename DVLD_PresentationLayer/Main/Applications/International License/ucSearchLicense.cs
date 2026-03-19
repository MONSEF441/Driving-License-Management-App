using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications.International_License
{
    public partial class ucSearchLicense : UserControl
    {
        public event Action<int> LicenseSearched;

        public ucSearchLicense()
        {
            InitializeComponent();
            tbLicenseID.KeyPress += tbLicenseID_KeyPress;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tbLicenseID.Text.Trim(), out int licenseId) || licenseId <= 0)
            {
                MessageBox.Show("Please enter a valid numeric License ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbLicenseID.Focus();
                tbLicenseID.SelectAll();
                return;
            }

            clsLicense license = clsLicense.Find(licenseId);
            if (license == null)
            {
                MessageBox.Show("License not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ucLicenseInfo1.LoadProfile(licenseId);
            LicenseSearched?.Invoke(licenseId);
        }

        private void tbLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
