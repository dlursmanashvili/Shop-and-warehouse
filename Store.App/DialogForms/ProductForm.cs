using Store.Models;
using Store.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store.App.DialogForms
{
    public partial class ProductForm : Form
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private List<Category> _allCategory;
        private readonly object _id;

        private bool EditMode => _id != null;

        public ProductForm()
        {
            InitializeComponent();
            _productService = new(Program.configuration);
            _categoryService = new(Program.configuration);
            _allCategory = _categoryService.Load().ToList();
        }

        public ProductForm(object id) : this()
        {
            _id = id;
            if (EditMode) LoadProducts();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            FillCategoryComboBox();
        }

        private void btnOk_Click(object sender, EventArgs e) => SaveProduct();

        private void LoadProducts()
        {
            var product = _productService.Get(_id);
            txtProductName.Text = product.ProductName;
            txtDescription.Text = product.Description;
            txtPrice.Text = product.Price.ToString();
            txtUnitsInStock.Text = product.UnitsInStock.ToString();
        }

        private void SaveProduct()
        {
            try
            {
                ValidateInputData();

                Product product = new Product()
                {
                    ID = _id,
                    CategoryID = cbCategory.SelectedValue,
                    ProductName = txtProductName.Text,
                    Description = txtDescription.Text,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    UnitsInStock = Convert.ToInt32(txtUnitsInStock.Text)
                };

                if (!EditMode && _productService.Insert(product) != DBNull.Value)
                {
                    DialogResult = DialogResult.OK;
                }
                if (EditMode)
                {
                    _productService.Update(product);
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

        private void FillCategoryComboBox()
        {
            cbCategory.DataSource = _allCategory;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "ID";

            if (EditMode)
            {
                var c = _productService.GetProductCategory(_id);
                cbCategory.SelectedItem = c;
                cbCategory.Text = c.CategoryName;
            }
        }

        private void ValidateInputData()
        {
            if (txtProductName.Text == "" || txtPrice.Text == "" || txtUnitsInStock.Text == "")
                throw new ArgumentNullException();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                e.Handled = true;
        }

        private void txtUnitsInStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar)) 
                e.Handled = true;
        }
    }
}
