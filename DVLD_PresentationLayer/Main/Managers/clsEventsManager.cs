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
        // ---------------- Wire / Unwire ----------------
        public void WireEvents(ucEntityManager manager)
        {
            if (manager == null) return;

            manager.ShowRequested += row => HandleShow(manager, row);
            manager.AddRequested += () => HandleAdd(manager);
            manager.EditRequested += row => HandleEdit(manager, row);
            manager.DeleteRequested += row => _ = HandleDelete(manager, row); // async safely wrapped
        }

        public void UnwireEvents(ucEntityManager manager)
        {
            if (manager == null) return;

            manager.ShowRequested -= row => HandleShow(manager, row);
            manager.AddRequested -= () => HandleAdd(manager);
            manager.EditRequested -= row => HandleEdit(manager, row);
            manager.DeleteRequested -= row => _ = HandleDelete(manager, row);
        }

        // ---------------- Handlers ----------------
        private void HandleShow(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            switch (manager.ManageMode)
            {
                case ucEntityManager.ManageType.People:
                    {
                        int personID = Convert.ToInt32(row["PersonID"]);
                        var person = clsPerson.Find(personID);
                        using var frm = new frmPersonHost(frmPersonHost.EditorMode.Show, person);
                        frm.ShowDialog();
                        break;
                    }

                case ucEntityManager.ManageType.Users:
                    {
                        int userID = Convert.ToInt32(row["UserID"]);
                        var user = clsUser.Find(userID);
                        using var frm = new frmUserEdit(frmUserEdit.EditorMode.Edit, user);
                        frm.ShowDialog();
                        break;
                    }

                case ucEntityManager.ManageType.Drivers:
                    // Add Drivers form here if needed
                    break;
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

                case ucEntityManager.ManageType.Drivers:
                    // Add Drivers form here if needed
                    break;
            }
        }

        private void HandleEdit(ucEntityManager manager, DataRow row)
        {
            if (row == null) return;

            switch (manager.ManageMode)
            {
                case ucEntityManager.ManageType.People:
                    {
                        int personID = Convert.ToInt32(row["PersonID"]);
                        var person = clsPerson.Find(personID);
                        var frm = new frmPersonHost(frmPersonHost.EditorMode.Edit, person);
                        frm.PersonSaved += async _ => await manager.RefreshDataAsync();
                        frm.ShowDialog();
                        break;
                    }

                case ucEntityManager.ManageType.Users:
                    {
                        int userID = Convert.ToInt32(row["UserID"]);
                        var user = clsUser.Find(userID);
                        var frm = new frmUserEdit(frmUserEdit.EditorMode.Edit, user);
                        frm.UserSaved += async _ => await manager.RefreshDataAsync();
                        frm.ShowDialog();
                        break;
                    }

                case ucEntityManager.ManageType.Drivers:
                    // Add Drivers form here if needed
                    break;

                case ucEntityManager.ManageType.ApplicationTypes:
                    {
                        int ApplicationTypeID = Convert.ToInt32(row["ApplicationTypeID"]);
                        var ApplicationType = clsApplicationType.Find(ApplicationTypeID);

                        if (ApplicationType == null)
                            MessageBox.Show("Find returned NULL!");

                        var frm = new frmEditApplicationTypes(ApplicationType);
                        frm.ApplicationTypeSaved += async _ => await manager.RefreshDataAsync();
                        frm.ShowDialog();
                        break;
                    }
                case ucEntityManager.ManageType.TestTypes:
                    {
                        int TestTypeID = Convert.ToInt32(row["TestTypeID"]);
                        var TestType = clsTestType.Find(TestTypeID);

                        if (TestType == null)
                            MessageBox.Show("Find returned NULL!");

                        var frm = new frmEditTestTypes(TestType);
                        frm.TestTypeSaved += async _ => await manager.RefreshDataAsync();
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
                        if (!clsPerson.CanDelete(personID))
                        {
                            MessageBox.Show("Cannot delete this person.");
                            return;
                        }
                        clsPerson.DeletePerson(personID);
                        break;
                    }

                case ucEntityManager.ManageType.Users:
                    {
                        int userID = Convert.ToInt32(row["UserID"]);
                        clsUser.DeleteUser(userID);
                        break;
                    }

                case ucEntityManager.ManageType.Drivers:
                    {
                        int driverID = Convert.ToInt32(row["DriverID"]);
                        clsDriver.DeleteDriver(driverID);
                        break;
                    }
            }

            await manager.RefreshDataAsync();
        }
    }
}
