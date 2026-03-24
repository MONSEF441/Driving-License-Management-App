using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications.International_License
{
    public partial class ucInternationalApplicationInfo : UserControl
    {
        public ucInternationalApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadInternationalApplicationInfo(
            clsApplication application,
            clsInternationalLicense internationalLicense,
            clsLicense localLicense,
            clsApplicationType applicationType)
        {
            lblILApplicationID.Text = application?.ApplicationID.ToString() ?? "N/A";
            lblInterLicenseID.Text = internationalLicense?.InternationalLicenseID.ToString() ?? "N/A";
            lblLocalLicenseID.Text = localLicense?.LicenseID.ToString() ?? "N/A";

            lblFees.Text = applicationType != null
                ? applicationType.ApplicationTypeFees.ToString("0.00")
                : (application != null ? application.PaidFees.ToString("0.00") : "N/A");

            lblApplicationDate.Text = application != null
                ? application.ApplicationDate.ToShortDateString()
                : "N/A";

            lblIssueDate.Text = internationalLicense != null
                ? internationalLicense.IssueDate.ToShortDateString()
                : "N/A";

            lblExpirationDate.Text = internationalLicense != null
                ? internationalLicense.ExpirationDate.ToShortDateString()
                : "N/A";

            if (internationalLicense != null)
            {
                clsUser createdByUser = clsUser.Find(internationalLicense.CreatedByUserID);
                lblCreatedBy.Text = createdByUser?.UserName ?? "Unknown";
            }
            else if (application != null)
            {
                clsUser createdByUser = clsUser.Find(application.CreatedByUserID);
                lblCreatedBy.Text = createdByUser?.UserName ?? "Unknown";
            }
            else
            {
                lblCreatedBy.Text = "N/A";
            }
        }
    }
}
