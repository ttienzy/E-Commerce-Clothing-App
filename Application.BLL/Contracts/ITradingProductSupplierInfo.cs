using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.InfoProviderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts
{
    public interface ITradingProductSupplierInfo
    {
        Task<BaseResponse<List<ProviderDataDto>>> ListProviderAsync();
        Task<BaseResponse<string>> CreateProviderAsync(ProviderDataDto providerDataDto);
        Task<BaseResponse<string>> UpdateProviderAsync(string PhoneNumber);
        Task<BaseResponse<ProviderDataDto?>> FindProviderAsync(string PhoneNumber);
        Task<BaseResponse<InfoTransactionProviderDto>> TransactionProviderAsync(InfoTransactionProviderDto infoTransactionProviderDto);
        Task<BaseResponse<List<ProviderDataDto>>> SearchProviderByNameAsync(string nameProvider);
    }
}
