using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessAccess
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationTypeFees { get; set; }

        public clsApplicationType()
        {
            ApplicationTypeID = -1;
            ApplicationTypeTitle = "";
            ApplicationTypeFees = 0m;
            Mode = enMode.AddNew;
        }

        private clsApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationTypeFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationTypeFees = ApplicationTypeFees;
            Mode = enMode.Update;
        }

        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            decimal ApplicationTypeFees = 0m;

            if (clsApplicationTypesDataAccess.GetApplicationTypeInfoByID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationTypeFees))
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFees);
            else
                return null;
        }

        public static clsApplicationType FindInternationalApplicationType()
        {
            // Common setup in DVLD projects
            clsApplicationType byId = Find(6);
            if (byId != null)
                return byId;

            DataTable dt = GetAllApplicationTypes();
            if (dt == null || dt.Rows.Count == 0)
                return null;

            if (!dt.Columns.Contains("ApplicationTypeID") || !dt.Columns.Contains("ApplicationTypeTitle"))
                return null;

            foreach (DataRow row in dt.Rows)
            {
                string title = row["ApplicationTypeTitle"]?.ToString() ?? string.Empty;
                if (title.IndexOf("international", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    int id = Convert.ToInt32(row["ApplicationTypeID"]);
                    return Find(id);
                }
            }

            return null;
        }

        public static clsApplicationType FindRenewLocalApplicationType()
        {
            DataTable dt = GetAllApplicationTypes();
            if (dt == null || dt.Rows.Count == 0)
                return null;

            if (!dt.Columns.Contains("ApplicationTypeID") || !dt.Columns.Contains("ApplicationTypeTitle"))
                return null;

            foreach (DataRow row in dt.Rows)
            {
                string title = row["ApplicationTypeTitle"]?.ToString() ?? string.Empty;
                if (title.IndexOf("renew", StringComparison.OrdinalIgnoreCase) >= 0
                    && title.IndexOf("local", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    int id = Convert.ToInt32(row["ApplicationTypeID"]);
                    return Find(id);
                }
            }

            return null;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesDataAccess.GetAllApplicationTypes();
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesDataAccess.UpdateApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    return false;
                case enMode.Update:
                    return _UpdateApplicationType();
            }
            return false;
        }
    }
}
