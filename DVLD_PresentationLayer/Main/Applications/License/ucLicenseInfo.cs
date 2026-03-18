using DVLD_BusinessAccess;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications.License
{
    public partial class ucLicenseInfo : UserControl
    {
        public ucLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadProfile(int licenseId)
        {

            if (licenseId <= 0)
                return;

            clsLicense license = clsLicense.Find(licenseId);
            if (license == null)
                return;

            clsApplication application = clsApplication.Find(license.ApplicationID);
            clsDriver driver = clsDriver.Find(license.DriverID);
            clsLicenseClass licenseClass = clsLicenseClass.Find(license.LicenseClassID);
            clsUser createdByUser = clsUser.Find(license.CreatedByUserID);

            clsPerson person = null;
            if (application != null)
                person = clsPerson.Find(application.ApplicationPersonID);
            else if (driver != null)
                person = clsPerson.Find(driver.PersonID);

            // Left side
            lblClassName.Text = licenseClass.ClassName ;
            lblName.Text = $"{person.FirstName} {person.LastName}";
            lblCIN.Text = person.CIN.ToString() ;                  
            lblGender.Text =person.Gender ? "Male" : "Female" ;      
            lblLicenseID.Text = license.LicenseID.ToString();                                     
            lblDriverID.Text = license.DriverID.ToString();                             
            lblIssueReason.Text = "First Time Issue";                                     
            lblNotes.Text = string.IsNullOrWhiteSpace(license.Notes) ? "N/A" : license.Notes; 

            
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();   
            lblExpirationDate.Text = license.ExpirationDate.ToShortDateString();                   
            lblIssueDate.Text = license.IssueDate.ToShortDateString();                        
            lblIsActive.Text = license.IsActive ? "Yes" : "No";                       
            lblIsDetained.Text = IsLicenseDetained(license.LicenseID) ? "Yes" : "No"; ; ; ; ; ; ; ; ; ; ;

           

            LoadPersonImage(person);
        }

       
        private bool IsLicenseDetained(int licenseId)
        {
            DataTable dt = clsDetainedLicense.GetAllDetainedLicenses();
            if (dt == null || dt.Rows.Count == 0)
                return false;

            foreach (DataRow row in dt.Rows)
            {
                int rowLicenseId = Convert.ToInt32(row["LicenseID"]);
                bool isReleased = Convert.ToBoolean(row["IsReleased"]);

                if (rowLicenseId == licenseId && !isReleased)
                    return true;
            }

            return false;
        }

        private void LoadPersonImage(clsPerson person)
        {
            if (person == null)
            {
                pbPicture.Image = Properties.Resources.person_man;
                return;
            }

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
                    // fallback to default image
                }
            }

            pbPicture.Image = person.Gender
                ? Properties.Resources.person_man
                : Properties.Resources.person_woman;
        }
    }
}
