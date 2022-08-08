using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Store.Models;
using Store.Services;

namespace Store.App.DialogForms
{
    public partial class UserForm : Form
    {
        private readonly UserService _userService;
        private readonly EmployeeService _employeeService;
        private readonly RoleService _roleService;
        private List<Employee> _emptyEmployees;
        private List<Role> _allRoles;
        private Role _userRole;
        private readonly object _id;

        private bool EditMode => _id != null;

        public UserForm()
        {
            InitializeComponent();
            _userService = new(Program.configuration);
            _employeeService = new(Program.configuration);
            _roleService = new(Program.configuration);
            _emptyEmployees = _employeeService.LoadEmptyEmployees().ToList();
            _allRoles = _roleService.Load().ToList();
        }

        public UserForm(object id) : this()
        {
            _id = id;
            _userRole = _userService.GetUserRole(_id);
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            if (!EditMode)
            {
                pOldPassword.Visible = false;
                flpPassword.Height = 64;
                this.Height = 278;
            }

            cbIsActive.Checked = true;
            FillEmployeesComboBox();
            FillRolesComboBox();
            if (EditMode) LoadUsers();
        }

        private void btnOk_Click(object sender, EventArgs e) => SaveUsers();

        private void lblAssignPassword_Click(object sender, EventArgs e)
        {
            if (EditMode)
            {
                if (!flpPassword.Visible)
                {
                    lblAssignPassword.Text = "Change password [-]";
                    this.Height = 304;
                    flpPassword.Visible = true;
                }
                else
                {
                    lblAssignPassword.Text = "Change password [+]";
                    this.Height = 220;
                    flpPassword.Visible = false;
                }
            }
        }
        
        private void SaveUsers()
        {
            try
            {
                ValidateInputData();
                _userService.BeginTransaction();

                var user = new User()
                {
                    Username = txtUsername.Text,
                    Password = txtConfrimPassword.Text,
                    IsActive = cbIsActive.Checked
                };
                user.ID = EditMode ? _id : cbEmployees.SelectedValue;

                if (!EditMode)
                {
                    var newId = _userService.Insert(user);

                    if (newId != DBNull.Value)
                    {
                        AssignRole(newId);
                        DialogResult = DialogResult.OK;
                    }
                }
                if (EditMode)
                {
                    _userService.Update(user);
                    UnassignRole();
                    AssignRole(_id);
                    if (flpPassword.Visible)
                        _userService.ChangeUserPassword(_id, txtConfrimPassword.Text, txtOldPassword.Text);
                    DialogResult = DialogResult.OK;
                }
                _userService.CommitTransaction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{ex.Message}",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                _userService.RollbackTransaction();
            }
        }

        private void LoadUsers()
        {
            var user = _userService.Get(_id);
            var employee = _employeeService.Get(_id);
            txtUsername.Text = user.Username;
            cbEmployees.Enabled = false;
            cbEmployees.Text = employee.FirstName + " " + employee.LastName;
            cbRoles.SelectedItem = _userRole;
            cbRoles.Text = _userRole.RoleName;
            lblPassword.Text = "New password";
            lblAssignPassword.Text = "Change password [+]";
            flpPassword.Visible = false;
            this.Height = 220;
        }

        private void FillEmployeesComboBox()
        {
            cbEmployees.DataSource = _emptyEmployees;
            cbEmployees.DisplayMember = "FirstName";
            cbEmployees.ValueMember = "ID";
        }

        private void FillRolesComboBox()
        {
            cbRoles.DataSource = _allRoles;
            cbRoles.DisplayMember = "RoleName";
            cbRoles.ValueMember = "ID";
        }

        private void AssignRole(object id)
            => _userService.AssignUserToRole(id, cbRoles.SelectedValue);

        private void UnassignRole()
            => _userService.UnassignUserToRole(_id, _userRole.ID);

        private void ValidateInputData()
        {
            if (txtUsername.Text == "" || cbEmployees.SelectedItem == null || cbRoles.SelectedItem == null)
                throw new ArgumentNullException();

            if (flpPassword.Visible && ((EditMode && txtOldPassword.Text == "") || txtPassword.Text == "" || txtConfrimPassword.Text == ""))
                throw new ArgumentNullException();

            if (txtPassword.Text != txtConfrimPassword.Text)
                throw new Exception("Password does not match!");
        }
    }
}