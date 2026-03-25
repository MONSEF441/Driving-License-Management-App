using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class frmDetainLicense : Form
    {
        private int _selectedLicenseId = -1;
        private int _selectedPersonId = -1;
        private int _detainId = -1;

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void UcSearchLicense1_LicenseSearched(int licenseId)
        {
            _selectedLicenseId = licenseId;
            _detainId = -1;
            lblShowNewLicenseInfo.Enabled = false;

            clsLicense selectedLicense = clsLicense.Find(licenseId);
            if (selectedLicense == null)
            {
                ResetAfterFailedSearch();
                return;
            }

            clsDriver driver = clsDriver.Find(selectedLicense.DriverID);
            _selectedPersonId = driver?.PersonID ?? -1;

            ucDetain1.LoadData(selectedLicense);

            if (clsDetainedLicense.IsLicenseDetained(licenseId))
            {
                btnDetain.Enabled = false;
                MessageBox.Show("Selected license is already detained.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnDetain.Enabled = true;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (_selectedLicenseId <= 0)
            {
                MessageBox.Show("Please select a license first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ucDetain1.IsFineFeesValid)
            {
                MessageBox.Show("Please enter valid fine fees.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsLicense license = clsLicense.Find(_selectedLicenseId);
            if (license == null)
            {
                MessageBox.Show("Selected license was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsDetainedLicense.IsLicenseDetained(license.LicenseID))
            {
                MessageBox.Show("Selected license is already detained.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnDetain.Enabled = false;
                return;
            }

            clsDetainedLicense detainedLicense = clsDetainedLicense.DetainLicense(
                license.LicenseID,
                ucDetain1.FineFees,
                Session.CurrentUser.UserID);

            if (detainedLicense == null)
            {
                MessageBox.Show("Failed to create detain record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!license.SetDetainedStatus(true))
            {
                clsDetainedLicense.DeleteDetainedLicense(detainedLicense.DetainID);
                MessageBox.Show("Failed to mark the license as detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _detainId = detainedLicense.DetainID;
            ucDetain1.SetDetainResult(_detainId);
            btnDetain.Enabled = false;
            lblShowNewLicenseInfo.Enabled = true;

            MessageBox.Show(
                $"License detained successfully.\nDetain ID = {_detainId}",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void lblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_selectedPersonId <= 0)
            {
                MessageBox.Show("No person selected yet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (frmLicenseHistory frm = new frmLicenseHistory(_selectedPersonId))
            {
                frm.ShowDialog();
            }
        }

        private void lblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_selectedLicenseId <= 0)
            {
                MessageBox.Show("No license selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (frmShowLocalLicense frm = new frmShowLocalLicense(_selectedLicenseId))
            {
                frm.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResetAfterFailedSearch()
        {
            _selectedLicenseId = -1;
            _selectedPersonId = -1;
            _detainId = -1;

            ucDetain1.Reset();
            btnDetain.Enabled = false;
            lblShowNewLicenseInfo.Enabled = false;
        }
    }
}
