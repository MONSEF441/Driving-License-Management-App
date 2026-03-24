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
        public int IssueReason { get; set; }      // 1=FirstTime, 2=Renew
        public decimal PaidFees { get; set; }
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
            IssueReason = 1;
            PaidFees = 0m;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
                           DateTime IssueDate, DateTime ExpirationDate, string Notes, bool IsActive,
                           int IssueReason, decimal PaidFees, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        // -------- Business rules --------
     

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicensesDataAccess.AddNewLicense(
                ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate,
                Notes, IsActive, IssueReason, PaidFees, CreatedByUserID);

            return LicenseID != -1;
        }

        private bool _UpdateLicense()
        {
            return clsLicensesDataAccess.UpdateLicense(
                LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate,
                Notes, IsActive, IssueReason, PaidFees, CreatedByUserID);
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
            int IssueReason = 1;
            decimal PaidFees = 0m;
            int CreatedByUserID = -1;

            if (clsLicensesDataAccess.GetLicenseInfoByID(
                LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate,
                ref ExpirationDate, ref Notes, ref IsActive, ref IssueReason, ref PaidFees, ref CreatedByUserID))
            {
                return new clsLicense(
                    LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate,
                    Notes, IsActive, IssueReason, PaidFees, CreatedByUserID);
            }

            return null;
        }

        public static clsLicense FindByApplicationID(int applicationID)
        {
            int licenseID = -1;
            int driverID = -1;
            int licenseClassID = -1;
            DateTime issueDate = DateTime.MinValue;
            DateTime expirationDate = DateTime.MinValue;
            string notes = "";
            bool isActive = false;
            int issueReason = 1;
            decimal paidFees = 0m;
            int createdByUserID = -1;

            if (clsLicensesDataAccess.GetLicenseInfoByApplicationID(
                applicationID, ref licenseID, ref driverID, ref licenseClassID, ref issueDate,
                ref expirationDate, ref notes, ref isActive, ref issueReason, ref paidFees, ref createdByUserID))
            {
                return new clsLicense(
                    licenseID, applicationID, driverID, licenseClassID, issueDate, expirationDate,
                    notes, isActive, issueReason, paidFees, createdByUserID);
            }

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

        public static DataTable GetAllLicenses() => clsLicensesDataAccess.GetAllLicenses();
        public static bool DeleteLicense(int LicenseID) => clsLicensesDataAccess.DeleteLicense(LicenseID);
        public static bool isLicenseExist(int LicenseID) => clsLicensesDataAccess.IsLicenseExist(LicenseID);

        public bool IsExpired() => ExpirationDate.Date <= DateTime.Now.Date;

        public bool CanBeRenewed(out string reason)
        {
            reason = string.Empty;

            if (!IsActive)
            {
                reason = "Selected license is not active anymore and cannot be renewed.";
                return false;
            }

            if (!IsExpired())
            {
                reason = "Selected license is not expired yet.";
                return false;
            }

            return true;
        }

        public bool Deactivate()
        {
            IsActive = false;
            return Save();
        }
    }
}
