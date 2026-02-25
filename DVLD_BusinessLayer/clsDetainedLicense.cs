using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessAccess
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleasedDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }

        public clsDetainedLicense()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.MinValue;
            FineFees = 0m;
            IsReleased = false;
            ReleasedDate = DateTime.MinValue;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;
            Mode = enMode.AddNew;
        }

        private clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, decimal FineFees, bool IsReleased, DateTime ReleasedDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.IsReleased = IsReleased;
            this.ReleasedDate = ReleasedDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            Mode = enMode.Update;
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicensesDataAccess.AddNewDetainedLicense(LicenseID, DetainDate, FineFees, IsReleased, ReleasedDate, ReleasedByUserID, ReleaseApplicationID);
            return DetainID != -1;
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicensesDataAccess.UpdateDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, IsReleased, ReleasedDate, ReleasedByUserID, ReleaseApplicationID);
        }

        public static clsDetainedLicense Find(int DetainID)
        {
            int LicenseID = -1;
            DateTime DetainDate = DateTime.MinValue;
            decimal FineFees = 0m;
            bool IsReleased = false;
            DateTime ReleasedDate = DateTime.MinValue;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (clsDetainedLicensesDataAccess.GetDetainedLicenseInfoByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref IsReleased, ref ReleasedDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, IsReleased, ReleasedDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateDetainedLicense();
            }
            return false;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesDataAccess.GetAllDetainedLicenses();
        }

        public static bool DeleteDetainedLicense(int DetainID)
        {
            return clsDetainedLicensesDataAccess.DeleteDetainedLicense(DetainID);
        }

        public static bool isDetainedLicenseExist(int DetainID)
        {
            return clsDetainedLicensesDataAccess.IsDetainedLicenseExist(DetainID);
        }
    }
}
