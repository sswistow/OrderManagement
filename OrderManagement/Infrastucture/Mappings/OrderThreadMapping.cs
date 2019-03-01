using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Infrastucture.Mappings
{
    public class OrderThreadMapping : IEntityTypeConfiguration<OrderThread>
    {
        private const string TableName = "thread";

        public void Configure(EntityTypeBuilder<OrderThread> builder)
        {
            builder.ToTable(TableName);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Interval).HasColumnName("interval");
            builder.Property(p => p.State).HasColumnName("state");
        }
    }
}
