using DVLD_BusinessAccess;
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
                cmDLApplications.Opening -= CmDLApplications_Opening;
                cmDLApplications.Opening += CmDLApplications_Opening;
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

        private void CmDLApplications_Opening(object sender, CancelEventArgs e)
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

            // Access dropdown items directly
            var vision = (ToolStripMenuItem)cmScheduleTests.DropDownItems["cmScheduleVisionTest"];
            var written = (ToolStripMenuItem)cmScheduleTests.DropDownItems["cmScheduleWrittenTest"];
            var street = (ToolStripMenuItem)cmScheduleTests.DropDownItems["cmScheduleStreetTest"];

            // Disable all first
            vision.Enabled = false;
            written.Enabled = false;
            street.Enabled = false;

            // If cancelled or completed → keep disabled
            if (isCancelled || isCompleted)
                return;

            int passedTests = clsTest.GetPassedTestCount(id);

            var testMap = new Dictionary<int, ToolStripMenuItem>()
    {
        {0, vision},
        {1, written},
        {2, street}
    };

            if (testMap.ContainsKey(passedTests))
                testMap[passedTests].Enabled = true;
        }
        private ToolStripMenuItem FindMenuItem(ToolStrip menu, string name)
            => menu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(i => i.Name == name);

        private ToolStripMenuItem FindMenuItem(ToolStripMenuItem parent, string name)
            => parent.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(i => i.Name == name);



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

        private void cmShowLicense_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Show License - Not implemented yet");
        }

        private void cmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("License History - Not implemented yet");
        }

        private void cmIssueDrivingLicense_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Issue Driving License - Not implemented yet");
        }
    }
}