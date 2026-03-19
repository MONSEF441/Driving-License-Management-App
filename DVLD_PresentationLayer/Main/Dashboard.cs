using DVLD_BusinessAccess;
using DVLD_PresentationAccess.Forms;
using DVLD_PresentationAccess.Main.Applications;
using DVLD_PresentationAccess.Main.Applications.International_License;
using DVLD_PresentationAccess.Main.Users;
using DVLD_PresentationAccess.Managers;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class Dashboard : Form
    {
        // ---------------- UI layout tracking ----------------
        private Size originalPanelSize;
        private Point[] originalButtonPositions;
        private int[] originalButtonHeights;

        // ---------------- Active control ----------------
        private UserControl activeControl = null;

        // ---------------- Managers ----------------
        private Dictionary<ucEntityManager.ManageType, ucEntityManager> managers;
        private readonly clsEventsManager eventsManager = new clsEventsManager();

        public Dashboard()
        {
            InitializeComponent();

            // Track layout for resizing
            StoreOriginalLayout();
            this.Resize += Dashboard_Resize;

            // Create managers hidden
            InitializeManagers();
        }

        // ---------------- Initialize managers ----------------

        private void InitializeManagers()
        {
            managers = new Dictionary<ucEntityManager.ManageType, ucEntityManager>
    {
        { ucEntityManager.ManageType.People, new ucEntityManager(ucEntityManager.ManageType.People) },
        { ucEntityManager.ManageType.Users, new ucEntityManager(ucEntityManager.ManageType.Users) },
        { ucEntityManager.ManageType.Drivers, new ucEntityManager(ucEntityManager.ManageType.Drivers) },
        { ucEntityManager.ManageType.ApplicationTypes, new ucEntityManager(ucEntityManager.ManageType.ApplicationTypes) },
        { ucEntityManager.ManageType.TestTypes, new ucEntityManager(ucEntityManager.ManageType.TestTypes) },
        { ucEntityManager.ManageType.LocalDLApplications, new ucEntityManager(ucEntityManager.ManageType.LocalDLApplications) },
        { ucEntityManager.ManageType.InterDLApplications, new ucEntityManager(ucEntityManager.ManageType.InterDLApplications) },
        { ucEntityManager.ManageType.DetainLicenses, new ucEntityManager(ucEntityManager.ManageType.DetainLicenses) }
    };

            foreach (var manager in managers.Values)
            {
                manager.Dock = DockStyle.Fill;
                manager.Visible = false;
                PanelContainer.Controls.Add(manager);

                // Only wire events here — DO NOT wire inside ucEntityManager constructor
                eventsManager.WireEvents(manager);
            }
        }

        // ---------------- Load Manage pages ----------------

        private void LoadPage(ucEntityManager.ManageType type)
        {
            if (!managers.TryGetValue(type, out var page)) return;
            LoadPage(page);
        }

        private void LoadPage(UserControl page)
        {
            if (page == null || activeControl == page) return;

            PanelContainer.BackgroundImage = Properties.Resources.DVLD_BackBlur;
            PanelContainer.BringToFront();

            if (activeControl != null) activeControl.Visible = false;

            if (!PanelContainer.Controls.Contains(page))
            {
                PanelContainer.Controls.Add(page);
                page.Dock = DockStyle.Fill;
            }

            page.Visible = true;
            page.BringToFront();

            btnBack.BringToFront();
            btnBack.Visible = true;

            activeControl = page;
        }


        // ---------------- Layout helpers ----------------
        private void StoreOriginalLayout()
        {
            originalPanelSize = PanelMenu.Size;
            var buttons = new List<Guna2GradientButton>
            {
                btnToggleMenu,
                btnHome,
                btnApplications,
                btnPeople,
                btnDrivers,
                btnUsers,
                btnAccountSettings
            };

            originalButtonPositions = buttons.Select(b => b.Location).ToArray();
            originalButtonHeights = buttons.Select(b => b.Height).ToArray();
        }
        private void Dashboard_Resize(object sender, EventArgs e) => AdjustMenuButtons();
        private void AdjustMenuButtons()
        {
            var buttons = new List<Guna2GradientButton>
            {
                btnToggleMenu,
                btnHome,
                btnApplications,
                btnPeople,
                btnDrivers,
                btnUsers,
                btnAccountSettings
            };

            if (originalButtonPositions == null || originalButtonHeights == null)
                return;

            if (this.WindowState == FormWindowState.Maximized)
            {
                int menuHeight = PanelMenu.Height;
                int totalButtons = buttons.Count;
                int newHeight = menuHeight / totalButtons;
                int currentY = 0;

                foreach (var btn in buttons)
                {
                    btn.Height = newHeight;
                    btn.Top = currentY;
                    btn.Left = 0;
                    currentY += newHeight;
                }
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                PanelMenu.Size = originalPanelSize;
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Location = originalButtonPositions[i];
                    buttons[i].Height = originalButtonHeights[i];
                }
            }
        }
        private void AdjustPanelMenu()
        {
            if (PanelMenu.Size == PanelMenu.MaximumSize)
            {
                foreach (Control c in PanelMenu.Controls)
                    if (c is Guna2GradientButton btn && btn != btnToggleMenu)
                    {
                        btn.Text = "";
                        btn.ImageAlign = HorizontalAlignment.Center;
                        btn.TextAlign = HorizontalAlignment.Center;
                        btn.Padding = new Padding(0);
                    }
                PanelMenu.Size = PanelMenu.MinimumSize;
            }
            else
            {
                PanelMenu.Size = PanelMenu.MaximumSize;
                foreach (Control c in PanelMenu.Controls)
                    if (c is Guna2GradientButton btn && btn != btnToggleMenu)
                    {
                        btn.Text = btn.Tag?.ToString();
                        btn.ImageAlign = HorizontalAlignment.Left;
                        btn.TextAlign = HorizontalAlignment.Center;
                        btn.Padding = new Padding(15, 0, 0, 0);
                    }
            }
        }



        // ---------------- Panel Menu buttons ----------------
        private void btnToggleMenu_Click(object sender, EventArgs e) => AdjustPanelMenu();
        private void btnHome_Click(object sender, EventArgs e)
        {
            PanelContainer.BackgroundImage = Properties.Resources.DVLD_Back;

            if (activeControl != null)
            {
                // Unwire events if needed
                if (activeControl is ucEntityManager oldManager)
                    eventsManager.UnwireEvents(oldManager);

                activeControl.Visible = false;

                if (activeControl is ucProfile profileControl)
                    profileControl.ResetLoaded();

                activeControl = null;
            }

            PanelContainer.Visible = true;
        }
        private void btnManagePeople_Click(object sender, EventArgs e) => LoadPage(ucEntityManager.ManageType.People);
        private void btnManageUsers_Click(object sender, EventArgs e) => LoadPage(ucEntityManager.ManageType.Users);
        private void btnManageDrivers_Click(object sender, EventArgs e) => LoadPage(ucEntityManager.ManageType.Drivers);
        private void btnBack_Click(object sender, EventArgs e)
        {
            btnBack.Visible = false;
            PanelMenu.BringToFront();
            btnHome_Click(btnBack, e);
        }

      
        
        // ---------------- Applications Context Menu buttons ----------------
        private void btnApplications_Click(object sender, EventArgs e)
            => menuApplications.Show(btnApplications, new Point(btnApplications.Width, btnHome.Height - 85));

        private void btncmManage_ApplicationTypes_Click(object sender, EventArgs e) => LoadPage(ucEntityManager.ManageType.ApplicationTypes);

        private void btncmManage_TestTypes_Click(object sender, EventArgs e) => LoadPage(ucEntityManager.ManageType.TestTypes);

        private void btncmManage_Local_DLApplications_Click(object sender, EventArgs e) => LoadPage(ucEntityManager.ManageType.LocalDLApplications);

        private void btncmManage_Inter_DLApplications_Click(object sender, EventArgs e) => LoadPage(ucEntityManager.ManageType.InterDLApplications);

        private void btncmManage_DetainLicenses_Click(object sender, EventArgs e) => LoadPage(ucEntityManager.ManageType.DetainLicenses);

       
        
        // ---------------- Account Settings Context Menu buttons ----------------
        private void btnAccountSettings_Click(object sender, EventArgs e)
            => menuAccountSettings.Show(btnAccountSettings, new Point(btnAccountSettings.Width, btnAccountSettings.Height - 85));

        private void btncmProfile_Click(object sender, EventArgs e)
        {
            using var frm = new frmUserInfo(Session.CurrentUser);
            frm.ShowDialog();
        }
        private void btncmChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword();
            frm.ShowDialog();

        }
        private void btncmSignOut_Click(object sender, EventArgs e)
        {
            Session.End();
            this.Hide();
            new LoginScreen().ShowDialog();
            this.Close();
        }

        private void cmNewLocalLicense_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicense frm = new frmNewLocalDrivingLicense(frmNewLocalDrivingLicense.EditorMode.Add);
            frm.ShowDialog();
        }

        private void cmNewInterLicense_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm = new frmNewInternationalLicense();
            frm.ShowDialog();
        }
    }
}
