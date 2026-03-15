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
using static DVLD_PresentationAccess.Main.Applications.ucScheduleTest; // changed reference

namespace DVLD_PresentationAccess.Main.Applications
{
    public partial class frmScheduleTest : Form
    {
        private TestType _TestMode;
        private clsLocalDrivingLicenseApplication _DLApp ; 
        private int _AppointmentID = -1;


        public enum TestType
        {
            VisionTest = 1,
            WritingTest = 2,
            StreetTest = 3
        }

        public frmScheduleTest(TestType mode, int DLApplicationID, int AppointmentID = -1)
        {
            InitializeComponent();

            _TestMode = mode;
            _DLApp = clsLocalDrivingLicenseApplication.Find(DLApplicationID) ;
            _AppointmentID = AppointmentID;

            lblTitle.Text = (_TestMode == TestType.VisionTest ? "Vision Test" : _TestMode == TestType.WritingTest ? "Writing Test" : "Street Test");
            
            ucScheduleTest1.LoadTestInfo(_DLApp, (ucScheduleTest.TestType) _TestMode, _AppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
