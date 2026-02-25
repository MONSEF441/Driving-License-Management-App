using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessAccess
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClassID = -1;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            Notes = "";
            IsActive = false;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
                           DateTime IssueDate, DateTime ExpirationDate, string Notes, bool IsActive, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicensesDataAccess.AddNewLicense(ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, IsActive, CreatedByUserID);
            return LicenseID != -1;
        }

        private bool _UpdateLicense()
        {
            return clsLicensesDataAccess.UpdateLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, IsActive, CreatedByUserID);
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            string Notes = "";
            bool IsActive = false;
            int CreatedByUserID = -1;

            if (clsLicensesDataAccess.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate, ref Notes, ref IsActive, ref CreatedByUserID))
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, IsActive, CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateLicense();
            }
            return false;
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicensesDataAccess.GetAllLicenses();
        }

        public static bool DeleteLicense(int LicenseID)
        {
            return clsLicensesDataAccess.DeleteLicense(LicenseID);
        }

        public static bool isLicenseExist(int LicenseID)
        {
            return clsLicensesDataAccess.IsLicenseExist(LicenseID);
        }
    }
}
