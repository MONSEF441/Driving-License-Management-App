using DVLD_BusinessAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications
{
    public partial class frmNewLocalDrivingLicense : Form
    {
        public event Action<clsLocalDrivingLicenseApplication> LocalDLApplicationSaved  ;

        private static object _LicenseClassesCache;
        public enum EditorMode
        {
            Add,
            Edit
        }

        private EditorMode _Mode;
        private clsLocalDrivingLicenseApplication _LocalApplication;

        public frmNewLocalDrivingLicense(EditorMode mode, clsLocalDrivingLicenseApplication localApplication = null)
        {
            InitializeComponent();
            _Mode = mode;
            LoadLicenseClasses();

            if (localApplication != null)
            {
                _LocalApplication = localApplication;
                LoadApplicationForEdit(_LocalApplication);
            }
        }

        private void LoadApplicationForEdit(clsLocalDrivingLicenseApplication localApplication)
        {
            _LocalApplication = localApplication;

            // Load the base application
            clsApplication application = clsApplication.Find(_LocalApplication.ApplicationID);

            if (application == null)
            {
                MessageBox.Show("Base application not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            // Load selected person into user control and disable it
            ucSearchPerson1.LoadPerson(application.ApplicationPersonID);
            ucSearchPerson1.Enabled = false;

            // Fill the form with existing data
            lblDLApplicationID.Text = _LocalApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = application.ApplicationDate.ToString();
            lblCreatedBy.Text = clsUser.Find(application.CreatedByUserID)?.UserName ?? "Unknown";
            cbLicenseClass.SelectedValue = _LocalApplication.LicenseClassID;

            // Skip to application info panel
            panelPersonSearch.Visible = false;
            panelApplicationInfo.Visible = true;
        }

        private void LoadLicenseClasses()
        {
            if (_LicenseClassesCache == null)
                _LicenseClassesCache = clsLicenseClass.GetAllLicenseClasses();

            cbLicenseClass.DataSource = _LicenseClassesCache;
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
            cbLicenseClass.SelectedValue = 1;

            lblApplicationDate.Text = DateTime.Now.ToString();
            lblCreatedBy.Text = Session.CurrentUser.UserName;
            
            DataRowView selectedRow = cbLicenseClass.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                lblApplicationFees.Text = selectedRow["ClassFees"].ToString();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ucSearchPerson1.SelectedPersonID <= 0)
            {
                MessageBox.Show("You must select a person first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (clsLocalDrivingLicenseApplication.isLocalDrivingLicenseApplicationExist(ucSearchPerson1.SelectedPersonID) && _Mode == EditorMode.Add)
            {
                MessageBox.Show("You must select a non existing user ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblDLApplicationID.Text = _LocalApplication?.LocalDrivingLicenseApplicationID.ToString() ?? "[????]";

            panelPersonSearch.Visible = false;
            panelApplicationInfo.Visible = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            panelApplicationInfo.Visible = false;
            panelPersonSearch.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == EditorMode.Add)
                SaveNewApplication();
            else
                UpdateApplication();
        }

        private void SaveNewApplication()
        {
            int selectedLicenseClassID = Convert.ToInt32(cbLicenseClass.SelectedValue);

            // Check if the person already has an active application for the same license class
            if (clsLocalDrivingLicenseApplication.DoesPersonHaveActiveApplicationForLicenseClass(ucSearchPerson1.SelectedPersonID, selectedLicenseClassID))
            {
                MessageBox.Show(
                    "This person already has an active (New or Completed) application for the selected license class.\n\n" +
                    "Previous applications must be cancelled before creating a new one.", 
                    "Active Application Exists", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            // Create the base application first
            clsApplication application = new clsApplication();
            application.ApplicationTypeID = 1; // New Driving License Application
            application.ApplicationPersonID = ucSearchPerson1.SelectedPersonID;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = 1; // New status
            application.LastStatusDate = DateTime.Now;
            application.PaidFees = Convert.ToDecimal(lblApplicationFees.Text);
            application.CreatedByUserID = Session.CurrentUser.UserID;

            // Save the base application
            if (!application.Save())
            {
                MessageBox.Show("Failed to save the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create and save the local driving license application
            _LocalApplication = new clsLocalDrivingLicenseApplication();
            _LocalApplication.ApplicationID = application.ApplicationID;
            _LocalApplication.LicenseClassID = selectedLicenseClassID;

            if (_LocalApplication.Save())
            {
                _Mode = EditorMode.Edit;
                lblDLApplicationID.Text = _LocalApplication.LocalDrivingLicenseApplicationID.ToString();
                LocalDLApplicationSaved?.Invoke(_LocalApplication);
                MessageBox.Show("Application saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Failed to save the local driving license application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateApplication()
        {
            if (_LocalApplication == null) return;

            int selectedLicenseClassID = Convert.ToInt32(cbLicenseClass.SelectedValue);

            // Update only the license class
            _LocalApplication.LicenseClassID = selectedLicenseClassID;

            // Update the base application fees if changed
            clsApplication application = clsApplication.Find(_LocalApplication.ApplicationID);
            if (application != null)
            {
                application.PaidFees = Convert.ToDecimal(lblApplicationFees.Text);
                application.Save();
            }

            if (_LocalApplication.Save())
            {
                LocalDLApplicationSaved?.Invoke(_LocalApplication);
                MessageBox.Show("Application updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update the local driving license application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView selectedRow = cbLicenseClass.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                lblApplicationFees.Text = selectedRow["ClassFees"].ToString();
            }
        }
    }
}
