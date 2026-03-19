using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications.License
{
    public partial class frmShowInternationalLicense : Form
    {
        private int _internationalLicenseId;


        public frmShowInternationalLicense(int internationalLicenseId)
        {
            _internationalLicenseId = internationalLicenseId;

            InitializeComponent();

            ucInternationalLicenseInfo1.LoadProfile(_internationalLicenseId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
