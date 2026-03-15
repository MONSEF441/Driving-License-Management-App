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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_PresentationAccess.Main.Applications
{
    public partial class ucApplicationBasicInfo : UserControl
    {
        private int _ApplicationId;
        private clsApplication _Application ;
        private clsApplicationType _applicationtype;
        private clsPerson _person;

        public event Action OnPersonSaved;

        public ucApplicationBasicInfo()
        {
            InitializeComponent();
        }

        public void LoadData(int applicationId)
        {
            _ApplicationId = applicationId;

            if (_ApplicationId <= 0)
                return;
                
            _Application = clsApplication.Find(_ApplicationId);
            
            if (_Application == null)
                return;
                
            _person = clsPerson.Find(_Application.ApplicationPersonID);
            _applicationtype = clsApplicationType.Find(_Application.ApplicationTypeID);

            if (_Application != null)
            {
               ApplicationID.Text = _Application.ApplicationID.ToString();
               ApplicantName.Text = _person.FirstName + " " + _person.LastName ;
               Type.Text = _applicationtype.ApplicationTypeTitle ;
               Fees.Text = _applicationtype.ApplicationTypeFees.ToString();
               Status.Text = GetStatusText(_Application.ApplicationStatus);
               Date.Text = _Application.ApplicationDate.ToShortDateString();
               StatusDate.Text = _Application.LastStatusDate.ToShortDateString();
               CreatedBy.Text = clsUser.Find(_Application.CreatedByUserID)?.UserName ?? "Unknown";
            }
        }

        private string GetStatusText(byte status)
        {
            switch (status)
            {
                case 1:
                    return "New";
                case 2:
                    return "Canceled";
                case 3:
                    return "Completed";
                default:
                    return "Unknown";
            }
        }

        private void btnShowPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_person == null) return;

            bool personWasSaved = false;

            frmPersonHost frm = new frmPersonHost(frmPersonHost.EditorMode.Show, _person);
            
            // Subscribe to person saved event
            frm.PersonSaved += (savedPerson) =>
            {
                _person = savedPerson;
                personWasSaved = true;
            };
            
            frm.ShowDialog();

            // Refresh AFTER the dialog closes if person was saved
            if (personWasSaved)
            {
                LoadData(_ApplicationId); // Refresh the current control
                OnPersonSaved?.Invoke(); // Notify parent to refresh
            }
        }
    }
}
