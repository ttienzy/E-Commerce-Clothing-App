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
    public class ProductsConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                .HasColumnType("NVARCHAR(55)")
                .HasMaxLength(55);
            builder.Property(p => p.Price)
                .HasColumnType("money");
            builder.HasOne(p => p.discounts)
                .WithMany(p => p.products)
                .HasForeignKey(p => p.DiscountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.inventories)
                .WithMany(p => p.products)
                .HasForeignKey(p => p.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.categories)
                .WithMany(p => p.products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
