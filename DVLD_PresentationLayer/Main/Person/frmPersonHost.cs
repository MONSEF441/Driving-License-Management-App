using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class frmPersonHost : Form
    {
        public enum EditorMode
        {
            Add,
            Edit,
            Show
        }

        private EditorMode _Mode;

        ucPersonEdit _ucPersonEdit;
       
        private clsPerson _person;

        public event Action<clsPerson> PersonSaved; // <-- change to clsPerson

        // Constructor
        public frmPersonHost(EditorMode mode, clsPerson person = null)
        {
            InitializeComponent();
            _Mode = mode;
            _person = person;


            this.DoubleBuffered = true;

        }


        private void frmEditorHost_Shown(object sender, EventArgs e)
        {
            LoadEditor();
        }

        private void LoadEditor()
        {
            panelContainer.Controls.Clear();

            string title;

            switch (_Mode)
            {
                case EditorMode.Show:
                    title = "Person Details";
                    LoadPersonDetails();
                    break;

                case EditorMode.Add:
                    title = "Add Person";
                    ucTitle.Visible = false;
                    LoadPersonEdit();
                    break;

                case EditorMode.Edit:
                    title = "UpdateType Person";
                    ucTitle.Visible = false;
                    LoadPersonEdit();
                    break;

                default:
                    return;
            }

            lblTitle.Text = title;
            lblPersonID.Text = _Mode == EditorMode.Add ? "N/A" :  _person.PersonID.ToString();
        }

        private void LoadPersonDetails()
        {
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(ucPersonCard);
            btnSave.Visible = false;

            ucPersonCard.EditRequested += OnPersonEditRequested;   
            ucPersonCard.LoadPerson(_person);
        }

        private void OnPersonEditRequested(clsPerson person)
        {
            var frmEdit = new frmPersonHost(frmPersonHost.EditorMode.Edit, person);
            frmEdit.PersonSaved += updatedPerson =>
            {
                // Refresh data in manager or reload card if needed
                ucPersonCard.LoadPerson(updatedPerson);
            };
            frmEdit.ShowDialog();
        }



        private void LoadPersonEdit()
        {
            _ucPersonEdit = new ucPersonEdit(
                _Mode == EditorMode.Edit ? _person.PersonID : -1)
            {
                Dock = DockStyle.Fill
            };

            panelContainer.Controls.Add(_ucPersonEdit);

            btnSave.Visible = true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            clsPerson person = _ucPersonEdit.GetPersonData();
            if (person == null)
                return; // Validation failed, ErrorProvider shows errors

            if (person.Save())
            {
                _person = person;
                PersonSaved?.Invoke(person);
                MessageBox.Show("Person saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
