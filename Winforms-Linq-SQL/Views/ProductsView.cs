using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformsLinqSQL.Models;
using WinformsLinqSQL.Repositories;

namespace WinformsLinqSQL.Views
{
    public partial class ProductsView : Form
    {
        private static ProductsView instance;
        private ProductsController controller;
        private BindingSource bindingSource;
        public ProductsView()
        {
            InitializeComponent();
            txtInStock.DataSource = new List<bool> { true, false };
            txtInStock.DropDownStyle = ComboBoxStyle.DropDownList;
            txtInStock.SelectedIndex = 0;
            this.controller = ProductsController.Instance;
            bindingSource = new BindingSource();
            DisplayData();
        }
        public static ProductsView Instance(Form container)
        {
            if (instance == null)
            {
                instance = new ProductsView();
                instance.MdiParent = container;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            return instance;
        }


        private void DisplayData()
        {
            var (data, isSuccessfull) = controller.GetAll(out string errorMessage);
            if (isSuccessfull)
            {
                bindingSource.DataSource = data;
                productGridView.DataSource = bindingSource;
                bindingSource.ResetBindings(false);
            }
            else
            {
                ViewHelpers.ShowMessageBox(errorMessage, false);
            }
        }
        private void PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                productGridView.DataSource = bindingSource;
                return;
            }
            int.TryParse(txtSearch.Text, out int id);
            productGridView.DataSource = bindingSource.List
                .Cast<dynamic>()
                .Where(product =>
                product.Id == id ||
                product.Name.ToLower().StartsWith(txtSearch.Text.ToLower())
                )
                .ToList();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (!double.TryParse(txtProductPrice.Text, out double price))
            {
                errorMessage = "Price field is Invalid";
            }
            else
            {
                Product product = new Product(txtProductName.Text, price, (bool)txtInStock.SelectedItem);
                if (controller.Insert(product, out errorMessage))
                {
                    DisplayData();
                    ViewHelpers.ShowMessageBox($"Successfully created user with id - {product.Id}", true);
                    return;
                }
            }
            ViewHelpers.ShowMessageBox(errorMessage, false);

        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (!int.TryParse(txtProductId.Text, out int id))
            {
                errorMessage = "Id must be a valid nubmer that exist in the table";
            }
            else if (!double.TryParse(txtProductPrice.Text, out double price))
            {
                errorMessage = "Price or/and in stock fields are Invalid";
            }
            else
            {
                Product product = new Product(id, txtProductName.Text, price, (bool)txtInStock.SelectedItem);
                if (controller.Update(product, out errorMessage))
                {
                    DisplayData();
                    ViewHelpers.ShowMessageBox($"Successfully updated user with id - {product.Id}", true);
                    return;
                }
            }
            ViewHelpers.ShowMessageBox(errorMessage, false);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            if (!int.TryParse(txtProductId.Text, out int id))
            {
                errorMessage = "Id must be a valid nubmer that exist in the table";
            }
            else
            {
                if (controller.Delete(id, out errorMessage))
                {
                    DisplayData();
                    ViewHelpers.ShowMessageBox($"Successfully deleted product with id - {id}", true);
                    return;
                }
            }
            ViewHelpers.ShowMessageBox(errorMessage, false);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtInStock.SelectedIndex = 0;
            txtSearch.Clear();
            productGridView.DataSource = bindingSource;
        }
        private void productGridView_Click(object sender, EventArgs e)
        {
            int rowIndex = productGridView.CurrentRow.Index;
            ViewHelpers.PopulateTextBoxesFromDateGrid(
                productGridView,
                productGridView.CurrentRow.Index,
                new TextBox[]
                {
                    txtProductId, txtProductName, txtProductPrice,
                }, new int[] { 0, 1, 2 });
            if (rowIndex < productGridView.RowCount - 1)
                txtInStock.SelectedItem = (bool)productGridView[3, rowIndex].Value;
            txtInStock.Select();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }
    }
}
