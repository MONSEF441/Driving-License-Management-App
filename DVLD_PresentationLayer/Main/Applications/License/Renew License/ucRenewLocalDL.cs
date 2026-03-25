using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class ucRenewLocalDL : UserControl
    {
        public ucRenewLocalDL()
        {
            InitializeComponent();
        }

        public string RenewalNotes => tbNotes.Text.Trim();

        public void LoadData(clsLicense oldLicense, int renewApplicationTypeID)
        {
            if (oldLicense == null)
                return;

            clsLicenseClass licenseClass = clsLicenseClass.Find(oldLicense.LicenseClassID);
            clsApplicationType renewAppType = clsApplicationType.Find(renewApplicationTypeID);

            decimal appFees = renewAppType?.ApplicationTypeFees ?? 0m;
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
