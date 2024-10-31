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
    public class ReceiptItemsConfiguration : IEntityTypeConfiguration<ReceiptItems>
    {
        public void Configure(EntityTypeBuilder<ReceiptItems> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.UnitPrice)
                 .HasColumnType("money");

            builder.HasOne(p => p.products)
                .WithMany(p => p.receiptItems)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.receipts)
                .WithMany(p => p.receiptItems)
                .HasForeignKey(p => p.ReceiptId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
