using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Editors
{
    public partial class ucUserCard : UserControl
    {
        public event Action<clsUser> EditRequested;
        private clsUser _currentUser;

        public ucUserCard()
        {
            InitializeComponent();

        }

        public void LoadUser(clsUser user)
        {
            if (user == null) return;

            _currentUser = user;
            this.SuspendLayout();

            lblUserID.Text = user.UserID.ToString();
            lblUserName.Text = user.UserName;
            lblActive.Text = user.IsActive ? "Yes" : "No";

            this.ResumeLayout();
        }

        private void btnEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Fire the event with the full user object
            EditRequested?.Invoke(_currentUser);
        }
    }
}
