using Amazon.core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Specifications
{
    public class OrdersSpecification : BaseSpecification<Order>
    {
        public OrdersSpecification(string email):base(o=>o.buyerEmail==email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

            AddOrderByDesc(o => o.OrderDate);
        }
        public OrdersSpecification(string email,int orderId):base(o=>o.buyerEmail==email && o.Id==orderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

        }
    }
}
