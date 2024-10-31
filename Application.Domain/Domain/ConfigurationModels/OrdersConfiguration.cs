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
    public class OrdersConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.HasKey(pk => pk.Id);
            builder.Property(p => p.CreatedAt)
                .HasColumnType("DATETIME");
            builder.Property(p => p.TotalOrderMoney)
                .HasColumnType("money");
            builder.HasOne(p => p.applicationUser)
                .WithMany(p => p.orders)
                .HasForeignKey(fk => fk.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
