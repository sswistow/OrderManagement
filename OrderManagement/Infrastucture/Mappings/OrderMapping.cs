using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Infrastucture.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        private const string TableName = "order";

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(TableName);
            builder.Property(p => p.Id).HasColumnName("id").IsRequired();
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CreationDate).HasColumnName("creation_date");
            builder.Property(p => p.State).HasColumnName("state");
            builder.HasOne(p => p.Thread);
        }
    }
}
