using System;

namespace WinformsLinqSQL.Models.TableModels
{
    public class OrderTableModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; } 
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }

    }
}
