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
    public partial class ucScheduleTest : UserControl
    {
        private TestType _TestMode;
        private clsLocalDrivingLicenseApplication _DLApp;
        private clsApplication _App;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        public enum enCreationMode { AddNew = 0, Update = 1 }
        private enCreationMode _Mode = enCreationMode.AddNew;

        public enum TestType
        {
            VisionTest = 1,
            WritingTest = 2,
            StreetTest = 3
        }

        public ucScheduleTest()
        {
            InitializeComponent();
        }

        public void LoadTestInfo(clsLocalDrivingLicenseApplication DLApp, TestType testType, int TestAppointmentID = -1) 
        {
            _TestMode = testType;
            _DLApp = DLApp;
            _App = clsApplication.Find(_DLApp.ApplicationID);
            _TestAppointmentID = TestAppointmentID;

            // Load common shared data
            lblDLAppID.Text = _DLApp.LocalDrivingLicenseApplicationID.ToString();
            lblDClassName.Text = clsLicenseClass.Find(_DLApp.LicenseClassID).ClassName;
            lblName.Text = clsPerson.Find(_App.ApplicationPersonID) is { } p ? $"{p.FirstName} {p.LastName}" : "Not Found";
            lblTrial.Text = clsTest.GetTestTrialCount(_DLApp.LocalDrivingLicenseApplicationID, (int)_TestMode).ToString();

            ApplyUI();

            if (_TestAppointmentID == -1)
            {
                LoadNewAppointment();
            }
            else
            {
                LoadExistingAppointment();
            }
        }

        private void LoadNewAppointment()
        {
            _Mode = enCreationMode.AddNew;
            _TestAppointment = new clsTestAppointment();
            lblTestAppointmentDate.Value = DateTime.Now;
            lblTestAppointmentDate.MinDate = DateTime.Now; // Cannot schedule in past
            lblTestAppointmentFees.Text = clsTestType.Find((int)_TestMode).TestTypeFees.ToString();

            // Check if passed, then you can't schedule or edit
            if (clsTest.IsTestPassed(_DLApp.LocalDrivingLicenseApplicationID, (int)_TestMode))
            {
                btnSave.Enabled = false;
                lblTestAppointmentDate.Enabled = false;
                MessageBox.Show("This test is already passed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadExistingAppointment()
        {
            _Mode = enCreationMode.Update;
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: Test Appointment not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            lblTestAppointmentDate.Value = _TestAppointment.AppointmentDate;
            lblTestAppointmentDate.MinDate = DateTime.Today; 
            lblTestAppointmentFees.Text = _TestAppointment.PaidFees.ToString();

            // Is the appointment Locked (i.e. Test was already taken)?
            if (_TestAppointment.IsLocked)
            {
                lblTestAppointmentDate.Enabled = false;
                btnSave.Enabled = false;
                lblTestTitle.Text = "Appointment is Locked";
                MessageBox.Show("Person already sat for the test, appointment locked.", "Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ApplyUI()
        {
            switch (_TestMode)
            {
                case TestType.VisionTest:
                    lblTestTitle.Text = "Vision Test";
                    pbTestPicture.Image = Properties.Resources.VisionTest;
                    break;

                case TestType.WritingTest:
                    lblTestTitle.Text = "Writing Test";
                    pbTestPicture.Image = Properties.Resources.WritingTest;
                    break;

                case TestType.StreetTest:
                    lblTestTitle.Text = "Street Test";
                    pbTestPicture.Image = Properties.Resources.StreetTest;
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == enCreationMode.AddNew)
            {
                if (!SaveNewAppointment()) return;
            }
            else
            {
                if (!SaveExistingAppointment()) return;
            }

            if (_TestAppointment.Save())
            {
                MessageBox.Show("Appointment Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enCreationMode.Update;
                _TestAppointmentID = _TestAppointment.AppointmentID;
            }
            else
            {
                MessageBox.Show("Error Saving Appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SaveNewAppointment()
        {
            // Can't have active unlocked appointment
            if (HasActiveAppointment())
            {
                MessageBox.Show("Person already has an active appointment for this test! You cannot add a new one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _TestAppointment.LocalDrivingLicenseApplicationID = _DLApp.LocalDrivingLicenseApplicationID;
            _TestAppointment.TestTypeID = (int)_TestMode;
            _TestAppointment.AppointmentDate = lblTestAppointmentDate.Value;
            _TestAppointment.PaidFees = Convert.ToDecimal(lblTestAppointmentFees.Text);
            
            if (Session.CurrentUser != null)
               _TestAppointment.CreatedByUserID = Session.CurrentUser.UserID; 
            else
               _TestAppointment.CreatedByUserID = 1;

            return true;
        }

        private bool SaveExistingAppointment()
        {
            _TestAppointment.LocalDrivingLicenseApplicationID = _DLApp.LocalDrivingLicenseApplicationID;
            _TestAppointment.TestTypeID = (int)_TestMode;
            _TestAppointment.AppointmentDate = lblTestAppointmentDate.Value;
            
            return true;
        }

        private bool HasActiveAppointment()
        {
            DataTable dt = clsTestAppointment.GetTestAppointmentsByLocalDLAppAndTestType(_DLApp.LocalDrivingLicenseApplicationID, (int)_TestMode);
            foreach (DataRow row in dt.Rows)
            {
                if (!(bool)row["IsLocked"])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
