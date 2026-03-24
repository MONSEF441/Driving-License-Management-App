using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications.License
{
    public partial class frmRenewLocalDL : Form
    {
        private int _selectedLicenseId = -1;
        private int _selectedPersonId = -1;
        private int _newLicenseId = -1;

        private event Action RenewAttemptFinished;

        public frmRenewLocalDL()
        {
            InitializeComponent();

            ucSearchLicense1.LicenseSearched += UcSearchLicense1_LicenseSearched;

            RenewAttemptFinished += () =>
            {
                btnRenew.Enabled = false;
                ucRenewLocalDL1.Enabled = false;
            };
        }

        private void UcSearchLicense1_LicenseSearched(int licenseId)
        {
            _selectedLicenseId = licenseId;
            _newLicenseId = -1;
            lblShowNewLicenseInfo.Enabled = false;

            clsLicense oldLicense = clsLicense.Find(licenseId);
            if (oldLicense == null)
            {
                RenewAttemptFinished?.Invoke();
                return;
            }

            clsDriver driver = clsDriver.Find(oldLicense.DriverID);
            _selectedPersonId = driver?.PersonID ?? -1;

            ucRenewLocalDL1.LoadData(oldLicense);

            if (!oldLicense.CanBeRenewed(out string reason))
            {
                RenewAttemptFinished?.Invoke();
                MessageBox.Show(reason, "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnRenew.Enabled = true;
            ucRenewLocalDL1.Enabled = true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (!TryBuildRenewContext(out clsLicense oldLicense, out clsDriver driver, out clsApplicationType appType, out clsLicenseClass licenseClass))
                return;

            if (!TryCreateRenewApplication(driver, appType, out clsApplication renewApp))
                return;

            if (!TryDeactivateOldLicense(oldLicense))
                return;

            if (!TryCreateRenewedLicense(oldLicense, renewApp, licenseClass, out clsLicense newLicense))
                return;

            CompleteRenew(driver, renewApp, newLicense);
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

        private void lblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_newLicenseId <= 0)
            {
                MessageBox.Show("No renewed license available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var frm = new frmShowLocalLicense(_newLicenseId);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool TryBuildRenewContext(out clsLicense oldLicense, out clsDriver driver, out clsApplicationType appType, out clsLicenseClass licenseClass)
        {
            oldLicense = null;
            driver = null;
            appType = null;
            licenseClass = null;

            if (_selectedLicenseId <= 0)
                return Fail("Please select a license first.", "Validation", MessageBoxIcon.Warning);

            oldLicense = clsLicense.Find(_selectedLicenseId);
            if (oldLicense == null)
                return Fail("License not found.", "Error");

            if (!oldLicense.CanBeRenewed(out string reason))
                return Fail(reason, "Not Allowed", MessageBoxIcon.Warning);

            driver = clsDriver.Find(oldLicense.DriverID);
            if (driver == null)
                return Fail("Driver not found.", "Error");

            clsApplication oldApplication = clsApplication.Find(oldLicense.ApplicationID);
            appType = oldApplication != null
                ? clsApplicationType.Find(oldApplication.ApplicationTypeID)
                : null;

            if (appType == null)
                return Fail("Application type was not found for the selected license.", "Error");

            licenseClass = clsLicenseClass.Find(oldLicense.LicenseClassID);
            if (licenseClass == null)
                return Fail("License class not found.", "Error");

            return true;
        }

        private bool TryCreateRenewApplication(clsDriver driver, clsApplicationType appType, out clsApplication renewApp)
        {
            renewApp = new clsApplication
            {
                ApplicationTypeID = appType.ApplicationTypeID,
                ApplicationPersonID = driver.PersonID,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = 3,
                LastStatusDate = DateTime.Now,
                PaidFees = appType.ApplicationTypeFees,
                CreatedByUserID = Session.CurrentUser.UserID
            };

            if (renewApp.Save())
                return true;

            return Fail("Failed to create renewal application.", "Error");
        }

        private bool TryDeactivateOldLicense(clsLicense oldLicense)
        {
            if (oldLicense.Deactivate())
                return true;

            return Fail("Failed to deactivate old license.", "Error");
        }

        private bool TryCreateRenewedLicense(clsLicense oldLicense, clsApplication renewApp, clsLicenseClass licenseClass, out clsLicense newLicense)
        {
            newLicense = new clsLicense
            {
                ApplicationID = renewApp.ApplicationID,
                DriverID = oldLicense.DriverID,
                LicenseClassID = oldLicense.LicenseClassID,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(10),
                Notes = ucRenewLocalDL1.RenewalNotes,
                IsActive = true,
                IssueReason = 2, // Renew
                PaidFees = licenseClass.ClassFees,
                CreatedByUserID = Session.CurrentUser.UserID
            };

            if (newLicense.Save())
                return true;

            oldLicense.IsActive = true;
            oldLicense.Save();

            return Fail("Failed to renew license.", "Error");
        }

        private void CompleteRenew(clsDriver driver, clsApplication renewApp, clsLicense newLicense)
        {
            _newLicenseId = newLicense.LicenseID;
            _selectedPersonId = driver.PersonID;

            ucRenewLocalDL1.SetRenewResult(renewApp.ApplicationID, _newLicenseId);

            ucRenewLocalDL1.Enabled = false;

            btnRenew.Enabled = false;
            lblShowNewLicenseInfo.Enabled = true;

            MessageBox.Show(
                $"License renewed successfully.\nNew License ID = {_newLicenseId}",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private bool Fail(string message, string title, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            RenewAttemptFinished?.Invoke();
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
            return false;
        }
    }
}
