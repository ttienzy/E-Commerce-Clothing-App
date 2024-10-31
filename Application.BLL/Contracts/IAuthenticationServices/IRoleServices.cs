using Application.DAL.Shared.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts.IAuthenticationServices
{
    public interface IRoleServices
    {
        Task<BaseResponse<List<IdentityRole<Guid>>>> ListRolesAsync();
        Task<BaseResponse<IdentityRole<Guid>>> FindRoleByIdAsync(Guid roleId);
        Task<BaseResponse<string>> AddRoleAsync(string nameRole);
        Task<BaseResponse<IdentityRole<Guid>>> EditRoleAsync(Guid id, string nameRole);
        Task<BaseResponse<string>> RemoveRoleAsync(Guid roleId);
    }
}
