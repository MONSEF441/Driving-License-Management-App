using DVLD_BusinessAccess;
using System;
using System.Data;
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
        private int _PersonID = -1;
        private DataTable _PeopleTable = clsPerson.GetAllPeople();
        private bool _IsEditMode => _user != null;

        public frmUserEdit(EditorMode Mode, clsUser user = null)
        {
            InitializeComponent();
            _Mode = Mode;

            if (user != null)
            {
                _user = user;
                _PersonID = _user.PersonID;
                LoadUserForEdit(_user);
            }
            this.DoubleBuffered = true;

        }

        private void frmUserEdit_Load(object sender, EventArgs e)
        {
            ucEntityFilter1.Bind(_PeopleTable);
            tabControl.SelectedIndex = 0; // always start from person page
        }

        private void LoadUserForEdit(clsUser user)
        {
            _user = user;
            _PersonID = user.PersonID;

            ucEntityFilter1.Enabled = false;
            btnAdd.Enabled = false;
            btnSearch.Enabled = false;

            var person = clsPerson.Find(_PersonID);
            ucPersonCard1.LoadPerson(person);

            tbUserName.Text = user.UserName;
            cbIsActive.Checked = user.IsActive;

            tbPassword.Text = "";
            tbConfirmPassword.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var filter = ucEntityFilter1.GetFilter();
            if (filter.Column == null)
            {
                MessageBox.Show("Please select a filter and enter a value.");
                return;
            }

            string column = filter.Column;
            string value = filter.Value.Replace("'", "''");

            string rowFilter = _PeopleTable.Columns[column].DataType == typeof(string)
                ? $"[{column}] = '{value}'"
                : $"[{column}] = {value}";

            DataRow[] rows = _PeopleTable.Select(rowFilter);
            if (rows.Length == 0)
            {
                MessageBox.Show("Person not found.");
                return;
            }

            if (rows.Length > 1)
            {
                MessageBox.Show("Multiple persons found. Please refine your search.");
                return;
            }

            _PersonID = Convert.ToInt32(rows[0]["PersonID"]);
            var person = clsPerson.Find(_PersonID);
            ucPersonCard1.LoadPerson(person);
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            var frm = new frmPersonHost(frmPersonHost.EditorMode.Add, null);
            frm.PersonSaved += person =>
            {
                _PersonID = person.PersonID;        
                ucPersonCard1.LoadPerson(person);   
            };
            frm.ShowDialog(this);
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_PersonID == -1)
            {
                MessageBox.Show("You must select a person first.");
                return;
            }

            lblUserID.Text = _user?.UserID.ToString() ?? "-1";
            tabControl.SelectedIndex = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return; // validation failed → errors shown inline

            if (_Mode == EditorMode.Add)
            {
                SaveNewUser();
            }
            else if (_Mode == EditorMode.Edit)
            {
                UpdateUser();
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveNewUser()
        {
            clsUser user = new clsUser
            {
                PersonID = _PersonID,
                UserName = tbUserName.Text.Trim(),
                Password = tbPassword.Text,
                IsActive = cbIsActive.Checked
            };

            if (user.Save())
            {
                _user = user; // keep reference
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

            if (!string.IsNullOrEmpty(tbPassword.Text) && tbPassword.Text == tbConfirmPassword.Text)
                _user.Password = tbPassword.Text;

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
            if (_PersonID <= 0)
            {
                errorProvider.SetError(ucPersonCard1, "Please select a person first.");
                valid = false;
            }

            // Username required & unique
            if (string.IsNullOrWhiteSpace(tbUserName.Text))
            {
                errorProvider.SetError(tbUserName, "Username is required.");
                valid = false;
            }
            else if (_Mode == EditorMode.Add && clsUser.isUserExist(tbUserName.Text.Trim()))
            {
                errorProvider.SetError(tbUserName, "Username already exists.");
                valid = false;
            }

            // Password & confirm (Add mode)
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
            else if (_Mode == EditorMode.Edit && !string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                // Optional password change
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
