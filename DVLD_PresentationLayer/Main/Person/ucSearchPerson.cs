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
    public partial class ucSearchPerson : UserControl
    {
        private bool _isLoaded = false;
        public event Action<clsPerson> PersonSelected;
  

        private int _PersonID = -1;
        private DataTable _PeopleTable;

        public int SelectedPersonID => _PersonID;

        public ucSearchPerson()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

        }

        private void ucSearchPerson_Load(object sender, EventArgs e)
        {
            if (_isLoaded) return;

            _PeopleTable = clsPerson.GetAllPeople();
            ucEntityFilter.Bind(_PeopleTable);

            _isLoaded = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            var filter = ucEntityFilter.GetFilter();

            if (filter.Column == null)
            {
                MessageBox.Show("Please select a filter and enter a value.");
                return;
            }
           

            string column = filter.Column;
            string value = filter.Value.Replace("'", "''");

            string rowFilter = _PeopleTable.Columns[column].DataType == typeof(string)
                ? $"[{column}] = '{value}'"
                : $"[{column}] = {value}";

            DataRow[] rows = _PeopleTable.Select(rowFilter);

            if (rows.Length == 0)
            {
                MessageBox.Show("Person not found.");
                return;
            }

            if (rows.Length > 1)
            {
                MessageBox.Show("Multiple persons found. Refine your search.");
                return;
            }

            _PersonID = Convert.ToInt32(rows[0]["PersonID"]);

            var person = clsPerson.Find(_PersonID);

            ucPersonCard.LoadPerson(person);

            PersonSelected?.Invoke(person);
            ucPersonCard.Enabled = false;

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            var frm = new frmPersonHost(frmPersonHost.EditorMode.Add, null);

            frm.PersonSaved += person =>
            {
                _PersonID = person.PersonID;
                ucPersonCard.LoadPerson(person);
                PersonSelected?.Invoke(person);
            };

            frm.ShowDialog(this);
        }

        public void LoadPerson(int personID)
        {
            var person = clsPerson.Find(personID);
            if (person == null) return;

            _PersonID = person.PersonID;
            ucPersonCard.LoadPerson(person);
        }

      
    }
}
