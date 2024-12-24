using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly dbContext _context;

        private ICategoriesRepository categoriesRepository;
        private IDiscountsRepository discountsRepository;
        private IIventoriesRepository inventoriesRepository;
        private IOrderPaymentsRepository orderPaymentsRepository;
        private IOrderItemsRepository orderItemsRepository;
        private IOrdersRepository ordersRepository;
        private IProductsRepository productsRepository;
        private IProvidersRepository providersRepository;
        private IReceiptItemsRepository receiptItemsRepository;
        private IReceiptsRepository receiptsRepository;
        private IUserAddressRepository userAddressRepository;
        private IApplicationUserRepository applicationUserRepository;
        private ICartRepository cartRepository;
        public UnitOfWork(dbContext dbContext)
        {
            _context = dbContext;
        }
        public IApplicationUserRepository tb_ApplicationUser => applicationUserRepository ??= new ApplicationUserRepository(_context);
        public ICategoriesRepository tb_Categories => categoriesRepository ??= new CategoriesRepository(_context);

        public IDiscountsRepository tb_Discounts => discountsRepository ??= new DiscountsRepository(_context);

        public IIventoriesRepository tb_Inventory => inventoriesRepository ??= new InventoriesRepository(_context);

        public IOrderPaymentsRepository tb_OrderPayments => orderPaymentsRepository ??= new OrderPaymentsRepository(_context);

        public IOrderItemsRepository tb_OrderItems => orderItemsRepository ??= new OrderItemsRepository(_context);

        public IOrdersRepository tb_Orders => ordersRepository ??= new OrdersRepository(_context);

        public IProductsRepository tb_Products => productsRepository ??= new ProductsRepository(_context);

        public IProvidersRepository tb_Providers => providersRepository ??= new ProvidersRepository(_context);

        public IReceiptsRepository tb_Receipts => receiptsRepository ??= new ReceiptsRepository(_context);

        public IReceiptItemsRepository tb_ReceiptsItems => receiptItemsRepository ??= new ReceiptItemsRepository(_context);

        public IUserAddressRepository tb_UserAddress => userAddressRepository ??= new UserAddressRepository(_context);

        public ICartRepository tb_Cart => cartRepository ??= new CartRepository(_context);

        public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

        public async Task<bool> SaveChangeAsync() => await _context.SaveChangesAsync() > 0;
    }
}
