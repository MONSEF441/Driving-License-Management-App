using DVLD_BusinessAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_PresentationAccess
{
    public static class Session
    {
        public static clsUser CurrentUser { get; private set; }
        public static clsPerson CurrentPerson { get; private set; }

        public static bool IsActive => CurrentUser != null;

        public static void Start(clsUser user, clsPerson person)
        {
            CurrentUser = user;
            CurrentPerson = person;
        }

        public static void End()
        {
            CurrentUser = null;
            CurrentPerson = null;
        }
       

    }


}
