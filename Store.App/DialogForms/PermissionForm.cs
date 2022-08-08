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
    public partial class PermissionForm : Form
    {
        private readonly PermissionService _permissionService;
        private readonly object _id;

        private bool EditMode => _id != null;

        public PermissionForm()
        {
            InitializeComponent();
            _permissionService = new(Program.configuration);
        }

        public PermissionForm(object id) : this()
        {
            _id = id;
            if (EditMode) LoadPermissions();
        }

        private void btnOk_Click(object sender, EventArgs e) => SavePermission();

        private void LoadPermissions()
        {
            var permission = _permissionService.Get(_id);
            txtPermissionName.Text = permission.PermissionName;
            txtPermissionCode.Text = permission.PermissionCode.ToString();
            txtDescription.Text = permission.Description;
        }

        private void SavePermission()
        {
            try
            {
                ValidateInputData();

                var permission = new Permission()
                {
                    ID = _id,
                    PermissionName = txtPermissionName.Text,
                    PermissionCode = Convert.ToInt16(txtPermissionCode.Text),
                    Description = txtDescription.Text
                };

                if (!EditMode && _permissionService.Insert(permission) != DBNull.Value)
                {
                    DialogResult = DialogResult.OK;
                }
                if (EditMode)
                {
                    _permissionService.Update(permission);
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
            if (txtPermissionName.Text == "" || txtPermissionCode.Text == "")
                throw new ArgumentNullException();
        }

        private void txtPermissionCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
