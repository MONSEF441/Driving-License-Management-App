using DVLD_BusinessAccess;
using System;
using System.IO;
using System.Windows.Forms;
using static DVLD_PresentationAccess.frmPersonHost;

namespace DVLD_PresentationAccess
{
    public partial class ucPersonCard : UserControl
    {
        public event Action<clsPerson> EditRequested;

        private clsPerson _currentPerson;


        public ucPersonCard()
        {
            InitializeComponent();
        }

        public void LoadPerson(clsPerson person)
        {
            if (person == null) return;

            _currentPerson = person; // store current person


            lblPersonID.Text = person.PersonID.ToString();
            lblFullName.Text = person.FirstName + " " + person.LastName;
            lblCIN.Text = person.CIN;
            lblGender.Text = person.Gender ? "Male" : "Female";
            lblCountry.Text = clsCountry.Find(person.NationalityCountryID).CountryName ;
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblPhone.Text = person.Phone;
            lblEmail.Text = person.Email;
            lblAddress.Text = person.Address;

            SetPersonImage(person);

        }


        private void SetPersonImage(clsPerson person)
        {
            if (!string.IsNullOrEmpty(person.ImagePath) && File.Exists(person.ImagePath))
            {
                pbPicture.ImageLocation = person.ImagePath;
            }
            else
            {
                pbPicture.Image = person.Gender
                    ? Properties.Resources.person_man
                    : Properties.Resources.person_woman;
            }
        }


        private void btnEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {   
                EditRequested?.Invoke(_currentPerson);
        }



    }
}
