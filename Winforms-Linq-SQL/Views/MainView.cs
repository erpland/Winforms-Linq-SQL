using System;
using System.Windows.Forms;

namespace WinformsLinqSQL.Views
{
    public partial class MainView : Form
    {
        CustomersView customersView;
        OrdersView ordersView;
        ProductsView productsView;
        public MainView()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            lblPage.Text = "Customers";
            if (customersView == null || customersView.IsDisposed)
            {
                customersView = CustomersView.Instance(this);
                customersView.Show();
            }
            else
            {
                customersView.Activate();
            }
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            lblPage.Text = "Orders";
            if (ordersView == null || ordersView.IsDisposed)
            {
                ordersView = OrdersView.Instance(this);
                ordersView.Show();
            }
            else
            {
                ordersView.Activate();
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            lblPage.Text = "Products";
            if (productsView == null || productsView.IsDisposed)
            {
                productsView = ProductsView.Instance(this);
                productsView.Show();
            }
            else
            {
                productsView.Activate();
            }
        }
    }
}
