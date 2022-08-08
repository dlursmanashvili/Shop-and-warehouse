using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Store.Models;
using Store.Services;

namespace Store.App.DialogForms
{
    public partial class CategoryForm : Form
    {
        private readonly object _id;
        private readonly CategoryService _categoryService;
        private readonly List<Category> _allCategories;

        private bool EditMode => _id != null;

        public CategoryForm()
        {
            InitializeComponent();
            _categoryService = new(Program.configuration);
            _allCategories = _categoryService.Load().ToList();
        }

        public CategoryForm(object id) : this()
        {
            _id = id;
        }
        private void CategoryForm_Load(object sender, EventArgs e)
        {
            FillCategoriesComboBox();
            if (EditMode) LoadCategories();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _categoryService.BeginTransaction();
                //Assign sub category
                SaveRoles();
                _categoryService.CommitTransaction();
            }
            catch (Exception ex)
            {
                _categoryService.RollbackTransaction();

                MessageBox.Show(
                    $"{ex.Message}",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void LoadCategories()
        {
            var category = _categoryService.Get(_id);
            txtName.Text = category.CategoryName;
            txtDescription.Text = category.Description;
            //cbCategories.SelectedItem = 
        }

        private void FillCategoriesComboBox()
        {
            cbCategories.DataSource = _allCategories;
            cbCategories.DisplayMember = "PermissionName";
            cbCategories.ValueMember = "ID";

            if (!EditMode)
            {
                cbCategories.SelectedItem = null;
                cbCategories.SelectedText = "None";
            }
        }

        private void SaveRoles()
        {
            ValidateInputData();

            var category = new Category()
            {
                ID = _id,
                CategoryName = txtName.Text,
                Description = txtDescription.Text,
                ParentID = cbCategories.SelectedValue
            };

            if (!EditMode && _categoryService.Insert(category) != DBNull.Value)
            {
                DialogResult = DialogResult.OK;
            }
            if (EditMode)
            {
                _categoryService.Update(category);
                DialogResult = DialogResult.OK;
            }
        }

        private void ValidateInputData()
        {
            if (txtName.Text == "")
                throw new ArgumentNullException();
        }
    }
}
