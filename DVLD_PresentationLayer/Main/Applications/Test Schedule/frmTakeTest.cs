using DVLD_BusinessAccess;
using System;
using System.Windows.Forms;
using static DVLD_PresentationAccess.frmScheduleAppointment;

namespace DVLD_PresentationAccess
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID;
        private AppontmentType _TestType;

        public frmTakeTest(int testAppointmentID, AppontmentType testType)
        {
            InitializeComponent();
            _TestAppointmentID = testAppointmentID;
            _TestType = testType;
            
            this.Load += FrmTakeTest_Load;
        }

        private void FrmTakeTest_Load(object sender, EventArgs e)
        {
            ucTakeTest1.LoadTestInfo(_TestType, _TestAppointmentID);

            // Fetch the test if it exists (meaning the appointment was locked)
            if (ucTakeTest1.IsAppointmentLocked())
            {
                int existingTestID = clsTest.GetTestIDByTestAppointmentID(_TestAppointmentID);

                if (existingTestID != -1)
                {
                    clsTest test = clsTest.Find(existingTestID);
                    if (test != null)
                    {
                        if (test.TestResult)
                            rbPass.Checked = true;  // Pass
                        else
                            rbFail.Checked = true;  // Fail

                        tbNotes.Text = test.Notes; 
                    }
                }

                MessageBox.Show("This test is already taken, you cannot modify it.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Disable the controls
                rbPass.Enabled = false;
                rbFail.Enabled = false;
                tbNotes.Enabled = false;

                if (btnSave != null) btnSave.Enabled = false; 
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e) 
        {
            if (ucTakeTest1.IsAppointmentLocked())
                return;

            if (!rbPass.Checked && !rbFail.Checked)
            {
                MessageBox.Show("Please select Pass or Fail result.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to save this result? After saving, you cannot change it.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            // Create the new test row
            clsTest newTest = new clsTest();
            newTest.TestAppointmentID = _TestAppointmentID;
            newTest.TestResult = rbPass.Checked; // true if pass is checked
            newTest.Notes = tbNotes.Text;
            newTest.CreatedByUserID = Session.CurrentUser.UserID ;

            if (newTest.Save())
            {
                // Test saved successfully, NOW we lock the appointment

                clsTestAppointment appointment = clsTestAppointment.Find(_TestAppointmentID);
                appointment.IsLocked = true;
                appointment.Save();

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: Data was not saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) 
        {
            this.Close();
        }
    }
}
