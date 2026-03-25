using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class ucReplaceLocalDL : UserControl
    {
        public ucReplaceLocalDL()
        {
            InitializeComponent();
        }

        private decimal GetApplicationFeesByTypeID(int applicationTypeID)
        {
            return clsApplicationType.Find(applicationTypeID)?.ApplicationTypeFees ?? 0m;
        }

        public void LoadData(clsLicense oldLicense, int applicationTypeID)
        {
            decimal applicationFees = GetApplicationFeesByTypeID(applicationTypeID);

            lblReplaceApplicationID.Text = "N/A";
            lblReplacedLocalLicenseID.Text = "N/A";

            lblOldLicenseID.Text = oldLicense.LicenseID.ToString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = applicationFees.ToString("0.00");
            lblCreatedBy.Text = Session.CurrentUser?.UserName ?? "N/A";
        }

        public void SetApplicationFees(int applicationTypeID)
        {
            decimal applicationFees = GetApplicationFeesByTypeID(applicationTypeID);
            lblApplicationFees.Text = applicationFees.ToString("0.00");
        }

        public void SetReplaceResult(int replaceApplicationId, int replacedLicenseId)
        {
            lblReplaceApplicationID.Text = replaceApplicationId.ToString();
            lblReplacedLocalLicenseID.Text = replacedLicenseId.ToString();
        }
    }
}
