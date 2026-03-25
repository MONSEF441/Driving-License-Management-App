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
    public partial class frmLocalDLApplicationInfo : Form
    {
        private int _DLApplicationID;
        private int _ApplicationID;
        
        public frmLocalDLApplicationInfo(int DLApplicationID, int ApplicationID)
        {
            InitializeComponent();

            _DLApplicationID = DLApplicationID;
            _ApplicationID = ApplicationID;

            ucDLApplicationInfo1.LoadData(DLApplicationID);
            ucApplicationBasicInfo1.LoadData(ApplicationID);

            // Subscribe to refresh event
            ucApplicationBasicInfo1.OnPersonSaved += RefreshData;
        }
        
        private void RefreshData()
        {
            // Refresh both user controls
            ucDLApplicationInfo1.LoadData(_DLApplicationID);
            ucApplicationBasicInfo1.LoadData(_ApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
