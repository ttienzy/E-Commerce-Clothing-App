using Application.BLL.Contracts.IAuthenticationServices;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.AuthenticationDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _authen;
        public AuthenticationController(IAuthentication authen)
        {
            _authen = authen;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Register details is required");
            }
            var data = await _authen.Register(registerDTO);
            if (data.success)
                return Ok(data);
            return new ErrorResponse<string>().Error(data);
        }

        [HttpPost("LogInUser")]
        public async Task<IActionResult> LogIn([FromBody] LogInDto logInDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("LogIn details is required");
            }
            var results = await _authen.Login(logInDTO);
            if (results.success)
                return Ok(results.response);
            return new ErrorResponse<object>().Error(results);
        }
        [HttpGet("LogoutAsync")]
        public async Task<IActionResult> LogoutAsync()
        {
            return Ok(await _authen.Logout());
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromQuery] string Email)
        {
            var results = await _authen.ForgotPassword(Email);
            if (results.success) return Ok("Redirect to SentOTP");
            return new ErrorResponse<string>().Error(results);
        }

        [HttpPost("SentOTP")]
        public IActionResult SentOTP([FromBody] string OTP)
        {
            var result = _authen.ValidateOTP(OTP);
            if (result)
                return Ok("Redirect to ResetPassword");
            return BadRequest("OTP is in correct");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] string newPassword)
        {
            var result = await _authen.ResetPassword(newPassword);
            if (result.success) 
                return Ok(result);

            return new ErrorResponse<string>().Error(result);
        }

        [HttpGet("ListUser")]
        public async Task<IActionResult> ListUser([FromQuery] Guid client_id)
        {
            var data = await _authen.ListInfoUser(client_id);
            if (data.success) return Ok(data.response);
            return new ErrorResponse<ApplicationUser>().Error(data);
        }

        [HttpPut("SetInfo")]
        public async Task<IActionResult> SetInfo([FromQuery] Guid UserId,[FromBody] SetInfoDto setInfoDto)
        {
            var data = await _authen.SetInfo(UserId, setInfoDto);
            if (data.success) return Ok(data.response);
            return BadRequest(data);
        }













        //[Authorize(Roles = "User")]
        [HttpPost("demo01")]
        public IActionResult demo01()
        {
            return Ok("This is user role");
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("demo02")]
        public IActionResult demo02()
        {
            return Ok("This is user role");
        }
    }
}
