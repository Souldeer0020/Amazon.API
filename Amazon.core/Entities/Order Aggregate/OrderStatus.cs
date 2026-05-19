using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Entities.Order_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")] // value stored in database
        Pending,
        [EnumMember(Value = "Payment received")]
        Paymentreceived,
        [EnumMember(Value = "Payment failed")]
        PaymentFailed


    }
}
