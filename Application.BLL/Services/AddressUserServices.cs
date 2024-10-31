using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.AddressDto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Services
{
    public class AddressUserServices : IAddressUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddressUserServices(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<BaseResponse<string>> AddAdressUserAsync(AddAdressUserDto addAdressUserDto)
        {
            try
            {
                var user = _userManager.FindByIdAsync(addAdressUserDto.UserId.ToString());
                if (user is null)
                {
                    return new BaseResponse<string>().NotFound("Invalid User");
                }
                await _unitOfWork.tb_UserAddress.AddAsync(_mapper.Map<UserAddress>(addAdressUserDto));
                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success("Susscess");
            }
            catch(Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<AddressUserData>> GetAdressUserAsync(Guid id)
        {
            try
            {
                var data = await _unitOfWork.tb_UserAddress.FindAddressByUserId(id);
                if (data is null)
                {
                    return new BaseResponse<AddressUserData>().NotFound("Invalid User");
                }
                return new BaseResponse<AddressUserData>().Success(_mapper.Map<AddressUserData>(data));
            }
            catch (Exception ex)
            {
                return new BaseResponse<AddressUserData>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<AddressUserData>> UpdateAdressUserAsync(AddAdressUserDto addAdressUserDto)
        {
            try
            {
                var data = await _unitOfWork.tb_UserAddress.FindAddressByUserId(addAdressUserDto.UserId);
                if (data is null)
                {
                    return new BaseResponse<AddressUserData>().NotFound("Invalid User");
                }
                data.Province = addAdressUserDto?.Province;
                data.District = addAdressUserDto?.District;
                data.Ward = addAdressUserDto?.Ward;
                _unitOfWork.tb_UserAddress.Update(data);
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<AddressUserData>().Success(_mapper.Map<AddressUserData>(data));
            }
            catch (Exception ex)
            {
                return new BaseResponse<AddressUserData>().InternalServerError(ex.Message);
            }
        }
    }
}
