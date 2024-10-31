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
    public class ReceiptsConfiguration : IEntityTypeConfiguration<Receipts>
    {
        public void Configure(EntityTypeBuilder<Receipts> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.CreatedAt)
                .HasColumnType("DATETIME");
            builder.Property(p => p.TotalReceiptMoney)
                .HasColumnType("money");
            builder.HasOne(p => p.providers)
                .WithMany(p => p.receipts)
                .HasForeignKey(fk =>fk.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
