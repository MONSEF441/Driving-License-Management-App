using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class ucRelease : UserControl
    {
        private int _licenseID = -1;

        public ucRelease()
        {
            InitializeComponent();
        }

        public void LoadData(clsDetainedLicense detainedLicense, int applicationTypeID)
        {
            _licenseID = detainedLicense.LicenseID;

            decimal applicationFees = 0m;
            clsApplicationType appType = clsApplicationType.Find(applicationTypeID);
            if (appType != null)
                applicationFees = appType.ApplicationTypeFees;

            lblDetainID.Text = detainedLicense.DetainID.ToString();
            lblLicenseID.Text = detainedLicense.LicenseID.ToString();
            lblDetainDate.Text = detainedLicense.DetainDate.ToShortDateString();
            lblFineFees.Text = detainedLicense.FineFees.ToString("0.00");

            lblApplicationID.Text = "N/A";
            lblApplicationFees.Text = applicationFees.ToString("0.00");
            lblTotalFees.Text = (detainedLicense.FineFees + applicationFees).ToString("0.00");
            lblCreatedBy.Text = Session.CurrentUser.UserName;
        }

        public void SetReleaseResult(int releaseApplicationID)
        {
            lblApplicationID.Text = releaseApplicationID.ToString();
        }

        public int GetLoadedLicenseID()
        {
            return _licenseID;
        }
    }
}
