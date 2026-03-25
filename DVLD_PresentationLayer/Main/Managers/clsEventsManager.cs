using DVLD_BusinessAccess;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
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

        // ---------------- CRUD Handlers ----------------
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
                case ucEntityManager.ManageType.LocalDLApplications:
                    {
                        int localDLID = Convert.ToInt32(row["L.D.LAppID"]);
                        var localApp = clsLocalDrivingLicenseApplication.Find(localDLID);
                        if (localApp != null)
                        {
                            int appID = localApp.ApplicationID;
                            using var frm = new frmLocalDLApplicationInfo(localDLID, appID);
                            frm.ShowDialog();
                            
                            // Refresh the table after closing the form
                            _ = manager.RefreshDataAsync();
                        }
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
                case ucEntityManager.ManageType.People:
                    {
                        int id = Convert.ToInt32(row["PersonID"]);
                        var entity = clsPerson.Find(id);
                        var frm = new frmPersonHost(frmPersonHost.EditorMode.Edit, entity);
                        frm.PersonSaved += async _ => await manager.RefreshDataAsync();
                        frm.ShowDialog();
                        break;
                    }
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
                                return; 
                            }
                        }
                        else
                        {
                            return; 
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
                                return; 
                            }
                        }
                        else
                        {
                            return; 
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
                                return; 
                            }
                        }
                        else
                        {
                            return; 
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
                                return;
                            }
                        }
                        else
                        {
                            return; 
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
                                return; 
                            }
                        }
                        else
                        {
                            return; 
                        }
                        break;
                    }
            }

            await manager.RefreshDataAsync();
        }

        // ---------------- Local DL Extra ----------------
        public void HandleCancelApplication(ucEntityManager manager, DataRow row) => _ = HandleCancel(manager, row);
        public void HandleScheduleVisionTest(ucEntityManager manager, DataRow row) => HandleScheduleTest(manager, row, frmScheduleAppointment.AppontmentType.VisionTest);
        public void HandleScheduleWrittenTest(ucEntityManager manager, DataRow row) => HandleScheduleTest(manager, row, frmScheduleAppointment.AppontmentType.WritingTest);
        public void HandleScheduleStreetTest(ucEntityManager manager, DataRow row) => HandleScheduleTest(manager, row, frmScheduleAppointment.AppontmentType.StreetTest);

        private async Task HandleCancel(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            int id = Convert.ToInt32(row["L.D.LAppID"]);
            var localApp = clsLocalDrivingLicenseApplication.Find(id);
            var application = clsApplication.Find(localApp.ApplicationID);

            if (application.CancelApplication())
                await manager.RefreshDataAsync();
        }

        private void HandleScheduleTest(ucEntityManager manager, DataRow row, frmScheduleAppointment.AppontmentType testMode)
        {
            if (row == null) return;

            int localDLID = Convert.ToInt32(row["L.D.LAppID"]);
            var localApp = clsLocalDrivingLicenseApplication.Find(localDLID);
            
            if (localApp == null)
            {
                MessageBox.Show("Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int appID = localApp.ApplicationID;
            
            using var frm = new frmScheduleAppointment(testMode, localDLID, appID);
            frm.ShowDialog();
            
            // Refresh the table after closing the form
            _ = manager.RefreshDataAsync();
        }

        public void HandleIssueDL(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            int localDLID = Convert.ToInt32(row["L.D.LAppID"]);
            var localApp = clsLocalDrivingLicenseApplication.Find(localDLID);

            if (localApp == null)
            {
                MessageBox.Show("Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int appID = localApp.ApplicationID;

            using var frm = new DVLD_PresentationAccess.frmIssueDL(localDLID, appID);
            frm.ShowDialog();

            // Refresh the table after closing the form
            _ = manager.RefreshDataAsync();
        }

        public void HandleShowLicense(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            int localDLID = Convert.ToInt32(row["L.D.LAppID"]);
            var localApp = clsLocalDrivingLicenseApplication.Find(localDLID);

            if (localApp == null)
            {
                MessageBox.Show("Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var license = clsLicense.FindByApplicationID(localApp.ApplicationID);

            if (license == null)
            {
                MessageBox.Show("No issued license found for this application.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var frm = new DVLD_PresentationAccess.frmShowLocalLicense(license.LicenseID);
            frm.ShowDialog();

            _ = manager.RefreshDataAsync();
        }

        public void HandleShowPersonLicenseHistory(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            int localDLID = Convert.ToInt32(row["L.D.LAppID"]);
            var localApp = clsLocalDrivingLicenseApplication.Find(localDLID);

            if (localApp == null)
            {
                MessageBox.Show("Application not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var application = clsApplication.Find(localApp.ApplicationID);
            if (application == null)
            {
                MessageBox.Show("Application details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int personID = application.ApplicationPersonID;

            using var frm = new DVLD_PresentationAccess.frmLicenseHistory(personID);
            frm.ShowDialog();

            _ = manager.RefreshDataAsync();
        }

        // ---------------- International DL Extra ----------------

        public void HandleInterShowPersonDetails(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            int driverID = Convert.ToInt32(row["DriverID"]);
            var driver = clsDriver.Find(driverID);

            if (driver == null)
            {
                MessageBox.Show("Driver not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var person = clsPerson.Find(driver.PersonID);
            if (person == null)
            {
                MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var frm = new frmPersonHost(frmPersonHost.EditorMode.Show, person);
            frm.ShowDialog();

            _ = manager.RefreshDataAsync();
        }

        public void HandleInterShowLicenseDetails(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            int internationalLicenseID = -1;

            if (row.Table.Columns.Contains("InternationalLicenseID"))
                internationalLicenseID = Convert.ToInt32(row["InternationalLicenseID"]);
            else if (row.Table.Columns.Contains("Int.LicenseID"))
                internationalLicenseID = Convert.ToInt32(row["Int.LicenseID"]);

            if (internationalLicenseID <= 0)
            {
                MessageBox.Show("International license ID was not found for this row.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var frm = new DVLD_PresentationAccess.frmShowInternationalLicense(internationalLicenseID);
            frm.ShowDialog();

            _ = manager.RefreshDataAsync();
        }

        public void HandleInterShowPersonLicenseHistory(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            int driverID = Convert.ToInt32(row["DriverID"]);
            var driver = clsDriver.Find(driverID);

            if (driver == null)
            {
                MessageBox.Show("Driver not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var frm = new DVLD_PresentationAccess.frmLicenseHistory(driver.PersonID);
            frm.ShowDialog();

            _ = manager.RefreshDataAsync();
        }
        
        // ---------------- Detain Licenses: open forms ----------------
        public void HandleOpenDetainForm(ucEntityManager manager)
        {
            using var frm = new DVLD_PresentationAccess.frmDetainLicense();
            frm.ShowDialog();
            _ = manager.RefreshDataAsync();
        }

        public void HandleOpenReleaseForm(ucEntityManager manager, DataRow row)
        {
            if (row == null)
            {
                using var frm1 = new DVLD_PresentationAccess.frmReleaseLicense();
                frm1.ShowDialog();
                _ = manager.RefreshDataAsync();
                return;
            }

            if (!CanReleaseDetainedLicense(row))
            {
                MessageBox.Show("This detained license is already released.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int licenseID = GetFirstIntColumnValue(row, "LicenseID", "LocalLicenseID", "L.LicenseID");
            if (licenseID <= 0)
            {
                MessageBox.Show("License ID not found for selected row.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var frm = new DVLD_PresentationAccess.frmReleaseLicense(licenseID);
            frm.ShowDialog();
            _ = manager.RefreshDataAsync();
        }

        // ---------------- Detain Licenses: context menu actions ----------------
        public void HandleDetainShowLicenseDetails(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            int licenseID = GetFirstIntColumnValue(row, "LicenseID", "LocalLicenseID", "L.LicenseID");
            if (licenseID <= 0)
            {
                MessageBox.Show("License ID not found for selected row.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var frm = new DVLD_PresentationAccess.frmShowLocalLicense(licenseID);
            frm.ShowDialog();

            _ = manager.RefreshDataAsync();
        }

        public void HandleDetainShowPersonDetails(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            if (!TryGetPersonFromDetainRow(row, out clsPerson person))
            {
                MessageBox.Show("Person details were not found for selected row.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var frm = new frmPersonHost(frmPersonHost.EditorMode.Show, person);
            frm.ShowDialog();

            _ = manager.RefreshDataAsync();
        }

        public void HandleDetainShowPersonLicenseHistory(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            if (!TryGetPersonFromDetainRow(row, out clsPerson person))
            {
                MessageBox.Show("Person was not found for selected row.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var frm = new DVLD_PresentationAccess.frmLicenseHistory(person.PersonID);
            frm.ShowDialog();

            _ = manager.RefreshDataAsync();
        }

        // ---------------- Detain Licenses: helper methods ----------------
        private bool TryGetPersonFromDetainRow(DataRow row, out clsPerson person)
        {
            person = null;

            int licenseID = GetFirstIntColumnValue(row, "LicenseID", "LocalLicenseID", "L.LicenseID");
            if (licenseID <= 0)
                return false;

            clsLicense license = clsLicense.Find(licenseID);
            if (license == null)
                return false;

            clsDriver driver = clsDriver.Find(license.DriverID);
            if (driver == null)
                return false;

            person = clsPerson.Find(driver.PersonID);
            return person != null;
        }

        private int GetFirstIntColumnValue(DataRow row, params string[] columnNames)
        {
            foreach (string col in columnNames)
            {
                if (row.Table.Columns.Contains(col) && row[col] != DBNull.Value)
                    return Convert.ToInt32(row[col]);
            }

            return -1;
        }

        private bool GetFirstBoolColumnValue(DataRow row, params string[] columnNames)
        {
            foreach (string col in columnNames)
            {
                if (!row.Table.Columns.Contains(col) || row[col] == DBNull.Value)
                    continue;

                object value = row[col];

                if (value is bool boolValue)
                    return boolValue;

                if (bool.TryParse(value.ToString(), out bool parsedBool))
                    return parsedBool;

                if (int.TryParse(value.ToString(), out int parsedInt))
                    return parsedInt != 0;
            }

            return false;
        }

        public bool CanReleaseDetainedLicense(DataRow row)
        {
            if (row == null)
                return false;

            bool isReleased = GetFirstBoolColumnValue(row, "IsReleased", "isReleased", "Released");
            return !isReleased;
        }

        // ---------------- Drivers Extra ----------------
        public void HandleDriverShowPersonDetails(ucEntityManager manager, DataRow row)
        {
            HandleInterShowPersonDetails(manager, row);
        }

        public void HandleDriverIssueInternationalLicense(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            int driverID = Convert.ToInt32(row["DriverID"]);
            var driver = clsDriver.Find(driverID);

            if (driver == null)
            {
                MessageBox.Show("Driver not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var activeInternational = clsInternationalLicense.FindActiveByDriverID(driverID);
            if (activeInternational != null)
            {
                MessageBox.Show(
                    $"This driver already has an active international license (ID = {activeInternational.InternationalLicenseID}).",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int activeLocalLicenseId = GetActiveLocalLicenseIdForDriver(driverID);
            if (activeLocalLicenseId <= 0)
            {
                MessageBox.Show(
                    "This driver does not have an active local license.\nCannot issue international license.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using var frm = new frmNewInternationalLicense(activeLocalLicenseId, true);
            frm.ShowDialog();

            _ = manager.RefreshDataAsync();
        }

        public void HandleDriverShowPersonLicenseHistory(ucEntityManager manager, DataRow row)
        {
            HandleInterShowPersonLicenseHistory(manager, row);
        }

        private int GetActiveLocalLicenseIdForDriver(int driverID)
        {
            DataTable dt = clsLicense.GetAllLicenses();
            if (dt == null || dt.Rows.Count == 0)
                return -1;

            int selectedLicenseId = -1;
            DateTime latestIssueDate = DateTime.MinValue;

            foreach (DataRow row in dt.Rows)
            {
                int rowDriverId = GetFirstIntColumnValue(row, "DriverID");
                if (rowDriverId != driverID)
                    continue;

                bool isActive = GetFirstBoolColumnValue(row, "IsActive", "isActive", "Active");
                if (!isActive)
                    continue;

                if (row.Table.Columns.Contains("ExpirationDate") && row["ExpirationDate"] != DBNull.Value)
                {
                    DateTime exp = Convert.ToDateTime(row["ExpirationDate"]);
                    if (exp.Date < DateTime.Now.Date)
                        continue;
                }

                int licenseId = GetFirstIntColumnValue(row, "LicenseID", "LocalLicenseID", "L.LicenseID");
                if (licenseId <= 0)
                    continue;

                DateTime issueDate = DateTime.MinValue;
                if (row.Table.Columns.Contains("IssueDate") && row["IssueDate"] != DBNull.Value)
                    issueDate = Convert.ToDateTime(row["IssueDate"]);

                if (issueDate >= latestIssueDate)
                {
                    latestIssueDate = issueDate;
                    selectedLicenseId = licenseId;
                }
            }

            return selectedLicenseId;
        }
    }
}