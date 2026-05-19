using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Entities.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered productItem, decimal price, int quantity)
        {
            this.productItem = productItem;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered productItem { get; set; } //product to be placed in order
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
