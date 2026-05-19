using Amazon.core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Specifications
{
    public class OrderWithPaymentIntentIdSpecifications :BaseSpecification<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId):base(o=>o.PaymentIntentId==paymentIntentId)
        {
            
        }
    }
}
