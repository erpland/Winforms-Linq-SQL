using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformsLinqSQL.Controllers;
using WinformsLinqSQL.Models.TableModels;

namespace WinformsLinqSQL.Views
{
    public partial class OrdersView : Form
    {

        private static OrdersView instance;
        private OrdersController ordersController;
        private BindingSource bindingSource;
        public OrdersView()
        {
            InitializeComponent();
            this.ordersController = OrdersController.Instance();
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
        public void DisplayData()
        {
            var (data, isSuccessfull) = ordersController.GetAllOrdersData(out string errorMessage); if (isSuccessfull)
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
        public void PerformSearch()
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
    }

}
