using Application.BLL.Contracts.IAuthenticationServices;
using Application.DAL.Shared.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleManager;
        public RoleController(IRoleServices roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet("GetRolesAsync")]
        public async Task<IActionResult> GetRolesAsync()
        {
            var roles = await _roleManager.ListRolesAsync();
            if (!roles.success)
                return new ErrorResponse<List<IdentityRole<Guid>>>().Error(roles);
            return Ok(roles.response);
        }
        [HttpPost]
        public async Task<IActionResult> FindRoleById([FromQuery] Guid id)
        {
            var data = await _roleManager.FindRoleByIdAsync(id);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<IdentityRole<Guid>>().Error(data);
        }
        [HttpPost("AddRoleAsync")]
        public async Task<IActionResult> AddRoleAsync([FromBody] string name)
        {
            var newRole = await _roleManager.AddRoleAsync(name);
            if (newRole.success)
            {
                return Ok(newRole);
            }
            return new ErrorResponse<string>().Error(newRole);
        }
        [HttpPut("UpdateRoleAsync")]
        public async Task<IActionResult> UpdateRoleAsync([FromQuery] Guid id, [FromBody] string name)
        {
            var role = await _roleManager.EditRoleAsync(id, name);
            if (role.success)
                return Ok(role);
            return new ErrorResponse<IdentityRole<Guid>>().Error(role);
        }
        [HttpDelete("DeleteRoleAsync")]
        public async Task<IActionResult> DeleteRoleAsync([FromQuery] Guid id)
        {
            var role = await _roleManager.RemoveRoleAsync(id);
            if (role.success)
                return Ok(role);
            return new ErrorResponse<string>().Error(role);
        }
    }
}
