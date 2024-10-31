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
    public class DiscountsConfiguration : IEntityTypeConfiguration<Discounts>
    {
        public void Configure(EntityTypeBuilder<Discounts> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasColumnType("NVARCHAR(125)");
            builder.Property(p => p.Discount_percent)
                .HasColumnType("DECIMAL(5,2)");
            builder.Property(p => p.CreateAt)
                .HasColumnType("DATETIME");
            builder.Property(p => p.UpdateAt)
                .HasColumnType("DATETIME");
        }
    }
}
