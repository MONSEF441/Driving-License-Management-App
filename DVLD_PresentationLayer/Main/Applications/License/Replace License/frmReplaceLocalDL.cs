using DVLD_BusinessAccess;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class frmReplaceLocalDL : Form
    {
        private int _selectedLicenseId = -1;
        private int _selectedPersonId = -1;
        private int _newLicenseId = -1;

        private event Action ReplaceAttemptFinished;

        private const int ReplaceLostLicenseApplicationTypeID = 3;
        private const int ReplaceDamagedLicenseApplicationTypeID = 4;

        public frmReplaceLocalDL()
        {
            InitializeComponent();

            ReplaceAttemptFinished += () =>
            {
                btnRenew.Enabled = false;
                ucReplaceLocalDL1.Enabled = false;
            };

            rbDamaged.Checked = true;
            UpdateReplacementUi();
            ReplaceAttemptFinished?.Invoke();
        }

        private void UcSearchLicense1_LicenseSearched(int licenseId)
        {
            _selectedLicenseId = licenseId;
            _newLicenseId = -1;
            lblShowNewLicenseInfo.Enabled = false;

            clsLicense oldLicense = clsLicense.Find(licenseId);
            if (oldLicense == null)
            {
                ReplaceAttemptFinished?.Invoke();
                return;
            }

            clsDriver driver = clsDriver.Find(oldLicense.DriverID);
            _selectedPersonId = driver?.PersonID ?? -1;

            clsApplicationType selectedType = GetSelectedReplacementApplicationType();
            if (selectedType == null)
            {
                ReplaceAttemptFinished?.Invoke();
                MessageBox.Show("Replacement application type was not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ucReplaceLocalDL1.LoadData(oldLicense, selectedType.ApplicationTypeID);

            if (!oldLicense.IsActive)
            {
                ReplaceAttemptFinished?.Invoke();
                MessageBox.Show("Selected license is not active and cannot be replaced.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnRenew.Enabled = true;
            ucReplaceLocalDL1.Enabled = true;
        }

        private void ReplacementOptionChanged(object sender, EventArgs e)
        {
            UpdateReplacementUi();

            if (_selectedLicenseId > 0)
                UcSearchLicense1_LicenseSearched(_selectedLicenseId);
        }

        private void UpdateReplacementUi()
        {
            if (rbDamaged.Checked)
                lblTitle.Text = "Replace Damaged License";
            else
                lblTitle.Text = "Replace Lost License";

            clsApplicationType selectedType = GetSelectedReplacementApplicationType();
            ucReplaceLocalDL1.SetApplicationFees(selectedType?.ApplicationTypeID ?? -1);
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (!TryBuildReplaceContext(out clsLicense oldLicense, out clsDriver driver, out clsApplicationType appType))
                return;

            if (!TryCreateReplaceApplication(driver, appType, out clsApplication replaceApp))
                return;

            if (!TryDeactivateOldLicense(oldLicense))
                return;

            if (!TryCreateReplacedLicense(oldLicense, replaceApp, out clsLicense newLicense))
                return;

            CompleteReplacement(driver, replaceApp, newLicense);
        }

        private bool TryBuildReplaceContext(out clsLicense oldLicense, out clsDriver driver, out clsApplicationType appType)
        {
            oldLicense = null;
            driver = null;
            appType = null;

            if (_selectedLicenseId <= 0)
                return Fail("Please select a license first.", "Validation", MessageBoxIcon.Warning);

            oldLicense = clsLicense.Find(_selectedLicenseId);
            if (oldLicense == null)
                return Fail("License not found.", "Error");

            if (!oldLicense.IsActive)
                return Fail("Selected license is not active and cannot be replaced.", "Not Allowed", MessageBoxIcon.Warning);

            driver = clsDriver.Find(oldLicense.DriverID);
            if (driver == null)
                return Fail("Driver not found.", "Error");

            appType = GetSelectedReplacementApplicationType();
            if (appType == null)
                return Fail("Replacement application type was not found.", "Error");

            return true;
        }

        private bool TryCreateReplaceApplication(clsDriver driver, clsApplicationType appType, out clsApplication replaceApp)
        {
            replaceApp = new clsApplication
            {
                ApplicationTypeID = appType.ApplicationTypeID,
                ApplicationPersonID = driver.PersonID,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = 3,
                LastStatusDate = DateTime.Now,
                PaidFees = appType.ApplicationTypeFees,
                CreatedByUserID = Session.CurrentUser.UserID
            };

            if (replaceApp.Save())
                return true;

            return Fail("Failed to create replacement application.", "Error");
        }

        private bool TryDeactivateOldLicense(clsLicense oldLicense)
        {
            if (oldLicense.Deactivate())
                return true;

            return Fail("Failed to deactivate old license.", "Error");
        }

        private bool TryCreateReplacedLicense(clsLicense oldLicense, clsApplication replaceApp, out clsLicense newLicense)
        {
            newLicense = new clsLicense
            {
                ApplicationID = replaceApp.ApplicationID,
                DriverID = oldLicense.DriverID,
                LicenseClassID = oldLicense.LicenseClassID,
                IssueDate = DateTime.Now,
                ExpirationDate = oldLicense.ExpirationDate,
                Notes = oldLicense.Notes,
                IsActive = true,
                IssueReason = rbDamaged.Checked
                    ? (int)clsLicense.enIssueReason.Damaged
                    : (int)clsLicense.enIssueReason.Lost,
                PaidFees = 0m,
                CreatedByUserID = Session.CurrentUser.UserID
            };

            if (newLicense.Save())
                return true;

            oldLicense.IsActive = true;
            oldLicense.Save();

            return Fail("Failed to replace license.", "Error");
        }

        private void CompleteReplacement(clsDriver driver, clsApplication replaceApp, clsLicense newLicense)
        {
            _newLicenseId = newLicense.LicenseID;
            _selectedPersonId = driver.PersonID;

            ucReplaceLocalDL1.SetReplaceResult(replaceApp.ApplicationID, _newLicenseId);

            ucReplaceLocalDL1.Enabled = false;
            btnRenew.Enabled = false;
            lblShowNewLicenseInfo.Enabled = true;

            MessageBox.Show(
                $"License replaced successfully.\nNew License ID = {_newLicenseId}",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private clsApplicationType GetSelectedReplacementApplicationType()
        {
            int appTypeID = rbDamaged.Checked
                ? ReplaceDamagedLicenseApplicationTypeID
                : ReplaceLostLicenseApplicationTypeID;

            return clsApplicationType.Find(appTypeID);
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
                MessageBox.Show("No replaced license available.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var frm = new frmShowLocalLicense(_newLicenseId);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool Fail(string message, string title, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            ReplaceAttemptFinished?.Invoke();
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
            return false;
        }
    }
}
