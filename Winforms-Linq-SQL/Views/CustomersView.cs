using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WinformsLinqSQL.Models;
using WinformsLinqSQL.Repositories;

namespace WinformsLinqSQL.Views
{
    public partial class CustomersView : Form
    {
        private static CustomersView instance;
        private CustomersController controller;
        private BindingSource bindingSource;
        private CustomersView()
        {
            InitializeComponent();
            this.controller = CustomersController.Instance();
            bindingSource = new BindingSource();
            DisplayData();

        }
        public static CustomersView Instance(Form container)
        {
            if (instance == null)
            {
                instance = new CustomersView();
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
                customerDataGrid.DataSource = bindingSource;
                bindingSource.ResetBindings(false);
            }
            else
            {
                ViewHelpers.ShowMessageBox(errorMessage, false);
            }
        }
        //filter locally instaed of fetching from database again
        private void PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                customerDataGrid.DataSource = bindingSource;
                return;
            }
            int.TryParse(txtSearch.Text, out int id);
            customerDataGrid.DataSource = bindingSource.List
                .Cast<dynamic>()
                .Where(customer =>
                customer.Id == id ||
                customer.FirstName.ToLower().StartsWith(txtSearch.Text.ToLower()) ||
                customer.LastName.ToLower().StartsWith(txtSearch.Text.ToLower()) ||
                customer.PhoneNumber.Equals(txtSearch.Text)
                )
                .ToList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer(txtFirstName.Text, txtLastName.Text, txtAdress.Text, txtPhoneNumber.Text);
            if (controller.Insert(customer, out string errorMessage))
            {
                DisplayData();
                ViewHelpers.ShowMessageBox($"Successfully created user with id - {customer.Id}", true);
            }
            else
            {
                ViewHelpers.ShowMessageBox(errorMessage, false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int.TryParse(txtId.Text, out int id);
            if (id == 0)
            {
                ViewHelpers.ShowMessageBox("Id must be a valid nubmer that exist in the table", false);
                return;
            }
            Customer customer = new Customer(id, txtFirstName.Text, txtLastName.Text, txtAdress.Text, txtPhoneNumber.Text);
            if (controller.Update(customer, out string errorMessage))
            {
                DisplayData();
                ViewHelpers.ShowMessageBox($"Successfully updated user with id - {customer.Id}", true);
            }
            else
            {
                ViewHelpers.ShowMessageBox(errorMessage, false);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int.TryParse(txtId.Text, out int id);
            if (id == 0)
            {
                ViewHelpers.ShowMessageBox("Id must be a valid nubmer that exist in the table", false);
                return;
            }
            if (controller.Delete(id, out string errorMessage))
            {
                DisplayData();
                ViewHelpers.ShowMessageBox($"Successfully deleted user with id - {id}", true);
            }
            else
            {
                ViewHelpers.ShowMessageBox(errorMessage, false);
            }
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAdress.Clear();
            txtPhoneNumber.Clear();
            txtSearch.Clear();
            customerDataGrid.DataSource = bindingSource;
        }
        private void customerDataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            ViewHelpers.PopulateTextBoxesFromDateGrid(
                customerDataGrid,
                customerDataGrid.CurrentRow.Index,
                new TextBox[]
                {
                    txtId, txtFirstName, txtLastName,txtPhoneNumber, txtAdress
                }, new int[]{
                0,1,2,3,4});
        }
    }
}
