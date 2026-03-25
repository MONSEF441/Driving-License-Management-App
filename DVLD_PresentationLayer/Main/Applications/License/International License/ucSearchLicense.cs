using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class ucSearchLicense : UserControl
    {
        public event Action<int> LicenseSearched;

        public ucSearchLicense()
        {
            InitializeComponent();
            tbLicenseID.KeyPress += tbLicenseID_KeyPress;
        }

        public void LoadAndLockLicense(int licenseId)
        {
            if (licenseId <= 0)
                return;

            tbLicenseID.Text = licenseId.ToString();

            if (SearchAndLoadLicense(licenseId, true))
            {
                tbLicenseID.Enabled = false;
                btnSearch.Enabled = false;
            }
        }

        public void UnlockSearch()
        {
            tbLicenseID.Enabled = true;
            btnSearch.Enabled = true;
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

            SearchAndLoadLicense(licenseId, true);
        }

        private bool SearchAndLoadLicense(int licenseId, bool showMessages)
        {
            clsLicense license = clsLicense.Find(licenseId);
            if (license == null)
            {
                if (showMessages)
                    MessageBox.Show("License not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }

            ucLicenseInfo1.LoadProfile(licenseId);
            LicenseSearched?.Invoke(licenseId);
            return true;
        }

        private void tbLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
