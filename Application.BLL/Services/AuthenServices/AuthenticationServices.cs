using Application.BLL.Contracts.IAuthenticationServices;
using Application.BLL.Contracts.TokenService;
using Application.BLL.Helper;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.AuthenticationDto;
using Application.DAL.Shared.UserDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;


namespace Application.BLL.Services.AuthenServices
{
    public class AuthenticationServices : IAuthentication
    {
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenServices _tokenService;
        private readonly IConfiguration _configuration;
        public AuthenticationServices(IMemoryCache memoryCache
            , UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            , IUnitOfWork unitOfWork
            , ITokenServices tokenService
            , IConfiguration configuration
            )
        {
            _memoryCache = memoryCache;
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _configuration = configuration;

        }

        private string GenerateRandomSixDigits()
        {
            var random = new Random();
            int digits = random.Next(100000, 999999);
            return digits.ToString();
        }
        public async Task<BaseResponse<string>> SetInfo(Guid UserId,SetInfoDto setInfoDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(UserId.ToString());
                if (user == null) 
                    return new BaseResponse<string>().NotFound("Not found");
                user.UserName = setInfoDto.UserName;
                user.Email = setInfoDto.Email;
                user.PhoneNumber = setInfoDto.PhoneNumber;
                user.Modified_At = setInfoDto.UpdatedAt;

                await _userManager.UpdateAsync(user);
                return new BaseResponse<string>().Success("Success");
            }
            catch(Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }
        public async Task<BaseResponse<string>> ForgotPassword(string Email)
        {
            var validateUser = await _userManager.FindByEmailAsync(Email);
            if (validateUser is null)
            {
                return new BaseResponse<string>().BadRequest("User isn't exists");
            }
            
            var createOTP = GenerateRandomSixDigits();

            _memoryCache.Set("GMAIL", Email, DateTime.Now.AddMinutes(10));
            _memoryCache.Set("OTP", createOTP, DateTime.Now.AddMinutes(10));

            EmailSender.SendEmailWithGoogleSMTP(_configuration["smtp:from_email"]
                , _configuration["smtp:from_password"]
                , createOTP
                , Email);

            return new BaseResponse<string>().Success("Enter the OTP code sent to your email :");
        }
        public bool ValidateOTP(string OTPSent)
        {
            if (OTPSent != _memoryCache.Get("OTP").ToString())
            {
                return false;
            }
            
            _memoryCache.Remove("OTP");
            return true;
        }
        public async Task<BaseResponse<string>> ResetPassword(string newpassword)
        {
            try
            {
                var validateUser = await _userManager.FindByEmailAsync(_memoryCache.Get("GMAIL").ToString());
                if (validateUser is null)
                {
                    return new BaseResponse<string>().BadRequest("User isn't exists");
                }
                _memoryCache.Remove("GMAIL");
                var token = await _userManager.GeneratePasswordResetTokenAsync(validateUser);

                var result = await _userManager.ResetPasswordAsync(validateUser, token, newpassword);
                if (!result.Succeeded)
                    return new BaseResponse<string>().BadRequest("invalid password");

                return new BaseResponse<string>().Success("reset password success");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }
        public async Task<BaseResponse<object>> Login(LogInDto logInDTO)
        {

            //Check Email
            var user = await _userManager.FindByEmailAsync(logInDTO.Email);
            if (user == null)
            {
                return new BaseResponse<object>().BadRequest("Error : Email isn't exists");
            }
            var pass = await _signInManager.CheckPasswordSignInAsync(user, logInDTO.Password, false);
            if (!pass.Succeeded)
            {
                return new BaseResponse<object>().BadRequest($"Password invalid ");
            }

            //Create accesstoken && refreshtoken
            var claims = await _tokenService.Claims(user);
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            //Save refreshToken in database
            //(user.RefreshToken, user.ExpriredToken) = refreshToken;
            //await _userManager.UpdateAsync(user);

            return new BaseResponse<object>().Success(new
            {
                AccessToken = accessToken,
                client_id = user.Id,
            });

        }

        //public async Task<BaseResponse<RefreshTokenDto>> Refresh(RefreshTokenDto authenticatedResponse)
        //{
        //    string accessToken = authenticatedResponse.AccessToken;
        //    string refreshToken = authenticatedResponse.RefreshToken;

        //    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        //    if (user == null || user.ExpriredToken < DateTime.UtcNow)
        //    {
        //        return new BaseResponse<RefreshTokenDTO>().Unauthorized("Your account has expired, please log in again .");
        //    }

        //    var principal = _tokenService.GetPrincipalFromTokenExpired(accessToken);
        //    var x = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
        //    if (principal.Identity.Name is null)
        //    {
        //        return new BaseResponse<RefreshTokenDTO>().Unauthorized("Invalid token .");
        //    }

        //    var newAccessToken = await _tokenService.GenerateAccessToken(principal.Claims.ToList());

        //    return new BaseResponse<RefreshTokenDTO>().Success(new RefreshTokenDTO { AccessToken = newAccessToken, RefreshToken = refreshToken });
        //}

        public async Task<BaseResponse<string>> Logout()
        {
            await _signInManager.SignOutAsync();
            return new BaseResponse<string>().Success("User logged out");
        }

        public async Task<BaseResponse<string>> Register(RegisterDto registerDTO)
        {
            //Check if user already exits
            var checkUser = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (checkUser is not null)
            {
                return new BaseResponse<string>().BadRequest($"Error : User with this email already exits .");
            }

            var user = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
            };
            try
            {

                //Create User & update password hash
                var results = await _userManager.CreateAsync(user, registerDTO.Password);
                if (!results.Succeeded)
                {
                    return new BaseResponse<string>().BadRequest($"User creation failed : {string.Join(",", results.Errors.Select(err => err.Description))}");
                }

                //Add Claims
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, registerDTO.UserName),
                new Claim(ClaimTypes.Email, registerDTO.Email)
            };
                var addClaimsResult = await _userManager.AddClaimsAsync(user, claims);
                if (!addClaimsResult.Succeeded)
                {
                    return new BaseResponse<string>().BadRequest($"Claims creation failed : {string.Join(",", addClaimsResult.Errors.Select(e => e.Description))}");
                }

                //Add Role
                var role = "User";
                var addRoleResult = await _userManager.AddToRoleAsync(user, role);
                if (!addRoleResult.Succeeded)
                {
                    return new BaseResponse<string>().BadRequest($"Role creation failed : {string.Join(",", addRoleResult.Errors.Select(e => e.Description))}");
                }

                // Add Address
                var address = new UserAddress
                {
                    UserId = user.Id,
                    Province = registerDTO.Province,
                    District = registerDTO.District,
                    Ward = registerDTO.Ward,
                };
                await _unitOfWork.tb_UserAddress.AddAsync(address);
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                await _userManager.DeleteAsync(user); //Rollback user
                return new BaseResponse<string>().BadRequest($"Customer creation failed : {ex.Message}");
            }

            return new BaseResponse<string>().Success("Register user success .");
        }

        public async Task<BaseResponse<ApplicationUser?>> ListInfoUser(Guid client_id)
        {
            try
            {
                var data = await _userManager.Users
                        .Include(e => e.address)
                        .Include(e => e.orders)
                        .FirstOrDefaultAsync(p => p.Id == client_id);

                
                if (data == null)
                {
                    return new BaseResponse<ApplicationUser?>().BadRequest("No record");
                }
                return new BaseResponse<ApplicationUser?>().Success(data);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ApplicationUser?>().InternalServerError(ex.Message);
            }
        }
    }
}
