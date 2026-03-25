using DVLD_BusinessAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class frmChangePassword : Form
    {
        public event Action<clsUser> UserSaved;


        public frmChangePassword()
        {
            InitializeComponent();

            ucUserCard1.LoadUser(Session.CurrentUser);
            this.DoubleBuffered = true;


        }

        private bool ValidateFields()
        {
            bool valid = true;
            errorProvider.Clear();

            // Current password check
            if (tbCurrentPassword.Text != Session.CurrentUser.Password)
            {
                errorProvider.SetError(tbCurrentPassword, "Current password is incorrect.");
                valid = false;
            }

            // New & confirm
            if (string.IsNullOrWhiteSpace(tbNewPassword.Text) || tbNewPassword.Text.Length < 6)
            {
                errorProvider.SetError(tbNewPassword, "New password must be at least 6 characters.");
                valid = false;
            }

            if (tbNewPassword.Text != tbConfirmPassword.Text)
            {
                errorProvider.SetError(tbConfirmPassword, "Passwords do not match.");
                valid = false;
            }

            return valid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return; // show errors inline

            // 🔥 Set new password
            Session.CurrentUser.Password = tbNewPassword.Text;

            if (Session.CurrentUser.Save())
            {
                UserSaved?.Invoke(Session.CurrentUser);
                Close();
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

      
    }




}
