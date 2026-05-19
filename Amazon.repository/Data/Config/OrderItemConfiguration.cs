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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(OrderItem=>OrderItem.productItem, Product=>Product.WithOwner()); //Cannot exist without its owner (OrderItem)

            builder.Property(o => o.Price).HasColumnType("decimal(18,2)");
        }
    }
}
