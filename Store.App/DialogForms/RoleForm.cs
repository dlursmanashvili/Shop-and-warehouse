using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Store.Models;
using Store.Services;

namespace Store.App.DialogForms
{
    public partial class RoleForm : Form
    {
        private readonly RoleService _roleService;
        private readonly PermissionService _permissionService;
        private List<Permission> _rolePermissions;
        private List<Permission> _allPermissions;
        private readonly object _id;

        private bool EditMode => _id != null;

        public RoleForm()
        {
            InitializeComponent();
            _roleService = new(Program.configuration);
            _permissionService = new(Program.configuration);
            _allPermissions = _permissionService.Load().ToList();
        }

        public RoleForm(object id) : this()
        {
            _id = id;
            _rolePermissions = _roleService.LoadRolePermissions(_id).ToList();
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            FillPermissionsCheckedListBox();
            if (EditMode) LoadRoles();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _roleService.BeginTransaction();
                if (EditMode) UnassignAllPermission();
                SaveRoles();
                _roleService.CommitTransaction();
            }
            catch (Exception ex)
            {
                _roleService.RollbackTransaction();

                MessageBox.Show(
                    $"{ex.Message}",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void LoadRoles()
        {
            var role = _roleService.Get(_id);
            txtRoleName.Text = role.RoleName;
            txtDescription.Text = role.Description;
        }

        private void FillPermissionsCheckedListBox()
        {
            clbRolePermissions.DataSource = _allPermissions;
            clbRolePermissions.DisplayMember = "PermissionName";
            clbRolePermissions.ValueMember = "ID";

            if (EditMode)
            {
                List<short> _rolePermissionID = _rolePermissions.Select(x => x.PermissionCode).ToList();

                for (int i = 0; i < _allPermissions.Count; i++)
                {
                    if (_rolePermissionID.Contains(_allPermissions[i].PermissionCode))
                    {
                        clbRolePermissions.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void SaveRoles()
        {
            ValidateInputData();

            var role = new Role()
            {
                ID = _id,
                RoleName = txtRoleName.Text,
                Description = txtDescription.Text
            };

            if (!EditMode)
            {
                var newId = _roleService.Insert(role);

                if (newId != DBNull.Value)
                {
                    AssignPermissionToRole(newId);
                    DialogResult = DialogResult.OK;
                }
            }
            if (EditMode)
            {
                _roleService.Update(role);
                AssignPermissionToRole(_id);
                DialogResult = DialogResult.OK;
            }
        }

        private void ValidateInputData()
        {
            if (txtRoleName.Text == "")
                throw new ArgumentNullException();
        }

        private void AssignPermissionToRole(object id)
        {
            foreach (Permission item in clbRolePermissions.CheckedItems)
            {
                _roleService.AssignPermissionToRole(id, item.ID);
            }
        }

        private void UnassignAllPermission()
        {
            foreach (Permission item in _rolePermissions)
            {
                _roleService.UnassignPermissionToRole(_id, item.ID);
            }
        }
    }
}