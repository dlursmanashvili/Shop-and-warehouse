using Store.App.DialogForms;
using Store.App.Interfaces;
using Store.App.ListForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Store.Services;

namespace Store.App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region Menu buttons

        // List buttons
        private void msEmployeesList_Click(object sender, EventArgs e) => OpenListForm<EmployeesForm>();
        private void msUsersList_Click(object sender, EventArgs e) => OpenListForm<UsersForm>();
        private void msProductsList_Click(object sender, EventArgs e) => OpenListForm<ProductsForm>();
        private void msPermissionsList_Click(object sender, EventArgs e) => OpenListForm<PermissionsForm>();
        private void msRolesList_Click(object sender, EventArgs e) => OpenListForm<RolesForm>();
        private void msCategoriesList_Click(object sender, EventArgs e) => OpenListForm<CategoriesForm>();

        // Add buttons
        private void msEmployeesAdd_Click(object sender, EventArgs e) => OpenAddDialogForm<EmployeesForm>();
        private void msUsersAdd_Click(object sender, EventArgs e) => OpenAddDialogForm<UsersForm>();
        private void msProductsAdd_Click(object sender, EventArgs e) => OpenAddDialogForm<ProductsForm>();
        private void msPermissionsAdd_Click(object sender, EventArgs e) => OpenAddDialogForm<PermissionsForm>();
        private void msRolesAdd_Click(object sender, EventArgs e) => OpenAddDialogForm<RolesForm>();
        private void msCategoriesAdd_Click(object sender, EventArgs e) => OpenAddDialogForm<CategoriesForm>();

        // Edit buttons
        private void msEmployeesEdit_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<EmployeesForm>(true);
        private void msUsersEdit_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<UsersForm>(true);
        private void msProductsEdit_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<ProductsForm>(true);
        private void msPermissionsEdit_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<PermissionsForm>(true);
        private void msRolesEdit_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<RolesForm>(true);
        private void msCategoriesEdit_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<CategoriesForm>(true);

        // Remove buttons
        private void msEmployeesDelete_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<EmployeesForm>(false);
        private void msUsersDelete_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<UsersForm>(false);
        private void msProductsDelete_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<ProductsForm>(false);
        private void msPermissionsDelete_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<PermissionsForm>(false);
        private void msRolesDelete_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<RolesForm>(false);
        private void msCategoriesDelete_Click(object sender, EventArgs e) => OpenEditOrDeleteDialogForm<CategoriesForm>(false);

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckPermissions();
        }

        private void OpenListForm<T>() where T : Form, new()
        {
            T form = new();
            form.MdiParent = this;
            form.Show();
        }

        private void OpenAddDialogForm<T>() where T : Form, IListForm, new()
        {
            var form = new T();
            form.InsertRecord();
        }

        private void OpenEditOrDeleteDialogForm<T>(bool isEdit) where T : Form, IListForm, new()
        {
            if (ActiveMdiChild is not T)
            {
                MessageBox.Show(
                    $"Open the appropiate form!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var listForm = ActiveMdiChild as IListForm;
            if (isEdit) listForm.UpdateRecord();
            else listForm.DeleteRecord();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            ValidateActiveMdiChild("None of the forms are open!");
            if (ActiveMdiChild is IListForm listForm)
                listForm.InsertRecord();
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            ValidateActiveMdiChild("Select the field!");
            if (ActiveMdiChild is IListForm listForm)
                listForm.UpdateRecord();
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            ValidateActiveMdiChild("Select the field!");
            if (ActiveMdiChild is IListForm listForm)
                listForm.DeleteRecord();
        }

        private void ValidateActiveMdiChild(string message)
        {
            if (ActiveMdiChild == null)
            {
                MessageBox.Show(
                    message,
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void CheckPermissions()
        {
            foreach (var item in menuStrip.Items)
            {
                CheckPermissions(item as ToolStripMenuItem);
            }
        }

        private void CheckPermissions(ToolStripMenuItem item)
        {
            if (item.Tag != null && item.Tag?.ToString() != string.Empty &&
                !CurrentUser.Permissions.Contains(Convert.ToInt16(item.Tag)))
            {
                item.Visible = false;
                return;
            }
           
            foreach (var subItem in item.DropDownItems)
            {
                if (subItem is not ToolStripMenuItem) continue;
                CheckPermissions(subItem as ToolStripMenuItem);
            }
        }
    }
}