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
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(o => o.Cost).HasColumnType("decimal(18,2)");
            //builder.HasMany<Order>()
            //    .WithOne(o => o.DeliveryMethod)
            //    .HasForeignKey(o => o.DeliveryMethodId);
        }

    }
}
