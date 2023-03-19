using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsLinqSQL.Views
{
    public partial class MainView : Form
    {
        CustomersView customersView;
        OrdersView ordersView;
        public MainView()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
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
    }
}
