using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Common;
using Application.DAL.Shared.Dtos.CartDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<string>> AddCartDto(AddCartDto addCartDto)
        {
            try
            {
                var x = _mapper.Map<Cart>(addCartDto);
                await _unitOfWork.tb_Cart.AddAsync(_mapper.Map<Cart>(addCartDto));
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<string>().Success("Success");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> DeleteCart(Guid cartId)
        {
            try
            {
                var result = await _unitOfWork.tb_Cart.FindByIdAsync(cartId);
                if (result == null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }
                _unitOfWork.tb_Cart.Delete(result);
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<string>().Success("Success");
            }catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<CartDataDto>>> GetAllCartByUserId(Guid userId)
        {
            try
            {
                var data = await _unitOfWork.tb_Cart.GetAllCartByUserId(userId);
                return new BaseResponse<List<CartDataDto>>().Success(data);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CartDataDto>>().InternalServerError(ex.Message);
            }
        }

        public int GetCountCart(Guid id)
        {
            var data = _unitOfWork.tb_Cart.FindByCondition(e => e.UserId.Equals(id)).Count();
            return data;
        }
    }
}
