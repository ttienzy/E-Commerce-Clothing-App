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
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Province)
                .HasColumnType("NVARCHAR(55)");
            builder.Property(p => p.District)
                .HasColumnType("NVARCHAR(55)");
            builder.Property(p => p.Ward)
                .HasColumnType("NVARCHAR(55)");
            builder.HasOne(p => p.applicationUser)
                .WithOne(f => f.address)
                .HasForeignKey<UserAddress>(fk => fk.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                

               
        }
    }
}
