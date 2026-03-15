using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;
using static DVLD_PresentationAccess.Main.Applications.frmScheduleAppointment;

namespace DVLD_PresentationAccess.Main.Applications
{
    public partial class ucTakeTest : UserControl
    {
        private AppontmentType _TestMode;
        private clsLocalDrivingLicenseApplication _DLApp;
        private clsApplication _App;
        private clsTestAppointment _TestAppointment;

        public ucTakeTest()
        {
            InitializeComponent();
        }

        public void LoadTestInfo(AppontmentType testType, int TestAppointmentID)
        {
            _TestMode = testType;
            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: Test Appointment not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DLApp = clsLocalDrivingLicenseApplication.Find(_TestAppointment.LocalDrivingLicenseApplicationID);
            _App = clsApplication.Find(_DLApp.ApplicationID);
            

            lblDLAppID.Text = _DLApp.LocalDrivingLicenseApplicationID.ToString();
            lblDClassName.Text = clsLicenseClass.Find(_DLApp.LicenseClassID).ClassName;
            lblName.Text = clsPerson.Find(_App.ApplicationPersonID) is { } p ? $"{p.FirstName} {p.LastName}" : "Not Found";
            lblTrial.Text = clsTest.GetTestTrialCount(_DLApp.LocalDrivingLicenseApplicationID, (int)_TestMode).ToString();
            
            lblDate.Text = _TestAppointment.AppointmentDate.ToString("d");
            lblTestAppointmentFees.Text = _TestAppointment.PaidFees.ToString("0.00");

            
            if (_TestAppointment.IsLocked)
            {
                int testID = clsTest.GetTestIDByTestAppointmentID(_TestAppointment.AppointmentID);
                lblTestID.Text = testID.ToString(); 
            }
            else
            {
                 lblTestID.Text = "Not Taken Yet";
            }

            ApplyUI();
        }

        private void ApplyUI()
        {
            switch (_TestMode)
            {
                case AppontmentType.VisionTest:
                    lblTestTitle.Text = "Vision Test";
                    pbTestPicture.Image = Properties.Resources.VisionTest;
                    break;
                case AppontmentType.WritingTest:
                    lblTestTitle.Text = "Writing Test";
                    pbTestPicture.Image = Properties.Resources.WritingTest;
                    break;
                case AppontmentType.StreetTest:
                    lblTestTitle.Text = "Street Test";
                    pbTestPicture.Image = Properties.Resources.StreetTest;
                    break;
            }
        }

        public bool IsAppointmentLocked()
        {
            return _TestAppointment?.IsLocked ?? false;
        }
    }
}
