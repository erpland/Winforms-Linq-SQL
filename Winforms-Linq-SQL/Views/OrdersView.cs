using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WinformsLinqSQL.Models;
using WinformsLinqSQL.Models.TableModels;
using WinformsLinqSQL.Repositories;

namespace WinformsLinqSQL.Views
{
    public partial class OrdersView : Form
    {

        private static OrdersView instance;
        private OrdersController controller;
        private BindingSource bindingSource;
        private OrdersView()
        {
            InitializeComponent();
            this.controller = OrdersController.Instance();
            bindingSource = new BindingSource();
            DisplayData();
        }
        public static OrdersView Instance(Form container)
        {
            if (instance == null)
            {
                instance = new OrdersView();
                instance.MdiParent = container;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            return instance;
        }
        private void DisplayData()
        {
            var (data, isSuccessfull) = controller.GetAll(out string errorMessage); if (isSuccessfull)
            {
                bindingSource.DataSource = data;
                OrdersDataGrid.DataSource = bindingSource;
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
                OrdersDataGrid.DataSource = bindingSource;
                return;
            }
            int.TryParse(txtSearch.Text, out int id);
            OrdersDataGrid.DataSource = bindingSource.List
                .Cast<OrderTableModel>()
                .Where(order =>
                order.OrderId == id ||
                order.CustomerId == id ||
                order.CustomerName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                order.PhoneNumber.Equals(txtSearch.Text)
                )
                .ToList();
        }
        private void OrdersDataGrid_Click(object sender, EventArgs e)
        {
            int rowIndex = OrdersDataGrid.CurrentRow.Index;
            ViewHelpers.PopulateTextBoxesFromDateGrid(OrdersDataGrid, rowIndex, new TextBox[]
            {
                txtOrderId,
                txtCustomerId,
            }, new int[]
            {
                0,2
            });
            if (rowIndex < OrdersDataGrid.RowCount - 1
                &&
                DateTime.TryParse(OrdersDataGrid[1, rowIndex].Value.ToString(), out DateTime orderDate))
                txtOrderDate.Value = orderDate;
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
        private void btnCreate_Click(object sender, EventArgs e)
        {
           ;
            if (!int.TryParse(txtCustomerId.Text, out int customerId))
            {
                ViewHelpers.ShowMessageBox("Customer Id must be a valid nubmer that exist in the table", false);
                return;
            }
            Order order = new Order(customerId,txtOrderDate.Value);
            if (controller.Insert(order, out string errorMessage))
            {
                DisplayData();
                ViewHelpers.ShowMessageBox($"Successfully created user with id - {order.Id}", true);
            }
            else
            {
                ViewHelpers.ShowMessageBox(errorMessage, false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int.TryParse(txtOrderId.Text, out int orderId);
            int.TryParse(txtCustomerId.Text, out int customerId);
            if (orderId == 0 || customerId == 0)
            {
                ViewHelpers.ShowMessageBox("Order id and Customer id must be a valid nubmer that exist in the table", false);
                return;
            }
            Order order = new Order(orderId,customerId,txtOrderDate.Value);
            if (controller.Update(order, out string errorMessage))
            {
                DisplayData();
                ViewHelpers.ShowMessageBox($"Successfully updated order with id - {order.Id}", true);
            }
            else
            {
                ViewHelpers.ShowMessageBox(errorMessage, false);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int.TryParse(txtOrderId.Text, out int id);
            if (id == 0)
            {
                ViewHelpers.ShowMessageBox("Customer Id must be a valid nubmer that exist in the table", false);
                return;
            }
            if (controller.Delete(id, out string errorMessage))
            {
                DisplayData();
                ViewHelpers.ShowMessageBox($"Successfully deleted order with id - {id}", true);
            }
            else
            {
                ViewHelpers.ShowMessageBox(errorMessage, false);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerId.Text = string.Empty;
            txtOrderId.Text = string.Empty;
            txtOrderDate.Text = string.Empty;
            OrdersDataGrid.DataSource = bindingSource;
        }
    }

}
