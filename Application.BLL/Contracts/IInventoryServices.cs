using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.InventoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts
{
    public interface IInventoryServices
    {
        Task<BaseResponse<List<InventoryDataDto>>> GetInventoryIncludeAsync();
        Task<BaseResponse<InventoryDataDto>> GetInventoryByIdAsync(Guid InventoryId);
        //Task<BaseResponse<string>> CreateInventoryAsync(Guid productId);
        Task<BaseResponse<string>> AddInventoryForProductAsync(Guid ProductId, Guid InventoryId);
        Task<BaseResponse<string>> UpdateInventoryAsync(Guid id);
        Task<BaseResponse<string>> DeleteInventoryAsync(Guid id);
    }
}
