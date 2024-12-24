using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.ManagerDTO;
using Application.DAL.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts
{
    public interface IPageManagers
    {
        Task<BaseResponse<ManagerInfoDto>> InfoForManager();
        Task<BaseResponse<List<BestSellingProduct>>> SoftByBestSellingProduct();
    }
}
