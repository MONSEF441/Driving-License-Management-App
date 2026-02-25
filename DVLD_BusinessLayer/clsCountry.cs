using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessAccess
{
    public class clsCountry
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
            Mode = enMode.AddNew;
        }

        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
            Mode = enMode.Update;
        }

        private bool _AddNewCountry()
        {
            this.CountryID = clsCountriesDataAccess.AddNewCountry(CountryName);
            return CountryID != -1;
        }

        private bool _UpdateCountry()
        {
            return clsCountriesDataAccess.UpdateCountry(CountryID, CountryName);
        }

        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";

            if (clsCountriesDataAccess.GetCountryInfoByID(CountryID, ref CountryName))
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;
            if (clsCountriesDataAccess.GetCountryInfoByName(CountryName, ref CountryID))
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateCountry();
            }
            return false;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();
        }

        public static bool DeleteCountry(int CountryID)
        {
            return clsCountriesDataAccess.DeleteCountry(CountryID);
        }

        public static bool isCountryExist(int CountryID)
        {
            return clsCountriesDataAccess.IsCountryExist(CountryID);
        }

        public static bool isCountryExist(string CountryName)
        {
            return clsCountriesDataAccess.IsCountryExist(CountryName);
        }
    }
}
