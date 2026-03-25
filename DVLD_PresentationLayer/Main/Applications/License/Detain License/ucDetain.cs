using DVLD_BusinessAccess;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class ucDetain : UserControl
    {
        private int _licenseID = -1;

        public ucDetain()
        {
            InitializeComponent();
            Reset();
           
        }

        public decimal FineFees
        {
            get
            {
                if (decimal.TryParse(tbFineFees.Text.Trim(), out decimal fineFees))
                    return fineFees;

                return -1m;
            }
        }

        public bool IsFineFeesValid => FineFees >= 0m;

        public void LoadData(clsLicense license)
        {
            if (license == null)
            {
                Reset();
                return;
            }

            _licenseID = license.LicenseID;
            lblDetainID.Text = "N/A";
            lblLicenseID.Text = license.LicenseID.ToString();
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = Session.CurrentUser.UserName;

            tbFineFees.Text = "0.00";
            tbFineFees.Focus();
            tbFineFees.SelectAll();
        }

        public void SetDetainResult(int detainID)
        {
            lblDetainID.Text = detainID.ToString();
            tbFineFees.Enabled = false;
        }

        public int GetLoadedLicenseID()
        {
            return _licenseID;
        }

        public void Reset()
        {
            _licenseID = -1;
            lblDetainID.Text = "[????]";
            lblLicenseID.Text = "[????]";
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = Session.CurrentUser?.UserName ?? "[????]";
            tbFineFees.Enabled = true;
            tbFineFees.Text = "0.00";
        }

        private void tbFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == '.' && !tbFineFees.Text.Contains("."))
                return;

            e.Handled = true;
        }

        private void tbFineFees_TextChanged(object sender, EventArgs e)
        {
            string input = tbFineFees.Text.Trim();
            if (string.IsNullOrWhiteSpace(input))
                return;

            if (!decimal.TryParse(input, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out _)
                && !decimal.TryParse(input, out _))
            {
                tbFineFees.Text = "0.00";
                tbFineFees.SelectionStart = tbFineFees.TextLength;
            }
        }
    }
}
