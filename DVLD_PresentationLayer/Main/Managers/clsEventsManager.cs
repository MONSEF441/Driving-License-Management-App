using DVLD_BusinessAccess;
using DVLD_PresentationAccess.Forms;
using DVLD_PresentationAccess.Main.Applications;
using DVLD_PresentationAccess.Main.Users;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Managers
{
    public class clsEventsManager
    {
        private Action<DataRow> _showHandler;
        private Action _addHandler;
        private Action<DataRow> _editHandler;
        private Action<DataRow> _deleteHandler;

        // ---------------- Wiring ----------------
        public void WireEvents(ucEntityManager manager)
        {
            UnwireEvents(manager);

            _showHandler = row => HandleShow(manager, row);
            _addHandler = () => HandleAdd(manager);
            _editHandler = row => HandleEdit(manager, row);
            _deleteHandler = row => _ = HandleDelete(manager, row);

            manager.ShowRequested += _showHandler;
            manager.AddRequested += _addHandler;
            manager.EditRequested += _editHandler;
            manager.DeleteRequested += _deleteHandler;
        }

        public void UnwireEvents(ucEntityManager manager)
        {
            if (manager == null) return;

            if (_showHandler != null) manager.ShowRequested -= _showHandler;
            if (_addHandler != null) manager.AddRequested -= _addHandler;
            if (_editHandler != null) manager.EditRequested -= _editHandler;
            if (_deleteHandler != null) manager.DeleteRequested -= _deleteHandler;

            _showHandler = null;
            _addHandler = null;
            _editHandler = null;
            _deleteHandler = null;
        }

        // ---------------- Core Handlers ----------------
        private void HandleShow(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            switch (manager.ManageMode)
            {
                case ucEntityManager.ManageType.People:
                    {
                        int id = Convert.ToInt32(row["PersonID"]);
                        var entity = clsPerson.Find(id);
                        using var frm = new frmPersonHost(frmPersonHost.EditorMode.Show, entity);
                        frm.ShowDialog();
                        break;
                    }
                case ucEntityManager.ManageType.Users:
                    {
                        int id = Convert.ToInt32(row["UserID"]);
                        var entity = clsUser.Find(id);
                        using var frm = new frmUserInfo(entity);
                        frm.ShowDialog();
                        break;
                    }
            }
        }

        private void HandleAdd(ucEntityManager manager)
        {
            switch (manager.ManageMode)
            {
                case ucEntityManager.ManageType.People:
                    {
                        var frm = new frmPersonHost(frmPersonHost.EditorMode.Add, null);
                        frm.PersonSaved += async _ => await manager.RefreshDataAsync();
                        frm.Show();
                        break;
                    }
                case ucEntityManager.ManageType.Users:
                    {
                        var frm = new frmUserEdit(frmUserEdit.EditorMode.Add, null);
                        frm.UserSaved += async _ => await manager.RefreshDataAsync();
                        frm.Show();
                        break;
                    }
                case ucEntityManager.ManageType.LocalDLApplications:
                    {
                        var frm = new frmNewLocalDrivingLicense(frmNewLocalDrivingLicense.EditorMode.Add, null);
                        frm.LocalDLApplicationSaved += async _ => await manager.RefreshDataAsync();
                        frm.Show();
                        break;
                    }
            }
        }

        private void HandleEdit(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            switch (manager.ManageMode)
            {
                case ucEntityManager.ManageType.Users:
                    {
                        int id = Convert.ToInt32(row["UserID"]);
                        var entity = clsUser.Find(id);
                        var frm = new frmUserEdit(frmUserEdit.EditorMode.Edit, entity);
                        frm.UserSaved += async _ => await manager.RefreshDataAsync();
                        frm.ShowDialog();
                        break;
                    }
                case ucEntityManager.ManageType.LocalDLApplications:
                    {
                        int id = Convert.ToInt32(row["L.D.LAppID"]);
                        var entity = clsLocalDrivingLicenseApplication.Find(id);
                        var frm = new frmNewLocalDrivingLicense(frmNewLocalDrivingLicense.EditorMode.Edit, entity);
                        frm.LocalDLApplicationSaved += async _ => await manager.RefreshDataAsync();
                        frm.ShowDialog();
                        break;
                    }
            }
        }

        private async Task HandleDelete(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            switch (manager.ManageMode)
            {
                case ucEntityManager.ManageType.People:
                    {
                        int personID = Convert.ToInt32(row["PersonID"]);
                        string personName = row["FullName"].ToString();
                        
                        // Check if can be deleted
                        if (!clsPerson.CanDelete(personID))
                        {
                            MessageBox.Show(
                                "Cannot delete this person.\n\nThis person is associated with other records (applications, users, drivers, etc.).",
                                "Cannot Delete",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }

                        // Confirm deletion
                        var result = MessageBox.Show(
                            $"Are you sure you want to delete '{personName}'?\n\nThis action cannot be undone.",
                            "Confirm Delete",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            if (clsPerson.DeletePerson(personID))
                            {
                                MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Don't refresh if delete failed
                            }
                        }
                        else
                        {
                            return; // User cancelled, don't refresh
                        }
                        break;
                    }

                case ucEntityManager.ManageType.Users:
                    {
                        int userID = Convert.ToInt32(row["UserID"]);
                        string userName = row["UserName"].ToString();

                        // Confirm deletion
                        var result = MessageBox.Show(
                            $"Are you sure you want to delete user '{userName}'?\n\nThis will revoke all access privileges for this user.\n\nThis action cannot be undone.",
                            "Confirm Delete",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            if (clsUser.DeleteUser(userID))
                            {
                                MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Don't refresh if delete failed
                            }
                        }
                        else
                        {
                            return; // User cancelled, don't refresh
                        }
                        break;
                    }

                case ucEntityManager.ManageType.Drivers:
                    {
                        int driverID = Convert.ToInt32(row["DriverID"]);
                        string driverName = row["FullName"].ToString();

                        // Confirm deletion
                        var result = MessageBox.Show(
                            $"Are you sure you want to delete driver '{driverName}'?\n\nThis will remove all driver records and associated licenses.\n\nThis action cannot be undone.",
                            "Confirm Delete",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            if (clsDriver.DeleteDriver(driverID))
                            {
                                MessageBox.Show("Driver deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete driver.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Don't refresh if delete failed
                            }
                        }
                        else
                        {
                            return; // User cancelled, don't refresh
                        }
                        break;
                    }

                case ucEntityManager.ManageType.LocalDLApplications:
                    {
                        int localDLAppID = Convert.ToInt32(row["L.D.LAppID"]);
                        string applicantName = row.Table.Columns.Contains("FullName") ? row["FullName"].ToString() : "Unknown";
                        string licenseClass = row.Table.Columns.Contains("ClassName") ? row["ClassName"].ToString() : "Unknown";

                        // Confirm deletion
                        var result = MessageBox.Show(
                            $"Are you sure you want to delete this application?\n\n" +
                            $"Applicant: {applicantName}\n" +
                            $"License Class: {licenseClass}\n\n" +
                            $"This will permanently remove both the local application and the base application.\n\n" +
                            $"This action cannot be undone.",
                            "Confirm Delete",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            bool deleted = clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(localDLAppID);
                            
                            if (deleted)
                            {
                                MessageBox.Show("Application deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete the application.\n\nThe application may have associated records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Don't refresh if delete failed
                            }
                        }
                        else
                        {
                            return; // User cancelled, don't refresh
                        }
                        break;
                    }

                case ucEntityManager.ManageType.InterDLApplications:
                    {
                        int internationalLicenseID = Convert.ToInt32(row["InternationalLicenseID"]);
                        string applicantName = row.Table.Columns.Contains("FullName") ? row["FullName"].ToString() : "Unknown";

                        // Confirm deletion
                        var result = MessageBox.Show(
                            $"Are you sure you want to delete this international license?\n\n" +
                            $"Applicant: {applicantName}\n\n" +
                            $"This action cannot be undone.",
                            "Confirm Delete",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            if (clsInternationalLicense.DeleteInternationalLicense(internationalLicenseID))
                            {
                                MessageBox.Show("International license deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete international license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Don't refresh if delete failed
                            }
                        }
                        else
                        {
                            return; // User cancelled, don't refresh
                        }
                        break;
                    }

                case ucEntityManager.ManageType.DetainLicenses:
                    {
                        int detainID = Convert.ToInt32(row["DetainID"]);
                        string licenseInfo = row.Table.Columns.Contains("LicenseID") ? $"License #{row["LicenseID"]}" : "Unknown";

                        // Confirm deletion
                        var result = MessageBox.Show(
                            $"Are you sure you want to delete this detain record?\n\n" +
                            $"{licenseInfo}\n\n" +
                            $"This action cannot be undone.",
                            "Confirm Delete",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            if (clsDetainedLicense.DeleteDetainedLicense(detainID))
                            {
                                MessageBox.Show("Detain record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete detain record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Don't refresh if delete failed
                            }
                        }
                        else
                        {
                            return; // User cancelled, don't refresh
                        }
                        break;
                    }
            }

            await manager.RefreshDataAsync();
        }

        // ---------------- LocalDL Extra ----------------
        public void HandleCancelApplication(ucEntityManager manager, DataRow row) => _ = HandleCancel(manager, row);
        public void HandleScheduleVisionTest(ucEntityManager manager, DataRow row) => HandleScheduleTest(manager, row, 1);
        public void HandleScheduleWrittenTest(ucEntityManager manager, DataRow row) => HandleScheduleTest(manager, row, 2);
        public void HandleScheduleStreetTest(ucEntityManager manager, DataRow row) => HandleScheduleTest(manager, row, 3);

        private async Task HandleCancel(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            int id = Convert.ToInt32(row["L.D.LAppID"]);
            var localApp = clsLocalDrivingLicenseApplication.Find(id);
            var application = clsApplication.Find(localApp.ApplicationID);

            if (application.CancelApplication())
                await manager.RefreshDataAsync();
        }

        private void HandleScheduleTest(ucEntityManager manager, DataRow row, int testTypeID)
        {
            int id = Convert.ToInt32(row["L.D.LAppID"]);
            MessageBox.Show($"Schedule Test {testTypeID} for Application {id}");
        }

        public void ConfigureTestMenuItems(DataRow row, ToolStripMenuItem visionMenuItem,
            ToolStripMenuItem writtenMenuItem, ToolStripMenuItem streetMenuItem)
        {
            if (row == null) return;

            try
            {
                int localDLAppID = Convert.ToInt32(row["L.D.LAppID"]);
                var localApp = clsLocalDrivingLicenseApplication.Find(localDLAppID);
                if (localApp == null) return;

                var application = clsApplication.Find(localApp.ApplicationID);
                if (application.IsCancelled() || application.IsCompleted())
                {
                    visionMenuItem.Enabled = false;
                    writtenMenuItem.Enabled = false;
                    streetMenuItem.Enabled = false;
                    return;
                }

                int passedTests = clsTest.GetPassedTestCount(localDLAppID);
                visionMenuItem.Enabled = passedTests == 0;
                writtenMenuItem.Enabled = passedTests == 1;
                streetMenuItem.Enabled = passedTests == 2;
            }
            catch
            {
                visionMenuItem.Enabled = false;
                writtenMenuItem.Enabled = false;
                streetMenuItem.Enabled = false;
            }
        }
    }
}