using Application.DAL.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.ConfigurationModels
{
    public class OrderPaymentConfiduration : IEntityTypeConfiguration<OrderPayment>
    {
        public void Configure(EntityTypeBuilder<OrderPayment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Amount)
                .HasColumnType("money");
            builder.Property(p => p.ProviderPayment)
                .HasColumnType("VARCHAR(25)");
            builder.Property(p => p.CreatedAt)
                .HasColumnType("DATETIME");
            builder.Property(p => p.UpdatedAt)
                .HasColumnType("DATETIME");
            builder.HasOne(p => p.orders)
                .WithOne(p => p.payments)
                .HasForeignKey<OrderPayment>(fk => fk.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
