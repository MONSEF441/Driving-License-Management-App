using DVLD_BusinessAccess;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class frmScheduleAppointment : Form
    {
        private int _DLApplicationID;
        private int _ApplicationID;
        private AppontmentType _AppointmentMode;


        public enum AppontmentType
        {
            VisionTest = 1,
            WritingTest = 2,
            StreetTest = 3
        }


        public frmScheduleAppointment(AppontmentType mode, int DLApplicationID, int ApplicationID)
        {
            InitializeComponent();

            _AppointmentMode = mode;
            _DLApplicationID = DLApplicationID;
            _ApplicationID = ApplicationID;

            ucDLApplicationInfo1.LoadData(DLApplicationID);
            ucApplicationBasicInfo1.LoadData(ApplicationID);

            ucApplicationBasicInfo1.OnPersonSaved += RefreshData;
           
            ApplyUI();
            FillAppointmentsTable();
        }
        
        private void RefreshData()
        {
            ucDLApplicationInfo1.LoadData(_DLApplicationID);
            ucApplicationBasicInfo1.LoadData(_ApplicationID);
        }

        private void ApplyUI()
        {
            switch (_AppointmentMode)
            {
                case AppontmentType.VisionTest:
                    lblAppointmentTitle.Text = "Vision Test";
                    pbAppointmentPicture.Image = Properties.Resources.VisionTest;
                    break;

                case AppontmentType.WritingTest:
                    lblAppointmentTitle.Text = "Writing Test";
                    pbAppointmentPicture.Image = Properties.Resources.WritingTest;
                    break;
                    
                case AppontmentType.StreetTest:
                    lblAppointmentTitle.Text = "Street Test";
                    pbAppointmentPicture.Image = Properties.Resources.StreetTest;
                    break;
            }
        }

        private void FillAppointmentsTable()
        {
            dgvAppointments.DataSource = clsTestAppointment.GetTestAppointmentsByLocalDLAppAndTestType(_DLApplicationID, (int)_AppointmentMode);
        }
        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            // 1) Verify that test hasn't been passed yet
            if (clsTest.IsTestPassed(_DLApplicationID, (int)_AppointmentMode))
            {
                MessageBox.Show("This person already passed this test.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2) Verify there are no Active appointments directly from the DataGridView
            foreach (DataGridViewRow row in dgvAppointments.Rows)
            {
                // Check if the IsLocked column exists and is false
                if (row.Cells["IsLocked"].Value != null && !(bool)row.Cells["IsLocked"].Value)
                {
                    string activeAppointmentID = row.Cells["AppointmentID"].Value.ToString();

                    MessageBox.Show($"Person already has an active appointment for this test (ID: {activeAppointmentID}). You cannot add a new one.",
                                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            
            frmScheduleTest test = new frmScheduleTest((frmScheduleTest.TestType)_AppointmentMode, _DLApplicationID);
            test.ShowDialog();
            FillAppointmentsTable(); 
        }
        private void cmEdit_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0) return;

            // Check if the appointment is already locked before attempting to edit
            if ((bool)dgvAppointments.SelectedRows[0].Cells["IsLocked"].Value)
            {
                MessageBox.Show("This appointment is locked and cannot be edited. The test was already taken.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int AppointmentID = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentID"].Value);

            // Call constructor passing AppointmentID (Edit mode)
            frmScheduleTest test = new frmScheduleTest((frmScheduleTest.TestType)_AppointmentMode, _DLApplicationID, AppointmentID);
            test.ShowDialog();
            FillAppointmentsTable();
        }
        private void cmTakeTest_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0) return;

            int testAppointmentID = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentID"].Value);
            bool isLocked = (bool)dgvAppointments.SelectedRows[0].Cells["IsLocked"].Value;

            if (isLocked)
            {
                MessageBox.Show("This appointment is already locked. The test has already been taken.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmTakeTest takeTestForm = new frmTakeTest(testAppointmentID, _AppointmentMode);
            takeTestForm.ShowDialog();
            
            // Refresh table after taking the test to reflect the lock state
            FillAppointmentsTable();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
