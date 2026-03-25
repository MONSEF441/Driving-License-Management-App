using DVLD_BusinessAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class ucProfile : UserControl
    {
        public bool IsLoaded { get; private set; } = false;
        public void MarkAsLoaded() => IsLoaded = true;
        public void ResetLoaded() => IsLoaded = false;


        public ucProfile()
        {
            InitializeComponent();
        }

        public void LoadProfile(clsUser user)
        {

            var person = clsPerson.Find(user.PersonID);
            ucPersonCard1.LoadPerson(person);
            ucUserCard1.LoadUser(user);

            IsLoaded = true;
        }
        public event Action<clsPerson> EditPersonRequested
        {
            add { ucPersonCard1.EditRequested += value; }
            remove { ucPersonCard1.EditRequested -= value; }
        }




    }
}
