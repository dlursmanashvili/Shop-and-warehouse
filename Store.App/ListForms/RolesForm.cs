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
    public partial class RolesForm : Form, IListForm
    {
        private int _id;
        private readonly RoleService _roleService;

        public RolesForm()
        {
            InitializeComponent();
            _roleService = new(Program.configuration);
        }

        private void RolesForm_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;
            grdList.DataSource = LoadRoles();
        }

        public void InsertRecord() => OpenForm();

        public void UpdateRecord() => OpenForm(_id);

        public void DeleteRecord()
        {
            try
            {
                if (MessageBox.Show("Do You Want Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    _roleService.Delete(_id);
                    grdList.DataSource = LoadRoles();
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
            throw new NotImplementedException();
        }

        private void grdList_CellEnter(object sender, DataGridViewCellEventArgs e)
            => _id = Convert.ToInt32(grdList.Rows[e.RowIndex].Cells["ID"].Value);

        private void grdList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            => UpdateRecord();

        private List<Role> LoadRoles() => _roleService.Load().ToList();

        private void OpenForm(int id = default)
        {
            RoleForm form = id == default ? new() : new(id);
            if (id == default) form.Text = $"Add new {form.Text.ToLower()}";
            else form.Text = $"Edit {form.Text.ToLower()}";

            if (form.ShowDialog() == DialogResult.OK)
                grdList.DataSource = LoadRoles();
        }
    }
}