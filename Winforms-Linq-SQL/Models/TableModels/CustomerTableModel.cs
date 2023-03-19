using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformsLinqSQL.Models.TableModels
{
    public class CustomerTableModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalOrders { get; set; }
        public double TotalPrice { get; set; }

    }
}
