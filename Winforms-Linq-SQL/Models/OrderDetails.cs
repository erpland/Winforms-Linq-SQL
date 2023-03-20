using System.Data.Linq.Mapping;

namespace WinformsLinqSQL.Repositories
{
    [Table(Name = "order_details")]
    public class OrderDetails
    {
        [Column(IsPrimaryKey = true, Name = "product_id")]
        public int ProductId { get; set; }
        [Column(IsPrimaryKey = true, Name = "order_id")]
        public int OrderId { get; set; }
        [Column(IsPrimaryKey = true, Name = "prodcut_qty")]
        public int Qty { get; set; }

        public OrderDetails()
        {
            
        }

        public OrderDetails(int orderId, int qty)
        {
            OrderId = orderId;
            Qty = qty;
        }

        public OrderDetails(int productId, int orderId, int qty)
        {
            ProductId = productId;
            OrderId = orderId;
            Qty = qty;
        }
    }
}
