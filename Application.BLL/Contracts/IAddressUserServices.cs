using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.AddressDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts
{
    public interface IAddressUserServices
    {
        Task<BaseResponse<AddressUserData>> GetAdressUserAsync(Guid id);
        Task<BaseResponse<string>> AddAdressUserAsync(AddAdressUserDto addAdressUserDto);
        Task<BaseResponse<AddressUserData>> UpdateAdressUserAsync(AddAdressUserDto addAdressUserDto);
    }
}
