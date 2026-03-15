using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessAccess
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int AppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }

        public clsTestAppointment()
        {
            AppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.MinValue;
            PaidFees = 0m;
            CreatedByUserID = -1;
            IsLocked = false;
            Mode = enMode.AddNew;
        }

        private clsTestAppointment(int AppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            this.AppointmentID = AppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            this.AppointmentID = clsTestAppointmentsDataAccess.AddNewTestAppointment(TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            return AppointmentID != -1;
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentsDataAccess.UpdateTestAppointment(AppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
        }

        public static clsTestAppointment Find(int AppointmentID)
        {
            int TestTypeID = -1;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.MinValue;
            decimal PaidFees = 0m;
            int CreatedByUserID = -1;
            bool IsLocked = false;

            if (clsTestAppointmentsDataAccess.GetTestAppointmentInfoByID(AppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked))
                return new clsTestAppointment(AppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateTestAppointment();
            }
            return false;
        }

        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentsDataAccess.GetAllTestAppointments();
        }

        public static bool DeleteTestAppointment(int AppointmentID)
        {
            return clsTestAppointmentsDataAccess.DeleteTestAppointment(AppointmentID);
        }

        public static bool isTestAppointmentExist(int AppointmentID)
        {
            return clsTestAppointmentsDataAccess.IsTestAppointmentExist(AppointmentID);
        }

        public static DataTable GetTestAppointmentsByLocalDLAppAndTestType(int localDLApplicationID, int testTypeID)
        {
            return clsTestAppointmentsDataAccess.GetTestAppointmentsByLocalDLAppAndTestType(localDLApplicationID, testTypeID);
        }
        
    }
}
