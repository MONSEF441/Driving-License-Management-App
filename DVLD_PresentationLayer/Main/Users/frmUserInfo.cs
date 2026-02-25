using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Forms
{
    public partial class frmUserInfo : Form
    {
        private clsUser _user;

        public frmUserInfo(clsUser user)
        {
            InitializeComponent();
            _user = user;

            this.DoubleBuffered = true;

            // Load profile
            ucProfile1.LoadProfile(_user);

            // Subscribe to edit person request
            ucProfile1.EditPersonRequested += OnEditPersonRequested;
        }

        // Called when the edit link in ucPersonCard is clicked
        private void OnEditPersonRequested(clsPerson person)
        {
            if (person == null) return;

            // Open edit form
            using var frm = new frmPersonHost(frmPersonHost.EditorMode.Edit, person);
            frm.PersonSaved += updatedPerson =>
            {
                // Refresh profile after save
                _user = clsUser.Find(_user.UserID); // reload user to get latest personID, if needed
                ucProfile1.LoadProfile(_user);
            };

            frm.ShowDialog(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
