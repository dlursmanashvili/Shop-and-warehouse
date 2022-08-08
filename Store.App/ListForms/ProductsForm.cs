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
    public partial class ProductsForm : Form, IListForm
    {
        private int _id;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private List<Category> _allCategory;

        public ProductsForm()
        {
            InitializeComponent();
            _productService = new(Program.configuration);
            _categoryService = new(Program.configuration);
            _allCategory = _categoryService.Load().ToList();
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            grdList.AutoGenerateColumns = false;
            grdList.DataSource = LoadProducts();
            FillCategoryComboBox();
        }

        public void InsertRecord() => OpenForm();

        public void UpdateRecord() => OpenForm(_id);

        public void DeleteRecord()
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Do You Want Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2))
                {
                    _productService.Delete(_id);
                    grdList.DataSource = LoadProducts();
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
            List<Product> result = LoadProducts();

            //if (cbCategory.Text != "")
            //{
            //    result = result.Where(p => p.CategoryID.Equals(cbCategory.SelectedValue));
            //}
            if (txtProductName.Text != "")
            {
                result = result.Where(p => p.ProductName.ToLower().Contains(txtProductName.Text.ToLower())).ToList();
            }
            if (txtDescription.Text != "")
            {
                result = result.Where(p => p.Description != null).ToList();
                result = result.Where(p => p.Description.ToLower().Contains(txtDescription.Text.ToLower())).ToList();
            }
            if (txtMinPrice.Text != "")
            {
                result = result.Where(p => p.Price > Convert.ToDecimal(txtMinPrice.Text)).ToList();
            }
            if (txtMaxPrice.Text != "")
            {
                result = result.Where(p => p.Price < Convert.ToDecimal(txtMaxPrice.Text)).ToList();
            }
            if (txtMinUnits.Text != "")
            {
                result = result.Where(p => p.UnitsInStock > Convert.ToInt32(txtMinUnits.Text)).ToList();
            }
            if (txtMaxUnits.Text != "")
            {
                result = result.Where(p => p.UnitsInStock < Convert.ToInt32(txtMaxUnits.Text)).ToList();
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
            txtProductName.Text = "";
            txtDescription.Text = "";
            txtMinPrice.Text = "";
            txtMaxPrice.Text = "";
            txtMinUnits.Text = "";
            txtMaxUnits.Text = "";
            grdList.DataSource = LoadProducts();
        }

        private List<Product> LoadProducts() => _productService.Load().ToList();

        private void FillCategoryComboBox()
        {
            cbCategory.DataSource = _allCategory;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "ID";
        }

        private void OpenForm(int id = default)
        {
            ProductForm form = id == default ? new() : new(id);
            if (id == default) form.Text = $"Add new {form.Text.ToLower()}";
            else form.Text = $"Edit {form.Text.ToLower()}";

            if (form.ShowDialog() == DialogResult.OK)
                grdList.DataSource = LoadProducts();
        }
    }
}
