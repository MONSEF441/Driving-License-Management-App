using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class frmReleaseLicense : Form
    {
        private const int ReleaseDetainedLicenseApplicationTypeID = 5;

        private int _selectedLicenseId = -1;
        private int _selectedPersonId = -1;
        private int _releaseApplicationId = -1;
        private clsDetainedLicense _activeDetainedLicense = null;

        private readonly int _presetLicenseId = -1;

        public frmReleaseLicense()
        {
            InitializeComponent();

            btnRelease.Enabled = false;
            lblShowNewLicenseInfo.Enabled = false;
        }

        public frmReleaseLicense(int presetLicenseId) : this()
        {
            _presetLicenseId = presetLicenseId;
            Shown += frmReleaseLicense_Shown;
        }

        private void frmReleaseLicense_Shown(object sender, EventArgs e)
        {
            if (_presetLicenseId > 0)
            {
                ucSearchLicense1.LoadAndLockLicense(_presetLicenseId);
                ucSearchLicense1.Enabled = false;
            }
        }

        private void UcSearchLicense1_LicenseSearched(int licenseId)
        {
            _selectedLicenseId = licenseId;
            _releaseApplicationId = -1;
            _activeDetainedLicense = null;
            lblShowNewLicenseInfo.Enabled = false;
            btnRelease.Enabled = false;

            clsLicense selectedLicense = clsLicense.Find(licenseId);
            if (selectedLicense == null)
            {
                ResetAfterFailedSearch();
                MessageBox.Show("License not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDriver driver = clsDriver.Find(selectedLicense.DriverID);
            _selectedPersonId = driver?.PersonID ?? -1;

            _activeDetainedLicense = clsDetainedLicense.FindActiveByLicenseID(licenseId);
            if (_activeDetainedLicense == null)
            {
                MessageBox.Show("Selected license is not detained.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ucRelease1.LoadData(_activeDetainedLicense, ReleaseDetainedLicenseApplicationTypeID);
            btnRelease.Enabled = true;
        }

        private void ResetAfterFailedSearch()
        {
            _selectedLicenseId = -1;
            _selectedPersonId = -1;
            _releaseApplicationId = -1;
            _activeDetainedLicense = null;
            btnRelease.Enabled = false;
            lblShowNewLicenseInfo.Enabled = false;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (_selectedLicenseId <= 0 || _activeDetainedLicense == null)
            {
                MessageBox.Show("Please select a detained license first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsLicense license = clsLicense.Find(_selectedLicenseId);
            if (license == null)
            {
                MessageBox.Show("License not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDriver driver = clsDriver.Find(license.DriverID);
            if (driver == null)
            {
                MessageBox.Show("Driver not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsApplication releaseApplication = new clsApplication
            {
                ApplicationTypeID = ReleaseDetainedLicenseApplicationTypeID,
                ApplicationPersonID = driver.PersonID,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = 3,
                LastStatusDate = DateTime.Now,
                CreatedByUserID = Session.CurrentUser.UserID
            };

            if (!releaseApplication.Save())
            {
                MessageBox.Show("Failed to create release application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_activeDetainedLicense.ReleaseDetainedLicense(Session.CurrentUser.UserID, releaseApplication.ApplicationID))
            {
                clsApplication.DeleteApplication(releaseApplication.ApplicationID);
                MessageBox.Show("Failed to update detained license release data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!license.SetDetainedStatus(false))
            {
                MessageBox.Show("Release application saved, but failed to update license detained flag.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _releaseApplicationId = releaseApplication.ApplicationID;
            ucRelease1.SetReleaseResult(_releaseApplicationId);
            btnRelease.Enabled = false;
            lblShowNewLicenseInfo.Enabled = true;

            MessageBox.Show(
                $"License released successfully.\nRelease Application ID = {_releaseApplicationId}",
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
    }
}
