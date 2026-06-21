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

        // store original loaded password (hashed from DB) to detect changes
        private string _originalPassword;

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
            Mode = enMode.AddNew;
            _originalPassword = Password;
        }

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            Mode = enMode.Update;

            // keep original loaded (hashed or legacy plain) password for comparison on updates
            _originalPassword = Password;
        }

        private bool _AddNewUser()
        {
            string plain = Password ?? string.Empty;
            string hashed = string.IsNullOrEmpty(plain) ? "" : clsGlobal.ComputeHash(plain);

            // send hashed to DB
            this.UserID = clsUsersDataAccess.AddNewUser(PersonID, UserName, hashed, IsActive);

            if (UserID != -1)
            {
                // store hashed as original but keep plain in-memory for presentation compatibility
                _originalPassword = hashed;
                this.Password = plain;
                return true;
            }

            return false;
        }

        private bool _UpdateUser()
        {
            string plain = Password ?? string.Empty;

            // Determine if password was changed by comparing plain to stored hashed (they'll differ)
            bool passwordChanged;
            if (string.IsNullOrEmpty(_originalPassword))
            {
                // legacy: no original hashed stored
                passwordChanged = true;
            }
            else
            {
                // If plain, when hashed, equals original then no change; otherwise change
                string hashedPlain = clsGlobal.ComputeHash(plain);
                passwordChanged = !string.Equals(hashedPlain, _originalPassword, StringComparison.OrdinalIgnoreCase);
            }

            string valueToStore = _originalPassword; // default: keep existing stored value
            if (passwordChanged)
            {
                valueToStore = string.IsNullOrEmpty(plain) ? "" : clsGlobal.ComputeHash(plain);
            }

            bool result = clsUsersDataAccess.UpdateUser(UserID, PersonID, UserName, valueToStore, IsActive);

            if (result)
            {
                // update original hashed value, but keep plain in-memory for presentation compatibility
                _originalPassword = valueToStore ?? string.Empty;
                this.Password = plain;
            }

            return result;
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
            // normalize inputs
            string userNameSafe = (username ?? string.Empty).Trim();
            string plain = (password ?? string.Empty).Trim();
            string hashedInput = clsGlobal.ComputeHash(plain).Trim();

            // 1) Try DB login with hashed password (expected normal path)
            if (clsUsersDataAccess.IsLoginValid(userNameSafe, hashedInput))
            {
                clsUser user = Find(userNameSafe);
                if (user == null) return null;

                // track original hashed value and keep plain password in-memory so presentation layer remains compatible
                user._originalPassword = (user.Password ?? string.Empty).Trim();
                user.Password = plain;
                return user;
            }

            // 2) Legacy: try DB login with plaintext (if DB still stores plain values)
            if (clsUsersDataAccess.IsLoginValid(userNameSafe, plain))
            {
                clsUser user = Find(userNameSafe);
                if (user == null) return null;

                // attempt to upgrade stored plaintext password to hashed value
                string newHashed = hashedInput;
                bool updated = clsUsersDataAccess.UpdateUser(user.UserID, user.PersonID, user.UserName, newHashed, user.IsActive);

                if (updated)
                {
                    user._originalPassword = newHashed;
                }
                // keep plain in-memory for presentation compatibility
                user.Password = plain;
                return user;
            }

            // not authenticated
            return null;
        }

        // Validate a plain-text password against stored hashed password
        public bool ValidatePassword(string plainPassword)
        {
            if (plainPassword == null) plainPassword = "";
            string hashed = clsGlobal.ComputeHash(plainPassword).Trim();
            string storedHashed = (_originalPassword ?? string.Empty).Trim();

            // if we have a tracked original hashed value, compare to it; otherwise fall back to comparing to current Password
            if (!string.IsNullOrEmpty(storedHashed))
                return string.Equals(hashed, storedHashed, StringComparison.OrdinalIgnoreCase);

            // legacy fallback: compare hashed input to current Password (if Password actually holds a hash)
            string curr = (this.Password ?? string.Empty).Trim();
            return string.Equals(hashed, curr, StringComparison.OrdinalIgnoreCase);
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

        public static bool isPersonUser(int PersonID)
        {
            return clsUsersDataAccess.isPersonUser(PersonID);
        }

        public static bool isLoginValid(string UserName, string Password)
        {
            return clsUsersDataAccess.IsLoginValid(UserName, Password);
        }
    }
}
