using DVLD_BusinessAccess;
using DVLD_PresentationAccess.Main.Managers;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Managers
{
    public partial class ucEntityManager : UserControl
    {
        private ManageType _Manage;
        private DataTable _CurrentTable;

        public ManageType ManageMode => _Manage;

        // ================= EVENTS =================
        public event Action AddRequested;
        public event Action<DataRow> ShowRequested;
        public event Action<DataRow> EditRequested;
        public event Action<DataRow> DeleteRequested;

        public bool IsLoaded { get; private set; } = false;



        public enum ManageType
        {
            People,
            Users,
            Drivers,
            LocalDrivingLicense,
            InternationalDrivingLicense,
            ApplicationTypes ,
            TestTypes,
            DetainLicenses
        }

        public ucEntityManager(ManageType manage)
        {
            _Manage = manage;
            InitializeComponent();
            ApplyUI();
            this.DoubleBuffered = true;
            typeof(DataGridView).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?.SetValue(dgvDVLD_Table, true, null);
        }

        private async void ucEntityManager_Load(object sender, EventArgs e)
        {
            if (IsLoaded) return;
            await RefreshDataAsync();
            IsLoaded = true;
        }

        // ================= CORE =================


        public async Task RefreshDataAsync()
        {
            if (_CurrentTable != null) _CurrentTable.Dispose();

            var table = await Task.Run(() =>
            {
                switch (_Manage)
                {
                    case ManageType.People: return clsPerson.GetAllPeople();
                    case ManageType.Users: return clsUser.GetAllUsers();
                    case ManageType.Drivers: return clsDriver.GetAllDrivers();
                    case ManageType.ApplicationTypes: return clsApplicationType.GetAllApplicationTypes();
                    case ManageType.TestTypes: return clsTestType.GetAllTestTypes();
                    case ManageType.LocalDrivingLicense: return clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
                    case ManageType.InternationalDrivingLicense: return clsInternationalLicense.GetAllInternationalLicenses();
                    case ManageType.DetainLicenses: return clsDetainedLicense.GetAllDetainedLicenses();

                    default: return new DataTable();
                }
            });

            _CurrentTable = table;

            // Invoke UI update safely
            if (InvokeRequired)
                Invoke(new Action(BindData));
            else
                BindData();
        }

        private void ContextMenuItems(bool value1, bool value2, bool value3, bool value4, bool value5)
        {
         cmShowDetails.Visible = value1;
         cmAdd.Visible = value2;
         cmEdit.Visible = value3;
         cmDelete.Visible = value4;
         cmSendEmail.Visible = value5;
         cmCall.Visible = value5;
        }
        private void ApplyUI() 
        {
            switch (_Manage)
            {
                case ManageType.People:
                    ucManageTitle.Text = "Manage People";
                    ucManagePicture.Image = Properties.Resources.People;
                    ContextMenuItems(true, true, true, true,true);
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

                case ManageType.LocalDrivingLicense:
                    ucManageTitle.Text = "Manage Local Driving License Applications";
                    ucManagePicture.Image = Properties.Resources.LocalDrivingLicense;
                    ContextMenuItems(false, false, true, false, false);

                    break;

                case ManageType.InternationalDrivingLicense:
                    ucManageTitle.Text = "Manage International Driving License Applications";
                    ucManagePicture.Image = Properties.Resources.InternationalDrivingLicense;
                    ContextMenuItems(false, false, true, false, false);

                    break;

                case ManageType.DetainLicenses:
                    ucManageTitle.Text = "Manage Detain License";
                    ucManagePicture.Image = Properties.Resources.DetainLicense;
                    ContextMenuItems(false, false, true, false, false);

                    break;
            }

        }

        private void BindData()
        {
            dgvDVLD_Table.DataSource = _CurrentTable;

            // 🔗 Bind filter
            ucEntityFilter1.Bind(_CurrentTable);

            ucEntityFilter1.FilterChanged -= OnFilterChanged;
            ucEntityFilter1.FilterChanged += OnFilterChanged;
        }

        private void OnFilterChanged(DataView dv)
        {
            dgvDVLD_Table.DataSource = dv;
        }

        // ================= HELPERS =================

        private DataRow GetSelectedRow()
        {
            if (dgvDVLD_Table.SelectedRows.Count == 0)
                return null;

            return ((DataRowView)dgvDVLD_Table.SelectedRows[0].DataBoundItem).Row;
        }


        // ================= CONTEXT MENU =================

        private void cm_Show_Click(object sender, EventArgs e) => ShowRequested?.Invoke(GetSelectedRow());
        private void cmAdd_Click(object sender, EventArgs e) => AddRequested?.Invoke();
        private void cm_Edit_Click(object sender, EventArgs e)  => EditRequested?.Invoke(GetSelectedRow());
        private void cm_Delete_Click(object sender, EventArgs e) => DeleteRequested?.Invoke(GetSelectedRow());

    }
}
