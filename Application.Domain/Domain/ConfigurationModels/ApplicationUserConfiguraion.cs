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
    public class ApplicationUserConfiguraion : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.UserName)
                .HasColumnType("NVARCHAR(55)")
                .IsRequired();
            builder.Property(p => p.PhoneNumber)
                .HasColumnType("VARCHAR(11)")
                .HasMaxLength(11)
                .IsRequired();
            builder.Property(p => p.Email)
                .HasMaxLength(125);
            builder.Property(p => p.Create_At)
                .HasColumnType("DATETIME");
            builder.Property(p => p.Modified_At)
                .HasColumnType("DATETIME");
        }
    }
}
