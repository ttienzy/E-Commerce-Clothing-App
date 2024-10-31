using Application.BLL.Contracts.IAuthenticationServices;
using Application.DAL.Shared.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Services.AuthenServices
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public RoleServices(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<BaseResponse<string>> AddRoleAsync(string nameRole)
        {
            try
            {
                var roles = await _roleManager.FindByNameAsync(nameRole);
                if (roles is not null)
                    return new BaseResponse<string>().BadRequest($"This {nameRole} really exists !");

                var newRole = await _roleManager.CreateAsync(new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = nameRole,
                    NormalizedName = nameRole.ToUpper()
                });
                return new BaseResponse<string>().Success("Add role Success .");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().BadRequest($"{ex.Message}");
            }
        }

        public async Task<BaseResponse<IdentityRole<Guid>>> EditRoleAsync(Guid id, string nameRole)
        {
            try
            {
                var getRoleById = await _roleManager.FindByIdAsync(id.ToString());
                if (getRoleById is null)
                    return new BaseResponse<IdentityRole<Guid>>().BadRequest($"Role {id} is't already exists");
                var getRoleByName = await _roleManager.FindByNameAsync(nameRole);
                if (getRoleByName is not null)
                    return new BaseResponse<IdentityRole<Guid>>().BadRequest($"This {nameRole} really exists !");

                getRoleById.Name = nameRole;
                getRoleById.NormalizedName = nameRole.ToUpper();
                await _roleManager.UpdateAsync(getRoleById);

                return new BaseResponse<IdentityRole<Guid>>().Success(getRoleById);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IdentityRole<Guid>>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<IdentityRole<Guid>>> FindRoleByIdAsync(Guid roleId)
        {
            try
            {
                var getRoleById = await _roleManager.FindByIdAsync(roleId.ToString());
                if (getRoleById is null)
                    return new BaseResponse<IdentityRole<Guid>>().BadRequest($"Role {roleId} is't already exists");
                return new BaseResponse<IdentityRole<Guid>>().Success(getRoleById);
            }
            catch (Exception ex)
            {
                return new BaseResponse<IdentityRole<Guid>>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<IdentityRole<Guid>>>> ListRolesAsync()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                if (roles.Count == 0)
                {
                    return new BaseResponse<List<IdentityRole<Guid>>>().BadRequest("List role is empty");
                }
                return new BaseResponse<List<IdentityRole<Guid>>>().Success(roles);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<IdentityRole<Guid>>>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> RemoveRoleAsync(Guid roleId)
        {
            try
            {
                var getRoleById = await _roleManager.FindByIdAsync(roleId.ToString());
                if (getRoleById is null)
                    return new BaseResponse<string>().BadRequest($"Role {roleId} is't already exists");

                await _roleManager.DeleteAsync(getRoleById);

                return new BaseResponse<string>().Success("Delete Role success .");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }
    }
}
