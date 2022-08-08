using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Store.App.DialogForms;
using Store.App.Interfaces;
using Store.Models;
using Store.Services;

namespace Store.App.ListForms
{
    public partial class EmployeesForm : Form, IListForm
    {
        private int _id;
        private readonly EmployeeService _employeeService;

        public EmployeesForm()
        {
            InitializeComponent();
            _employeeService = new(Program.configuration);
        }

        private void EmployeesForm_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;
            grdList.DataSource = LoadEmployees();
        }

        public void InsertRecord() => OpenForm();

        public void UpdateRecord() => OpenForm(_id);

        public void DeleteRecord()
        {
            try
            {
                if (MessageBox.Show("Do You Want Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    _employeeService.Delete(_id);
                    grdList.DataSource = LoadEmployees();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        public void SearchRecords()
        {
            List<Employee> result = LoadEmployees();

            //result = result.Where(e =>
            //    (txtFirstName.Text == "" || e.FirstName.Contains(txtFirstName.Text)) &&
            //    (txtLastName.Text == "" || e.LastName.Contains(txtLastName.Text)) &&
            //    (cbBirthDate.Checked || e.BirthDate > dtBirthFrom.Value && e.BirthDate < dtBirthTo.Value) &&
            //    (cbHireDate.Checked || e.HireDate > dtHireFrom.Value && e.HireDate < dtHireTo.Value)
            //).ToList();

            if (txtFirstName.Text != "")
            {
                result = result.Where(e => e.FirstName.ToLower().Contains(txtFirstName.Text.ToLower())).ToList();
            }
            if (txtLastName.Text != "")
            {
                result = result.Where(e => e.LastName.ToLower().Contains(txtLastName.Text.ToLower())).ToList();
            }
            if (cbBirthDate.Checked)
            {
                result = result.Where(e => e.BirthDate > dtBirthFrom.Value && e.BirthDate < dtBirthTo.Value).ToList();
            }
            if (cbHireDate.Checked)
            {
                result = result.Where(e => e.HireDate > dtHireFrom.Value && e.HireDate < dtHireTo.Value).ToList();
            }

            grdList.DataSource = result;
        }

        private void grdList_CellEnter(object sender, DataGridViewCellEventArgs e)
            => _id = Convert.ToInt32(grdList.Rows[e.RowIndex].Cells["ID"].Value);

        private void grdList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            => UpdateRecord();

        private void btnSearch_Click(object sender, EventArgs e) => SearchRecords();

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            cbBirthDate.Checked = false;
            cbHireDate.Checked = false;
            dtBirthFrom.Value = DateTime.Today;
            dtBirthTo.Value = DateTime.Today;
            dtHireFrom.Value = DateTime.Today;
            dtHireTo.Value = DateTime.Today;
            grdList.DataSource = LoadEmployees();
        }

        private List<Employee> LoadEmployees() => _employeeService.Load().ToList();

        private void OpenForm(int id = default)
        {
            EmployeeForm form = id == default ? new() : new(id);
            if (id == default) form.Text = $"Add new {form.Text.ToLower()}";
            else  form.Text = $"Edit {form.Text.ToLower()}";

            if (form.ShowDialog() == DialogResult.OK)
                grdList.DataSource = LoadEmployees();
        }
    }
}