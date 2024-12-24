using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.AuthenticationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts.IAuthenticationServices
{
    public interface IAuthentication
    {
        Task<BaseResponse<string>> Register(RegisterDto registerDTO);
        Task<BaseResponse<object>> Login(LogInDto logInDTO);
        Task<BaseResponse<string>> Logout();
        Task<BaseResponse<string>> ForgotPassword(string ToEmail);
        bool ValidateOTP(string OTPSent);
        Task<BaseResponse<string>> ResetPassword(string newpassword);
        //Task<BaseResponse<RefreshTokenDto>> Refresh(RefreshTokenDto refreshTokenDTO);
        Task<BaseResponse<ApplicationUser?>> ListInfoUser(Guid client_id);
        Task<BaseResponse<string>> SetInfo(Guid UserId, SetInfoDto setInfoDto);
    }
}
