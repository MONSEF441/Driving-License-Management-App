using DVLD_DataAccess;
using System;
using System.Data;

namespace DVLD_BusinessAccess
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }


        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = false;
            Notes = string.Empty;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }

        private clsTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes ?? string.Empty;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Update;
        }

        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = string.Empty;
            int CreatedByUserID = -1;

            if (clsTestsDataAccess.GetTestInfoByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            else
                return null;
        }

        public static bool IsTestPassed(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestsDataAccess.IsTestPassed(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static int GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestsDataAccess.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static byte GetTestTrialCount(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestsDataAccess.GetTestTrialCount(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestsDataAccess.AddNewTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);
            return TestID != -1;
        }

        private bool _UpdateTest()
        {
            return clsTestsDataAccess.UpdateTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateTest();
            }
            return false;
        }

        public static bool DeleteTest(int TestID)
        {
            return clsTestsDataAccess.DeleteTest(TestID);
        }

        public static bool IsTestExist(int TestID)
        {
            return clsTestsDataAccess.IsTestExist(TestID);
        }

        public static DataTable GetAllTests()
        {
            return clsTestsDataAccess.GetAllTests();
        }

        public static int GetTestIDByTestAppointmentID(int TestAppointmentID)
        {
            return clsTestsDataAccess.GetTestIDByTestAppointmentID(TestAppointmentID);
        }
    }
}
