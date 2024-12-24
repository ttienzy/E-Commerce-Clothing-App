using Application.DAL.Domain.ConfigurationModels;
using Application.DAL.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;



namespace Application.DAL.DbContextData
{
    public class dbContext : IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid> 
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options) { }

        public DbSet<UserAddress> userAddresses { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<OrderPayment> orderPayment { get; set; }
        public DbSet<OrderItems> orderItems { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Discounts> discounts { get; set; }
        public DbSet<Inventories> inventories { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Providers> providers { get; set; }
        public DbSet<Receipts> receipts { get; set; }
        public DbSet<ReceiptItems> ReceiptItems { get; set; }
        public DbSet<Cart> carts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserConfiguraion());
            builder.ApplyConfiguration(new OrderItemsConfiguration());
            builder.ApplyConfiguration(new OrderPaymentConfiduration());
            builder.ApplyConfiguration(new OrdersConfiguration());
            builder.ApplyConfiguration(new ProductsConfiguration());
            builder.ApplyConfiguration(new ProvidersConfiguration());
            builder.ApplyConfiguration(new ReceiptItemsConfiguration());
            builder.ApplyConfiguration(new ReceiptsConfiguration());
            builder.ApplyConfiguration(new UserAddressConfiguration());
            builder.ApplyConfiguration(new DiscountsConfiguration());
            builder.ApplyConfiguration(new InventoryConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
