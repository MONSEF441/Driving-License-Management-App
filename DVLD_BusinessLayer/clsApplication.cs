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
        public int ApplicationPersonID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsApplication()
        {
            ApplicationID = -1;
            ApplicationTypeID = -1;
            ApplicationDate = DateTime.MinValue;
            ApplicationPersonID = -1;
            ApplicationStatus = 0;
            LastStatusDate = DateTime.MinValue;
            PaidFees = 0m;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsApplication(int ApplicationID, int ApplicationTypeID, DateTime ApplicationDate, int ApplicationPersonID,
                               byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationPersonID = ApplicationPersonID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsDataAccess.AddNewApplication(ApplicationTypeID, ApplicationPersonID, ApplicationDate,
                                                                            ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            return ApplicationID != -1;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsDataAccess.UpdateApplication(ApplicationID, ApplicationTypeID, ApplicationPersonID, ApplicationDate,
                                                                            ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
        }

        public static clsApplication Find(int ApplicationID)
        {
            int ApplicationTypeID = -1;
            DateTime ApplicationDate = DateTime.MinValue;
            int ApplicationPersonID = -1;
            byte ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.MinValue;
            decimal PaidFees = 0m;
            int CreatedByUserID = -1;

            if (clsApplicationsDataAccess.GetApplicationInfoByID(ApplicationID, ref ApplicationTypeID, ref ApplicationPersonID, ref ApplicationDate,
                                                                ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
                return new clsApplication(ApplicationID, ApplicationTypeID, ApplicationDate, ApplicationPersonID,
                                          ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
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

        public bool CancelApplication()
        {
            if (ApplicationID == -1) return false;

            // ApplicationStatus 2 = Cancelled
            this.ApplicationStatus = 2;
            this.LastStatusDate = DateTime.Now;

            return Save();
        }

        public bool IsActive()
        {
            // ApplicationStatus: 1=New, 2=Cancelled, 3=Completed
            return ApplicationStatus != 2;
        }

        public bool IsNew()
        {
            return ApplicationStatus == 1;
        }

        public bool IsCancelled()
        {
            return ApplicationStatus == 2;
        }

        public bool IsCompleted()
        {
            return ApplicationStatus == 3;
        }

     
    }
}
