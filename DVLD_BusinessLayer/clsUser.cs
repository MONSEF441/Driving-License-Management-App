using System;
using System.Data;
using System.IdentityModel.Selectors;
using DVLD_DataAccess;

namespace DVLD_BusinessAccess
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
            Mode = enMode.AddNew;
        }

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUsersDataAccess.AddNewUser(PersonID, UserName, Password, IsActive);
            return UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsUsersDataAccess.UpdateUser(UserID, PersonID, UserName, Password, IsActive);
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;

            if (clsUsersDataAccess.GetUserInfoByID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }
        public static clsUser Find(string SearchUserName)
        {
            int UserID = -1;
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;

            if (clsUsersDataAccess.GetUserInfoByUserName(SearchUserName ,ref UserID, ref PersonID , ref UserName , ref Password, ref IsActive))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }
        public static clsUser Login(string username, string password)
        {
            clsUser user = Find(username);

            if (user == null)
                return null;

            if (user.Password == password)
                return user;

            return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersDataAccess.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUsersDataAccess.DeleteUser(UserID);
        }

        public static bool isUserExist(int UserID)
        {
            return clsUsersDataAccess.IsUserExist(UserID);
        }

        public static bool isUserExist(string UserName)
        {
            return clsUsersDataAccess.IsUserExist(UserName);
        }

        public static bool isLoginValid(string UserName, string Password)
        {
            return clsUsersDataAccess.IsLoginValid(UserName, Password);
        }


       

    }
}
