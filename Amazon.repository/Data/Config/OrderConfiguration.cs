using Amazon.core.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.repository.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());

            builder.Property(o => o.Status).HasConversion(
                
                o=>o.ToString(),
                o=>(OrderStatus) Enum.Parse(typeof(OrderStatus),o)
                
                );
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            //builder.HasOne(o => o.DeliveryMethod).WithOne();

            builder.HasMany(o => o.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
