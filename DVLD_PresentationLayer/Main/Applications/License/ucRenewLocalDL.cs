using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications.License
{
    public partial class ucRenewLocalDL : UserControl
    {
        public ucRenewLocalDL()
        {
            InitializeComponent();
        }

        public string RenewalNotes => tbNotes.Text.Trim();

        public void LoadData(clsLicense oldLicense)
        {
            if (oldLicense == null)
                return;

            clsApplication oldApplication = clsApplication.Find(oldLicense.ApplicationID);
            clsLicenseClass licenseClass = clsLicenseClass.Find(oldLicense.LicenseClassID);

            decimal appFees = (oldApplication != null ? clsApplicationType.Find(oldApplication.ApplicationTypeID) : null)?.ApplicationTypeFees ?? 0m;
            decimal licenseFees = licenseClass?.ClassFees ?? 0m;

            lblRenewLApplicationID.Text = "N/A";
            lblRnewedLocalLicenseID.Text = "N/A";

            lblOldLicenseID.Text = oldLicense.LicenseID.ToString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(10).ToShortDateString();

            lblApplicationFees.Text = appFees.ToString("0.00");
            lblLicenseFees.Text = licenseFees.ToString("0.00");
            lblTotalFees.Text = (appFees + licenseFees).ToString("0.00");
            lblCreatedBy.Text = Session.CurrentUser.UserName;

            tbNotes.Clear();
        }

        public void SetRenewResult(int renewApplicationId, int renewedLicenseId)
        {
            lblRenewLApplicationID.Text = renewApplicationId.ToString();
            lblRnewedLocalLicenseID.Text = renewedLicenseId.ToString();
        }
    }
}
