using DVLD_BusinessAccess;
using DVLD_PresentationAccess.Main.Applications.License;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications.International_License
{
    public partial class frmNewInternationalLicense : Form
    {
        private int _selectedLocalLicenseId = -1;
        private int _selectedPersonId = -1;
        private int _internationalLicenseId = -1;

        public frmNewInternationalLicense()
        {
            InitializeComponent();

            btnIssue.Enabled = false;
            lblShowLicenseInfo.Enabled = false;

            ucSearchLicense1.LicenseSearched += UcSearchLicense1_LicenseSearched;
           
           
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

            clsDriver driver = clsDriver.Find(localLicense.DriverID);
            _selectedPersonId = driver?.PersonID ?? -1;

            clsInternationalLicense existingInternational = clsInternationalLicense.FindByLocalLicenseID(localLicenseId);
            clsApplicationType appType = clsApplicationType.FindInternationalApplicationType();

            if (existingInternational != null)
            {
                _internationalLicenseId = existingInternational.InternationalLicenseID;
                btnIssue.Enabled = false;
                lblShowLicenseInfo.Enabled = true;

                clsApplication app = clsApplication.Find(existingInternational.ApplicationID);
                ucInternationalApplicationInfo1.LoadInternationalApplicationInfo(app, existingInternational, localLicense, appType);

                MessageBox.Show(
                    $"Local License ID = {localLicenseId} already has International License ID = {_internationalLicenseId}.",
                    "Already Issued",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            btnIssue.Enabled = true;
            lblShowLicenseInfo.Enabled = false;
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

            if (clsInternationalLicense.FindByLocalLicenseID(localLicense.LicenseID) != null)
            {
                btnIssue.Enabled = false;
                lblShowLicenseInfo.Enabled = true;
                MessageBox.Show("This local license already has an international license.", "Already Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            clsDriver driver = clsDriver.Find(localLicense.DriverID);
            if (driver == null)
            {
                MessageBox.Show("Driver record was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
                Notes = true,
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
