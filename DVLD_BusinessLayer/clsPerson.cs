using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_BusinessAccess
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string CIN { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        public clsPerson()
        {
            PersonID = -1;
            CIN = "0";
            FirstName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gender = true;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";

            Mode = enMode.AddNew;
        }

        private clsPerson(
            int PersonID, string CIN, string FirstName, string LastName, DateTime DateOfBirth, bool Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.CIN = CIN;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;

            Mode = enMode.Update;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonDataAccess.AddNewPerson(
                CIN, FirstName,LastName, DateOfBirth, Gender, Address,
                Phone, Email, NationalityCountryID, ImagePath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonDataAccess.UpdatePerson(
                PersonID, CIN, FirstName, LastName, DateOfBirth, Gender, Address,
                Phone, Email, NationalityCountryID, ImagePath);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }

        public static clsPerson Find(int PersonID)
        {
            string CIN = "";
            string FirstName = "", LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            bool Gender = true;
            string Address = "", Phone = "", Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";

            bool isFound = clsPersonDataAccess.GetPersonInfoByID(
                PersonID, ref CIN, ref FirstName, ref LastName, ref DateOfBirth,
                ref Gender, ref Address, ref Phone, ref Email,
                ref NationalityCountryID, ref ImagePath);

            if (isFound)
            {
                return new clsPerson(PersonID, CIN, FirstName,
                    LastName, DateOfBirth, Gender, Address,
                    Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonDataAccess.GetAllPeople();
        }

        public static bool CanDelete(int personID)
        {
            // If he is a user → block delete
            return !clsPersonDataAccess.IsUser(personID);
        }

        public static bool DeletePerson(int personID)
        {
            if (!CanDelete(personID))
                return false;

            return clsPersonDataAccess.DeletePerson(personID);
        }

        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonDataAccess.IsPersonExist(PersonID);
        }
        public static bool CINExist(string cin, int excludePersonID = -1)
        {
            return clsPersonDataAccess.IsCINExist(cin,excludePersonID);
        }

    }
}
