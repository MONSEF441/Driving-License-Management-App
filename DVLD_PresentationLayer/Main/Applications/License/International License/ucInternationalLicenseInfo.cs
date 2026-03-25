using DVLD_BusinessAccess;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class ucInternationalLicenseInfo : UserControl
    {
        public ucInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadProfile(int internationalLicenseId)
        {
            if (internationalLicenseId <= 0)
                return;

            clsInternationalLicense interLicense = clsInternationalLicense.Find(internationalLicenseId);
            if (interLicense == null)
                return;

            clsDriver driver = clsDriver.Find(interLicense.DriverID);
            clsPerson person = driver != null ? clsPerson.Find(driver.PersonID) : null;
            clsLicense localLicense = clsLicense.Find(interLicense.IssuedUsingLocalLicenseID);

            lblIntLicenseID.Text = interLicense.InternationalLicenseID.ToString();
            lblApplicationID.Text = interLicense.ApplicationID.ToString();
            lblDriverID.Text = interLicense.DriverID.ToString();
            lblLicenseID.Text = interLicense.IssuedUsingLocalLicenseID.ToString();
            lblIssueDate.Text = interLicense.IssueDate.ToShortDateString();
            lblExpirationDate.Text = interLicense.ExpirationDate.ToShortDateString();
            lblIsActive.Text = interLicense.Notes ? "Yes" : "No";

            if (person != null)
            {
                lblName.Text = $"{person.FirstName} {person.LastName}";
                lblCIN.Text = person.CIN;
                lblGender.Text = person.Gender ? "Male" : "Female";
                lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
                LoadPersonImage(person);
            }
          
        }

        private void LoadPersonImage(clsPerson person)
        {
            if (!string.IsNullOrWhiteSpace(person.ImagePath) && File.Exists(person.ImagePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(person.ImagePath, FileMode.Open, FileAccess.Read))
                    using (Image img = Image.FromStream(fs))
                    {
                        pbPicture.Image = new Bitmap(img);
                    }
                    return;
                }
                catch
                {
                }
            }

            pbPicture.Image = person.Gender
                ? Properties.Resources.person_man
                : Properties.Resources.person_woman;
        }
    }
}
