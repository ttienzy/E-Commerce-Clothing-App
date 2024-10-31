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
    public class ProvidersConfiguration : IEntityTypeConfiguration<Providers>
    {
        public void Configure(EntityTypeBuilder<Providers> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NameProvider)
                .HasColumnType("NVARCHAR(55)");
            builder.Property(p => p.TelNo)
                .HasColumnType("VARCHAR(11)")
                .HasMaxLength(11);
        }
    }
}
