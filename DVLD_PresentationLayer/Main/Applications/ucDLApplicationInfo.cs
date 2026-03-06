using DVLD_BusinessAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Applications
{
    public partial class ucDLApplicationInfo : UserControl
    {
        private int _DLApplicationID;
        private clsLocalDrivingLicenseApplication _DLApplication ;
        
        public ucDLApplicationInfo()
        {
            InitializeComponent();
        }
        
        
        public void LoadData(int dlapplicationid)
        {
            _DLApplicationID = dlapplicationid;

            if (_DLApplicationID <= 0)
                return;
                
            _DLApplication = clsLocalDrivingLicenseApplication.Find(_DLApplicationID);
            
            if (_DLApplication == null)
                return;
            
             DLAppID.Text = _DLApplicationID.ToString();
             LicenseName.Text = clsLicenseClass.Find(_DLApplication.LicenseClassID).ClassName; 
             PassedTests.Text = $"{clsTest.GetPassedTestCount(_DLApplicationID)}/3";
        }

        private void btnShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("License class details would be shown here.", "License Class Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
