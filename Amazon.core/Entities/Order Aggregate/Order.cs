using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Entities.Order_Aggregate
{
    public class Order :BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal,string paymentIntentId)
        {
            this.buyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string buyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; }= OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        //[NotMapped]
        public int DeliveryMethodId { get; set; } // foreign key is specified here in order to define relationship between order and deliveryMethod
        public DeliveryMethod DeliveryMethod { get; set; } // relationship between delivery method and order is one to many because one delivery method can be used by many orders but one order can only have one delivery method
        public ICollection<OrderItem> Items { get; set; }=new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
            =>  SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; }
    }
}
