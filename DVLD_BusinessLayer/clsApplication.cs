using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessAccess
{
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ApplicationID { get; set; }
        public int ApplicationTypeID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicantPersonID { get; set; }
        public byte Status { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsApplication()
        {
            ApplicationID = -1;
            ApplicationTypeID = -1;
            ApplicationDate = DateTime.MinValue;
            ApplicantPersonID = -1;
            Status=0;
            LastStatusDate = DateTime.MinValue;
            PaidFees = 0m;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsApplication(int ApplicationID, int ApplicationTypeID, DateTime ApplicationDate, int ApplicantPersonID,
                               byte Status, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicantPersonID = ApplicantPersonID;
            this.Status = Status;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsDataAccess.AddNewApplication(ApplicationTypeID, ApplicantPersonID, ApplicationDate,
                                                                            Status, LastStatusDate, PaidFees, CreatedByUserID);
            return ApplicationID != -1;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsDataAccess.UpdateApplication(ApplicationID, ApplicationTypeID, ApplicantPersonID, ApplicationDate,
                                                                            Status, LastStatusDate, PaidFees, CreatedByUserID);
        }

        public static clsApplication Find(int ApplicationID)
        {
            int ApplicationTypeID = -1;
            DateTime ApplicationDate = DateTime.MinValue;
            int ApplicantPersonID = -1;
            byte Status = 0;
            DateTime LastStatusDate = DateTime.MinValue;
            decimal PaidFees = 0m;
            int CreatedByUserID = -1;

            if (clsApplicationsDataAccess.GetApplicationInfoByID(ApplicationID, ref ApplicationTypeID, ref ApplicantPersonID, ref ApplicationDate,
                                                                ref Status, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
                return new clsApplication(ApplicationID, ApplicationTypeID, ApplicationDate, ApplicantPersonID,
                                          Status, LastStatusDate, PaidFees, CreatedByUserID);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationsDataAccess.GetAllApplications();
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationsDataAccess.DeleteApplication(ApplicationID);
        }

        public static bool isApplicationExist(int ApplicationID)
        {
            return clsApplicationsDataAccess.IsApplicationExist(ApplicationID);
        }
    }
}
