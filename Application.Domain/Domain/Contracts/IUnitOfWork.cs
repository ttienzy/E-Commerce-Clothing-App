using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository tb_ApplicationUser { get; }
        ICategoriesRepository tb_Categories { get; }
        IDiscountsRepository tb_Discounts { get; }
        IIventoriesRepository tb_Inventory { get; }
        IOrderPaymentsRepository tb_OrderPayments { get; }
        IOrderItemsRepository tb_OrderItems { get; }
        IOrdersRepository tb_Orders { get; }
        IProductsRepository tb_Products { get; }
        IProvidersRepository tb_Providers { get; }
        IReceiptsRepository tb_Receipts { get; }
        IReceiptItemsRepository tb_ReceiptsItems { get; }
        IUserAddressRepository tb_UserAddress { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<bool> SaveChangeAsync();
    }
}
