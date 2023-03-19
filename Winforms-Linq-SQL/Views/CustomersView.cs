using WinformsLinqSQL.Controllers;
using WinformsLinqSQL.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsLinqSQL.Views
{
    public partial class CustomersView : Form
    {

        private static CustomersView instance;
        private CustomersController controller;
        private CustomersView()
        {
            InitializeComponent();
            this.controller = CustomersController.Instance();
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

        public void DisplayData()
        {
            var (data, isSuccessfull) = controller.GetAllCustomerData(out string errorMessage);
            if (isSuccessfull)
            {
                customerDataGrid.DataSource = data;
            }
            else
            {
                ShowMessageBox(errorMessage, false);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer(txtFirstName.Text, txtLastName.Text, txtAdress.Text, txtPhoneNumber.Text);
            if (controller.InsertNewCustomer(customer, out string errorMessage))
            {
                DisplayData();
                ShowMessageBox($"Successfully created user with id - {customer.Id}", true);
            }
            else
            {
                ShowMessageBox(errorMessage, false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int.TryParse(txtId.Text, out int id);
            if (id == 0)
            {
                ShowMessageBox("Id must be a valid nubmer that exist in the table", false);
                return;
            }
            Customer customer = new Customer(id, txtFirstName.Text, txtLastName.Text, txtAdress.Text, txtPhoneNumber.Text);
            if (controller.UpdateCustomer(customer, out string errorMessage))
            {
                DisplayData();
                ShowMessageBox($"Successfully updated user with id - {customer.Id}", true);
            }
            else
            {
                ShowMessageBox(errorMessage, false);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int.TryParse(txtId.Text, out int id);
            if (id == 0)
            {
                ShowMessageBox("Id must be a valid nubmer that exist in the table", false);
                return;
            }
            if (controller.DeleteCustomer(id, out string errorMessage))
            {
                DisplayData();
                ShowMessageBox($"Successfully deleted user with id - {id}", true);
            }
            else
            {
                ShowMessageBox(errorMessage, false);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                DisplayData();
                return;
            }

            int id = int.TryParse(txtSearch.Text, out id) ? id : 0;
            var (data, isSuccessfull) = controller.SearchCustomer(id, txtSearch.Text, out string errorMessage);
            if (isSuccessfull)
            {
                customerDataGrid.DataSource = data;
            }
            else
            {
                ShowMessageBox(errorMessage, false);
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAdress.Clear();
            txtPhoneNumber.Clear();
            DisplayData();
        }
        private void customerDataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            int rowIdx = customerDataGrid.CurrentRow.Index;
            txtId.Text = customerDataGrid[0, rowIdx].Value.ToString();
            txtFirstName.Text = customerDataGrid[1, rowIdx].Value.ToString();
            txtLastName.Text = customerDataGrid[2, rowIdx].Value.ToString();
            txtPhoneNumber.Text = customerDataGrid[3, rowIdx].Value.ToString();
            txtAdress.Text = customerDataGrid[4, rowIdx].Value.ToString();
        }
        public void ShowMessageBox(string message, bool isSuccess = false)
        {
            MessageBox.Show(message, isSuccess ? "Success" : "Error", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
    }
}
