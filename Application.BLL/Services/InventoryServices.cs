using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Common;
using Application.DAL.Shared.Dtos.InventoryDto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Services
{
    public class InventoryServices : IInventoryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InventoryServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<string>> AddInventoryForProductAsync(Guid ProductId, Guid InventoryId)
        {
            try
            {
                var product = await _unitOfWork.tb_Products.FindByIdAsync(ProductId);
                if (product == null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.NOT_FOUND_PRODUCT);
                }
                var inventory = await _unitOfWork.tb_Inventory.FindByIdAsync(InventoryId);
                if (inventory == null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }

                product.InventoryId = InventoryId;
                _unitOfWork.tb_Products.Update(product);

                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success("Added success");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        //public async Task<BaseResponse<string>> CreateInventoryAsync(Guid productId)
        //{
        //    try
        //    {
        //        var product = await _unitOfWork.tb_Products.FindByIdAsync(productId);
        //        if (product == null)
        //        {
        //            return new BaseResponse<string>().NotFound(ErrorMessage.NOT_FOUND_PRODUCT);
        //        }
                

        //        await _unitOfWork.SaveChangeAsync();

        //        return new BaseResponse<string>().Success("Added success");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<string>().InternalServerError(ex.Message);
        //    }
        //}

        public async Task<BaseResponse<string>> DeleteInventoryAsync(Guid id)
        {
            try
            {
                var inventory = await _unitOfWork.tb_Inventory.FindByIdAsync(id);
                if (inventory == null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }

                _unitOfWork.tb_Inventory.Delete(inventory);
                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success("Success");
            }
            catch(Exception ex)
            {
                return new BaseResponse<string>().InternalServerError($"{ex.Message}"); 
            }
        }

        public async Task<BaseResponse<InventoryDataDto>> GetInventoryByIdAsync(Guid InventoryId)
        {
            try
            {
                var data = await _unitOfWork.tb_Inventory.FindByIdAsync(InventoryId);
                if (data == null)
                {
                    return new BaseResponse<InventoryDataDto>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }
                return new BaseResponse<InventoryDataDto>().Success(_mapper.Map<InventoryDataDto>(data));
            }
            catch(Exception e)
            {
                return new BaseResponse<InventoryDataDto>().InternalServerError(e.Message);
            }
        }

        public async Task<BaseResponse<List<InventoryDataDto>>> GetInventoryIncludeAsync()
        {
            
            try
            {
                var data = await _unitOfWork.tb_Inventory.GetInventoryIncludeAsync();
                return new BaseResponse<List<InventoryDataDto>>().Success(_mapper.Map<List<InventoryDataDto>>(data));
            }
            catch (Exception e)
            {
                return new BaseResponse<List<InventoryDataDto>>().InternalServerError(e.Message);
            }
        }

        public async Task<BaseResponse<string>> UpdateInventoryAsync(Guid id)
        {

            try
            {
                var data = await _unitOfWork.tb_Inventory.FindByIdAsync(id);
                if (data == null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }
                return new BaseResponse<string>().Success("success");
            }
            catch (Exception e)
            {
                return new BaseResponse<string>().InternalServerError(e.Message);
            }
        }
    }
}
