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
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventories>
    {
        public void Configure(EntityTypeBuilder<Inventories> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.CreateAt)
                .HasColumnType("DATETIME");
            builder.Property(p => p.UpdateAt)
                .HasColumnType("DATETIME");
        }
    }
}
