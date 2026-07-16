using DVLD_BusinessAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class frmIssueDL : Form
    {
        private int _DLID;
        private int _AppID;
        public frmIssueDL(int DLID ,int AppID )
        {
            InitializeComponent();

                _DLID = DLID;
                _AppID = AppID;

            ucDLApplicationInfo1.LoadData( DLID);
            ucApplicationBasicInfo1.LoadData( AppID);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsApplication application = clsApplication.Find(_AppID);
            clsLocalDrivingLicenseApplication localApp = clsLocalDrivingLicenseApplication.Find(_DLID);
            clsLicenseClass licenseClass = clsLicenseClass.Find(localApp.LicenseClassID);

            if (application == null || localApp == null || licenseClass == null)
            {
                MessageBox.Show("Error: Missing application data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDriver driver = clsDriver.FindByPersonID(application.ApplicationPersonID);

            if (driver == null)
            {
                driver = new clsDriver
                {
                    PersonID = application.ApplicationPersonID,
                    CreatedByUserID = Session.CurrentUser.UserID,
                    CreatedDate = DateTime.Now
                };

                if (!driver.Save())
                {
                    MessageBox.Show("Error: Failed to Create Driver Record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            clsLicense newLicense = new clsLicense
            {
                ApplicationID = application.ApplicationID,
                DriverID = driver.DriverID,
                LicenseClassID = localApp.LicenseClassID,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(licenseClass.DefaultValidityLength),
                Notes = guna2TextBox1.Text.Trim(),
                IsActive = true,
                CreatedByUserID = Session.CurrentUser.UserID
            };

            if (!newLicense.Save())
            {
                MessageBox.Show("Error: Failed to Issue License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            application.ApplicationStatus = 3;
            application.LastStatusDate = DateTime.Now;

            if (!application.Save())
            {
                MessageBox.Show("License was issued, but failed to update application status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ucApplicationBasicInfo1.LoadData(_AppID);
            ucDLApplicationInfo1.LoadData(_DLID);

            MessageBox.Show(
                $"License Issued Successfully with License ID = {newLicense.LicenseID}",
                "Succeeded",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            btnIssue.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
