using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_PresentationAccess.Main.Managers
{
    public partial class ucEntityFilter : UserControl
    {
        private DataTable _table;

        // 🔔 Fired when filter changes
        public event Action<DataView> FilterChanged;

        public ucEntityFilter()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

        }

        // ========================= PUBLIC =========================

        public void Bind(DataTable table)
        {
            _table = table;

            cbColumns.Items.Clear();
            cbColumns.Items.Add("None");

            foreach (DataColumn col in table.Columns)
                cbColumns.Items.Add(col.ColumnName);

            cbColumns.SelectedIndex = 0;
        }

        public (string Column, string Value) GetFilter()
        {
            if (_table == null)
                return (null, null);

            if (cbColumns.SelectedIndex == 0 || string.IsNullOrWhiteSpace(txtFilter.Text))
                return (null, null);

            return (cbColumns.SelectedItem.ToString(), txtFilter.Text.Trim());
        }

        // ========================= EVENTS =========================

        private void cbColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = cbColumns.SelectedIndex != 0;
            txtFilter.Clear();
            ApplyFilter();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        // ========================= CORE =========================

        private void ApplyFilter()
        {
            if (_table == null)
                return;

            DataView dv = _table.DefaultView;

            if (cbColumns.SelectedIndex == 0 || string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                dv.RowFilter = "";
            }
            else
            {
                string column = cbColumns.SelectedItem.ToString();
                string value = txtFilter.Text.Replace("'", "''");

                if (_table.Columns[column].DataType == typeof(string))
                    dv.RowFilter = $"[{column}] LIKE '%{value}%'";
                else
                    dv.RowFilter =
                        $"CONVERT([{column}], 'System.String') LIKE '%{value}%'";
            }

            FilterChanged?.Invoke(dv);
        }
    }
}
