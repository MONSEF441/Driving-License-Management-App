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

namespace DVLD_PresentationAccess.Main.Applications.License
{
    public partial class frmLicenseHistory : Form
    {
        private int _personId = -1;

        public frmLicenseHistory(int personID)
        {
            _personId = personID;
            InitializeComponent();

            clsPerson person = clsPerson.Find(_personId);

            ucPersonCard1.LoadPerson(person);
            ucDriverLicenses1.LoadHistory(_personId);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
