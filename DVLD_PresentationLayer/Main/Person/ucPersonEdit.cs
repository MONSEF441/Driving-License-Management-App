using DVLD_BusinessAccess;
using DVLD_PresentationAccess.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class ucPersonEdit : UserControl
    {
        private int _PersonID;
        private bool _IsEditMode;
        private clsPerson _Person;
        private static object _countriesCache;



        public ucPersonEdit(int personID = -1)
        {
            InitializeComponent();
            _PersonID = personID;
            _IsEditMode = personID != -1;


        }
        private void ucPersonEdit_Load(object sender, EventArgs e)
        {
            LoadCountries();

            if (_IsEditMode)
            {
                _Person = clsPerson.Find(_PersonID);
                if (_Person == null) return;
                FillPersonData();
                SetDefaultImageIfNeeded();


            }
            else
            {
                _Person = new clsPerson(); // ✅ VERY IMPORTANT
                SetDefaultImageIfNeeded();
            }

        }

        private void LoadCountries()
        {
      
            if (_countriesCache == null)
                _countriesCache = clsCountry.GetAllCountries();

            cbCountry.DataSource = _countriesCache;
            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "CountryID";
            cbCountry.SelectedValue = 211;
        }

        public clsPerson GetPersonData()
        {
            if (!ValidateFields())
                return null; // Return null if validation fails

            _Person.PersonID = _PersonID;
            _Person.FirstName = tbFirstName.Text.Trim();
            _Person.LastName = tbLastName.Text.Trim();
            _Person.CIN = tbCIN.Text.Trim();
            _Person.Gender = rbMale.Checked;
            _Person.NationalityCountryID = (int)cbCountry.SelectedValue;
            _Person.DateOfBirth = tbDateOfBirth.Value;
            _Person.Phone = tbPhone.Text.Trim();
            _Person.Email = tbEmail.Text.Trim();
            _Person.Address = tbAddress.Text.Trim();
            _Person.ImagePath = pbPicture.ImageLocation ?? string.Empty;

            return _Person;
        }

        private void FillPersonData() 
        {

            tbFirstName.Text = _Person.FirstName;
            tbLastName.Text = _Person.LastName;
            tbCIN.Text = _Person.CIN;

            rbMale.Checked = _Person.Gender;
            rbFemale.Checked = !_Person.Gender;
            
            cbCountry.SelectedValue = _Person.NationalityCountryID;
            tbDateOfBirth.Value = _Person.DateOfBirth;
            tbPhone.Text = _Person.Phone;
            tbEmail.Text = _Person.Email;
            tbAddress.Text = _Person.Address;
            pbPicture.ImageLocation = _Person.ImagePath ;


            if (!string.IsNullOrEmpty(_Person.ImagePath))
            {
                pbPicture.ImageLocation = _Person.ImagePath;
            }
            else
            {
                pbPicture.ImageLocation = null;
                btnRemoveImage.Visible = false;
            }

            SetDefaultImageIfNeeded();



        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbCIN.Text))
            {
                MessageBox.Show("Please enter CIN first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp"
            };

           

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = $"Person_{tbCIN.Text.Trim()}.jpg";

                string savedPath = clsImageHelper.SaveImage( ofd.FileName,"People",fileName);

                pbPicture.ImageLocation = savedPath;
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pbPicture.ImageLocation) &&
         File.Exists(pbPicture.ImageLocation))
            {
                File.Delete(pbPicture.ImageLocation);
            }

            pbPicture.ImageLocation = null;
            _Person.ImagePath = string.Empty;

            SetDefaultImageIfNeeded();
        }

        private void SetDefaultImageIfNeeded()
        {
            if (!string.IsNullOrEmpty(pbPicture.ImageLocation))
                return; // custom image exists → do nothing

            pbPicture.Image = rbMale.Checked
                ? Properties.Resources.person_man
                : Properties.Resources.person_woman;
        }

        public bool ValidateFields()
        {
            bool valid = true;
            errorProvider.Clear();

            // First Name
            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                errorProvider.SetError(tbFirstName, "First name is required.");
                valid = false;
            }

            // Last Name
            if (string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                errorProvider.SetError(tbLastName, "Last name is required.");
                valid = false;
            }

            // CIN
            if (string.IsNullOrWhiteSpace(tbCIN.Text))
            {
                errorProvider.SetError(tbCIN, "CIN is required.");
                valid = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(tbCIN.Text.Trim(), @"^[A-Za-z]\d{6}$"))
            {
                errorProvider.SetError(tbCIN, "CIN must start with a letter followed by 6 digits.");
                valid = false;
            }
            else if (_PersonID == -1 && clsPerson.CINExist(tbCIN.Text.Trim()))
            {
                errorProvider.SetError(tbCIN, "CIN already exists.");
                valid = false;
            }

            // Email
            if (!string.IsNullOrWhiteSpace(tbEmail.Text) && !tbEmail.Text.Contains("@"))
            {
                errorProvider.SetError(tbEmail, "Email must contain '@'.");
                valid = false;
            }

            return valid;
        }

    }


}



 