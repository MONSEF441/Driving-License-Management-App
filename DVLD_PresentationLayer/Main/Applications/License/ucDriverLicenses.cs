using DVLD_BusinessAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DVLD_PresentationAccess
{
    public partial class ucDriverLicenses : UserControl
    {
        public ucDriverLicenses()
        {
            InitializeComponent();
        }

        public void LoadHistory(int personId)
        {
            var driverIds = GetDriverIdsByPersonId(personId);

            DataTable localHistory = FilterByDriverIds(
                clsLicense.GetAllLicenses(),
                "DriverID",
                driverIds);

            DataTable internationalHistory = FilterByDriverIds(
                clsInternationalLicense.GetAllInternationalLicenses(),
                "DriverID",
                driverIds);

            dgvLocalLicenses.DataSource = localHistory;
            dgvInternationalLicenses.DataSource = internationalHistory;

         
        }

        private List<int> GetDriverIdsByPersonId(int personId)
        {
            var result = new List<int>();
            DataTable drivers = clsDriver.GetAllDrivers();

            if (drivers == null || drivers.Rows.Count == 0)
                return result;

            foreach (DataRow row in drivers.Rows)
            {
                int rowPersonId = Convert.ToInt32(row["PersonID"]);
                if (rowPersonId == personId)
                    result.Add(Convert.ToInt32(row["DriverID"]));
            }

            return result;
        }

        private DataTable FilterByDriverIds(DataTable source, string driverIdColumnName, List<int> driverIds)
        {
            if (source == null)
                return new DataTable();

            DataTable result = source.Clone();

            if (driverIds == null || driverIds.Count == 0 || !source.Columns.Contains(driverIdColumnName))
                return result;

            var idSet = new HashSet<int>(driverIds);

            foreach (DataRow row in source.Rows)
            {
                int rowDriverId = Convert.ToInt32(row[driverIdColumnName]);
                if (idSet.Contains(rowDriverId))
                    result.ImportRow(row);
            }

            return result;
        }

    }
}
