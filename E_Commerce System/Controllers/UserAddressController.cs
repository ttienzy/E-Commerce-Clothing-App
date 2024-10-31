using Application.BLL.Contracts;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.AddressDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly IAddressUserServices _addressUserServices;
        public UserAddressController(IAddressUserServices addressUserServices)
        {
            _addressUserServices = addressUserServices;
        }

        [HttpGet("GetAddress")]
        public async Task<IActionResult> GetAddress([FromQuery] Guid id)
        {
            var data = await _addressUserServices.GetAdressUserAsync(id);
            if(data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<AddressUserData>().Error(data);
        }

        [HttpPost("AddUserAddress")]
        public async Task<IActionResult> AddUserAddress([FromBody] AddAdressUserDto addAdressUserDto)
        {
            var data = await _addressUserServices.AddAdressUserAsync(addAdressUserDto);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }

        [HttpPut("UpdateUserAddress")]
        public async Task<IActionResult> UpdateUserAddress([FromBody] AddAdressUserDto addAdressUserDto)
        {
            var data = await _addressUserServices.UpdateAdressUserAsync(addAdressUserDto);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<AddressUserData>().Error(data);
        }
    }
}
