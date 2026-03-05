using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Users
{
    public partial class frmUserEdit : Form
    {
        public event Action<clsUser> UserSaved;
        public enum EditorMode
        {
            Add,
            Edit
        }

        private EditorMode _Mode;

        private clsUser _user;
      

        public frmUserEdit(EditorMode mode, clsUser user = null)
        {
            InitializeComponent();
            _Mode = mode;

            if (user != null)
            {
                _user = user;
                LoadUserForEdit(_user);
            }

            this.DoubleBuffered = true;
          
        }

    

        private void LoadUserForEdit(clsUser user)
        {
            _user = user;

            // Load selected person into user control
            ucSearchPerson1.LoadPerson(user.PersonID);
            ucSearchPerson1.Enabled = false; 

            tbUserName.Text = user.UserName;
            cbIsActive.Checked = user.IsActive;

            tbPassword.Text = "";
            tbConfirmPassword.Text = "";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ucSearchPerson1.SelectedPersonID <= 0)
            {
                MessageBox.Show("You must select a person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (clsUser.isPersonUser(ucSearchPerson1.SelectedPersonID) && _Mode == EditorMode.Add)
            {
                MessageBox.Show("You must select a non existing user ","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            lblUserID.Text = _user?.UserID.ToString() ?? "[????]";


            panelPersonSearch.Visible = false;
            panelUserDetails.Visible = true;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            panelUserDetails.Visible = false;
            panelPersonSearch.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            if (_Mode == EditorMode.Add)
                SaveNewUser();
            else
                UpdateUser();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveNewUser()
        {
            clsUser user = new clsUser
            {
                PersonID = ucSearchPerson1.SelectedPersonID,
                UserName = tbUserName.Text.Trim(),
                Password = tbPassword.Text,
                IsActive = cbIsActive.Checked
            };

            if (user.Save())
            {
                _user = user;
                UserSaved?.Invoke(user);
                MessageBox.Show("User added successfully");
                Close();
            }
        }

        private void UpdateUser()
        {
            if (_user == null) return;

            _user.UserName = tbUserName.Text.Trim();
            _user.IsActive = cbIsActive.Checked;

            if (!string.IsNullOrWhiteSpace(tbPassword.Text) &&
                tbPassword.Text == tbConfirmPassword.Text)
            {
                _user.Password = tbPassword.Text;
            }

            if (_user.Save())
            {
                UserSaved?.Invoke(_user);
                MessageBox.Show("User updated successfully");
            }
        }

        private bool ValidateFields()
        {
            bool valid = true;
            errorProvider.Clear();

            // Person must be selected
            if (ucSearchPerson1.SelectedPersonID <= 0)
            {
                errorProvider.SetError(ucSearchPerson1, "Please select a person first.");
                valid =  false;
            }

           

            // Username required & unique
            if (string.IsNullOrWhiteSpace(tbUserName.Text))
            {
                errorProvider.SetError(tbUserName, "Username is required.");
                valid = false;
            }
            else if (_Mode == EditorMode.Add &&
                     clsUser.isUserExist(tbUserName.Text.Trim()))
            {
                errorProvider.SetError(tbUserName, "Username already exists.");
                valid = false;
            }

            // Password validation
            if (_Mode == EditorMode.Add)
            {
                if (string.IsNullOrWhiteSpace(tbPassword.Text))
                {
                    errorProvider.SetError(tbPassword, "Password is required.");
                    valid = false;
                }
                else if (tbPassword.Text.Length < 6)
                {
                    errorProvider.SetError(tbPassword, "Password must be at least 6 characters.");
                    valid = false;
                }

                if (tbPassword.Text != tbConfirmPassword.Text)
                {
                    errorProvider.SetError(tbConfirmPassword, "Passwords do not match.");
                    valid = false;
                }
            }
            else if (_Mode == EditorMode.Edit &&
                     !string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                if (tbPassword.Text.Length < 6)
                {
                    errorProvider.SetError(tbPassword, "Password must be at least 6 characters.");
                    valid = false;
                }

                if (tbPassword.Text != tbConfirmPassword.Text)
                {
                    errorProvider.SetError(tbConfirmPassword, "Passwords do not match.");
                    valid = false;
                }
            }

            return valid;
        }

     
    }
}