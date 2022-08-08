using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Store.App;
using Store.Models;
using Store.Services;

namespace Store.App.DialogForms
{
    public partial class EmployeeForm : Form
    {
        private readonly EmployeeService _employeeService;
        private readonly object _id;

        private bool EditMode => _id != null;

        public EmployeeForm()
        {
            InitializeComponent();
            _employeeService = new(Program.configuration);
        }

        public EmployeeForm(object id) : this()
        {
            _id = id;
            if (EditMode) LoadEmployee();
        }

        private void btnOk_Click(object sender, EventArgs e) => SaveEmployee();

        private void LoadEmployee()
        {
            var employee = _employeeService.Get(_id);
            txtFirstName.Text = employee.FirstName;
            txtLastName.Text = employee.LastName;
            dtBirthdate.Value = employee.BirthDate;
            dtHiredate.Value = employee.HireDate;
        }

        private void SaveEmployee()
        {
            try
            {
                ValidateInputData();

                Employee employee = new Employee()
                {
                    ID = _id,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    BirthDate = dtBirthdate.Value,
                    HireDate = dtHiredate.Value
                };

                if (!EditMode && _employeeService.Insert(employee) != DBNull.Value)
                {
                    DialogResult = DialogResult.OK;
                }
                if (EditMode)
                {
                    _employeeService.Update(employee);
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{ex.Message}",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void ValidateInputData()
        {
            if (txtFirstName.Text == "" || txtLastName.Text == "")
                throw new ArgumentNullException();
        }

        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e) => OnlyLetters(e);

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e) => OnlyLetters(e);

        private void OnlyLetters(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !Char.IsLetter(e.KeyChar))
                e.Handled = true;
        }
    }
}