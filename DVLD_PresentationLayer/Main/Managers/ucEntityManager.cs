using DVLD_BusinessAccess;
using DVLD_PresentationAccess.Main.Applications.Licinse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Managers
{
    public partial class ucEntityManager : UserControl
    {
        private readonly ManageType _manage;
        private DataTable _currentTable;
        private readonly clsEventsManager _eventsManager = new clsEventsManager();

        public ManageType ManageMode => _manage;
        public bool IsLoaded { get; private set; }

        public event Action AddRequested;
        public event Action<DataRow> ShowRequested;
        public event Action<DataRow> EditRequested;
        public event Action<DataRow> DeleteRequested;

        public enum ManageType
        {
            People,
            Users,
            Drivers,
            LocalDLApplications,
            InterDLApplications,
            ApplicationTypes,
            TestTypes,
            DetainLicenses
        }

        public ucEntityManager(ManageType manage)
        {
            _manage = manage;
            InitializeComponent();

            ApplyUI();
            EnableDoubleBuffering();
        }

        private void EnableDoubleBuffering()
        {
            DoubleBuffered = true;
            typeof(DataGridView)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(dgvDVLD_Table, true, null);
        }

        private async void ucEntityManager_Load(object sender, EventArgs e)
        {
            if (IsLoaded) return;

            await RefreshDataAsync();
            IsLoaded = true;

            if ((_manage == ManageType.LocalDLApplications || _manage == ManageType.InterDLApplications)
                && cmDLApplications != null)
            {
                cmDLApplications.Opening -= cmDLApplications_Opening;
                cmDLApplications.Opening += cmDLApplications_Opening;
            }
        }

        private void ApplyUI()
        {
            switch (_manage)
            {
                case ManageType.People:
                    ucManageTitle.Text = "Manage People";
                    ucManagePicture.Image = Properties.Resources.People;
                    ContextMenuItems(true, true, true, true, true);
                    break;
                case ManageType.Users:
                    ucManageTitle.Text = "Manage Users";
                    ucManagePicture.Image = Properties.Resources.Users;
                    ContextMenuItems(true, true, true, true, true);
                    break;
                case ManageType.Drivers:
                    ucManageTitle.Text = "Manage Drivers";
                    ucManagePicture.Image = Properties.Resources.Drivers;
                    ContextMenuItems(true, true, true, true, true);
                    break;
                case ManageType.ApplicationTypes:
                    ucManageTitle.Text = "Manage Application Types";
                    ucManagePicture.Image = Properties.Resources.ApplicationTypes;
                    ContextMenuItems(false, false, true, false, false);
                    break;
                case ManageType.TestTypes:
                    ucManageTitle.Text = "Manage Test Types";
                    ucManagePicture.Image = Properties.Resources.TestTypes;
                    ContextMenuItems(false, false, true, false, false);
                    break;
                case ManageType.LocalDLApplications:
                    ucManageTitle.Text = "Manage Local Driving License Applications";
                    ucManagePicture.Image = Properties.Resources.LocalDrivingLicense;
                    dgvDVLD_Table.ContextMenuStrip = cmDLApplications;
                    break;
                case ManageType.InterDLApplications:
                    ucManageTitle.Text = "Manage International Driving License Applications";
                    ucManagePicture.Image = Properties.Resources.InternationalDrivingLicense;
                    dgvDVLD_Table.ContextMenuStrip = cmDLApplications;
                    break;
                case ManageType.DetainLicenses:
                    ucManageTitle.Text = "Manage Detain Licenses";
                    ucManagePicture.Image = Properties.Resources.DetainLicense;
                    ContextMenuItems(false, false, true, false, false);
                    break;
            }
        }

        private void ContextMenuItems(bool show, bool add, bool edit, bool delete, bool communication)
        {
            cmShowDetails.Visible = show;
            cmAdd.Visible = add;
            cmEdit.Visible = edit;
            cmDelete.Visible = delete;
            cmSendEmail.Visible = communication;
            cmCall.Visible = communication;
        }

        public async Task RefreshDataAsync()
        {
            _currentTable?.Dispose();

            var table = await Task.Run(() =>
            {
                return _manage switch
                {
                    ManageType.People => clsPerson.GetAllPeople(),
                    ManageType.Users => clsUser.GetAllUsers(),
                    ManageType.Drivers => clsDriver.GetAllDrivers(),
                    ManageType.ApplicationTypes => clsApplicationType.GetAllApplicationTypes(),
                    ManageType.TestTypes => clsTestType.GetAllTestTypes(),
                    ManageType.LocalDLApplications => clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications(),
                    ManageType.InterDLApplications => clsInternationalLicense.GetAllInternationalLicenses(),
                    ManageType.DetainLicenses => clsDetainedLicense.GetAllDetainedLicenses(),
                    _ => new DataTable()
                };
            });

            _currentTable = table;

            if (InvokeRequired)
                Invoke(new Action(BindData));
            else
                BindData();
        }

        private void BindData()
        {
            dgvDVLD_Table.DataSource = _currentTable;
            ucEntityFilter1.FilterChanged -= OnFilterChanged;
            ucEntityFilter1.FilterChanged += OnFilterChanged;
            ucEntityFilter1.Bind(_currentTable);
        }

        private void OnFilterChanged(DataView dv) => dgvDVLD_Table.DataSource = dv;

        private DataRow GetSelectedRow()
        {
            if (dgvDVLD_Table.SelectedRows.Count == 0) return null;
            return ((DataRowView)dgvDVLD_Table.SelectedRows[0].DataBoundItem).Row;
        }

        private void cmDLApplications_Opening(object sender, CancelEventArgs e)
        {
            var row = GetSelectedRow();
            if (row == null)
            {
                e.Cancel = true;
                return;
            }

            int id = Convert.ToInt32(row["L.D.LAppID"]);

            var localApp = clsLocalDrivingLicenseApplication.Find(id);
            if (localApp == null)
            {
                e.Cancel = true;
                return;
            }
             
            var application = clsApplication.Find(localApp.ApplicationID);
            if (application == null)
            {
                e.Cancel = true;
                return;
            }

            bool isCancelled = application.IsCancelled();
            bool isCompleted = application.IsCompleted();
            bool isNew = application.IsNew();

            // Access menu items
            var vision = (ToolStripMenuItem)cmScheduleTests.DropDownItems["cmScheduleVisionTest"];
            var written = (ToolStripMenuItem)cmScheduleTests.DropDownItems["cmScheduleWrittenTest"];
            var street = (ToolStripMenuItem)cmScheduleTests.DropDownItems["cmScheduleStreetTest"];

            // Get passed tests count directly from the bound row since the view updates correctly
            int passedTests = 0;
            if (row.Table.Columns.Contains("Passed Tests"))
                passedTests = Convert.ToInt32(row["Passed Tests"]);
            else if (row.Table.Columns.Contains("PassedTests")) // fallback column name
                passedTests = Convert.ToInt32(row["PassedTests"]);
            else
                passedTests = clsTest.GetPassedTestCount(id); // fallback to method call

            // If cancelled: only show application details
            if (isCancelled)
            {
                cmAppDetails.Enabled = true;
                
                cmAppEdit.Enabled = false;
                cmAppDelete.Enabled = false;
                cmAppCancel.Enabled = false;
                cmScheduleTests.Enabled = false;
                cmIssueDL.Enabled = false;
                cmShowLicense.Enabled = false;
                cmShowPersonLicenseHistory.Enabled = false;
                return;
            }

            // If completed: only show details, license, and history
            if (isCompleted)
            {
                cmAppDetails.Enabled = true;
                cmShowLicense.Enabled = true;
                cmShowPersonLicenseHistory.Enabled = true;
                
                cmAppEdit.Enabled = false;
                cmAppDelete.Enabled = false;
                cmAppCancel.Enabled = false;
                cmScheduleTests.Enabled = false;
                cmIssueDL.Enabled = false;
                return;
            }

            // If New status
            if (isNew)
            {
                // Enable standard options
                cmAppDetails.Enabled = true;
                cmAppEdit.Enabled = true;
                cmAppDelete.Enabled = true;
                cmAppCancel.Enabled = true;
                cmShowPersonLicenseHistory.Enabled = true;
                cmShowLicense.Enabled = false;

                // All 3 tests passed: disable schedule tests, enable issue license
                if (passedTests == 3)
                {
                    cmScheduleTests.Enabled = false;
                    cmIssueDL.Enabled = true;
                }
                else
                {
                    // Tests not completed: enable schedule tests, disable issue license
                    cmScheduleTests.Enabled = true;
                    cmIssueDL.Enabled = false;

                    // Enable only the next test to schedule
                    vision.Enabled = passedTests == 0;
                    written.Enabled = passedTests == 1;
                    street.Enabled = passedTests == 2;
                }
            }
        }
        

        // ---------------- General context menu handlers ----------------
        private void cm_Show_Click(object sender, EventArgs e) => ShowRequested?.Invoke(GetSelectedRow());
        private void cmAdd_Click(object sender, EventArgs e) => AddRequested?.Invoke();
        private void cm_Edit_Click(object sender, EventArgs e) => EditRequested?.Invoke(GetSelectedRow());
        private void cm_Delete_Click(object sender, EventArgs e) => DeleteRequested?.Invoke(GetSelectedRow());

        // ---------------- Local DL specific ----------------
        private void cmCancelApplication_Click(object sender, EventArgs e)
        {
            var row = GetSelectedRow();
            if (row != null)
                _eventsManager.HandleCancelApplication(this, row);
        }

        private void cmScheduleVisionTest_Click(object sender, EventArgs e)
        {
            var row = GetSelectedRow();
            if (row != null)
                _eventsManager.HandleScheduleVisionTest(this, row);
        }

        private void cmScheduleWrittenTest_Click(object sender, EventArgs e)
        {
            var row = GetSelectedRow();
            if (row != null)
                _eventsManager.HandleScheduleWrittenTest(this, row);
        }

        private void cmScheduleStreetTest_Click(object sender, EventArgs e)
        {
            var row = GetSelectedRow();
            if (row != null)
                _eventsManager.HandleScheduleStreetTest(this, row);
        }

        private void cmIssueDrivingLicense_Click(object sender, EventArgs e)
        {
            var row = GetSelectedRow();
            if (row != null)
                _eventsManager.HandleIssueDL(this, row);
        }

        private void cmShowLicense_Click(object sender, EventArgs e)
        {
            var row = GetSelectedRow();
            if (row != null)
                _eventsManager.HandleShowLicense(this, row);
        }

        private void cmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            var row = GetSelectedRow();
            if (row != null)
                _eventsManager.HandleShowPersonLicenseHistory(this, row);
        }
    }
}