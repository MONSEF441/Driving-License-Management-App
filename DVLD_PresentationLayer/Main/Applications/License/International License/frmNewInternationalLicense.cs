using DVLD_BusinessAccess;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class frmNewInternationalLicense : Form
    {
        private int _selectedLocalLicenseId = -1;
        private int _selectedPersonId = -1;
        private int _internationalLicenseId = -1;

        private readonly int _lockedLocalLicenseId;
        private readonly bool _lockSearchControl;

        public frmNewInternationalLicense() : this(-1, false)
        {
        }

        public frmNewInternationalLicense(int localLicenseId, bool lockSearchControl)
        {
            _lockedLocalLicenseId = localLicenseId;
            _lockSearchControl = lockSearchControl;

            InitializeComponent();

            btnIssue.Enabled = false;
            lblShowLicenseInfo.Enabled = false;

            ucSearchLicense.LicenseSearched += UcSearchLicense1_LicenseSearched;

            if (_lockSearchControl && _lockedLocalLicenseId > 0)
                ucSearchLicense.LoadAndLockLicense(_lockedLocalLicenseId);
        }

        private void UcSearchLicense1_LicenseSearched(int localLicenseId)
        {
            _selectedLocalLicenseId = localLicenseId;
            _internationalLicenseId = -1;

            clsLicense localLicense = clsLicense.Find(localLicenseId);
            if (localLicense == null)
            {
                btnIssue.Enabled = false;
                lblShowLicenseInfo.Enabled = false;
                return;
            }

            if (!localLicense.IsActive)
            {
                btnIssue.Enabled = false;
                lblShowLicenseInfo.Enabled = false;
                MessageBox.Show(
                    "You cannot issue an international license using an inactive local license.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            clsDriver driver = clsDriver.Find(localLicense.DriverID);
            _selectedPersonId = driver?.PersonID ?? -1;

            if (driver == null)
            {
                btnIssue.Enabled = false;
                lblShowLicenseInfo.Enabled = false;
                return;
            }

            var activeInternational = clsInternationalLicense.FindActiveByDriverID(driver.DriverID);
            if (activeInternational != null)
            {
                _internationalLicenseId = activeInternational.InternationalLicenseID;
                lblShowLicenseInfo.Enabled = true;

                MessageBox.Show(
                    $"Driver already has an active international license (ID = {_internationalLicenseId}).\n" +
                    $"It will be deactivated automatically when issuing the new one.",
                    "Active License Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                lblShowLicenseInfo.Enabled = false;
            }

            btnIssue.Enabled = true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (_selectedLocalLicenseId <= 0)
            {
                MessageBox.Show("Please search and select a valid local license first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsLicense localLicense = clsLicense.Find(_selectedLocalLicenseId);
            if (localLicense == null)
            {
                MessageBox.Show("Selected local license was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!localLicense.IsActive)
            {
                MessageBox.Show("You cannot issue an international license using an inactive local license.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clsDriver driver = clsDriver.Find(localLicense.DriverID);
            if (driver == null)
            {
                MessageBox.Show("Driver record was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Deactivate currently active international license (if any), then issue a new one.
            var activeInternational = clsInternationalLicense.FindActiveByDriverID(driver.DriverID);
            if (activeInternational != null)
            {
                if (!activeInternational.Deactivate())
                {
                    MessageBox.Show("Failed to deactivate the existing active international license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            clsApplicationType appType = clsApplicationType.FindInternationalApplicationType();
            if (appType == null)
            {
                MessageBox.Show("International application type is not configured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsApplication app = new clsApplication
            {
                ApplicationTypeID = appType.ApplicationTypeID,
                ApplicationPersonID = driver.PersonID,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = 3,
                LastStatusDate = DateTime.Now,
                PaidFees = appType.ApplicationTypeFees,
                CreatedByUserID = Session.CurrentUser.UserID
            };

            if (!app.Save())
            {
                MessageBox.Show("Failed to create international application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsInternationalLicense international = new clsInternationalLicense
            {
                ApplicationID = app.ApplicationID,
                DriverID = driver.DriverID,
                IssuedUsingLocalLicenseID = localLicense.LicenseID,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(1),
                Notes = true, // active
                CreatedByUserID = Session.CurrentUser.UserID
            };

            if (!international.Save())
            {
                MessageBox.Show("Failed to issue international license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _internationalLicenseId = international.InternationalLicenseID;
            _selectedPersonId = driver.PersonID;

            ucInternationalApplicationInfo1.LoadInternationalApplicationInfo(app, international, localLicense, appType);

            btnIssue.Enabled = false;
            lblShowLicenseInfo.Enabled = true;

            MessageBox.Show(
                $"International License issued successfully.\nInternational License ID = {_internationalLicenseId}",
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

            using var frm = new frmLicenseHistory(_selectedPersonId);
            frm.ShowDialog();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_internationalLicenseId <= 0)
            {
                MessageBox.Show("No international license available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var frm = new frmShowInternationalLicense(_internationalLicenseId);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

      
    }
}
