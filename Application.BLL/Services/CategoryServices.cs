using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Common;
using Application.DAL.Shared.Dtos.CategoryDto;
using Application.DAL.Shared.Dtos.DiscountDto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<string>> AddRelationCategoryFroProductsAsync(Guid ProductId, Guid categoryId)
        {
            try
            {
                var product = await _unitOfWork.tb_Products.FindByIdAsync(ProductId);
                if (product is null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.NOT_FOUND_PRODUCT);
                }

                var category = await _unitOfWork.tb_Categories.FindByIdAsync(categoryId);
                if (category is null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }


                product.CategoryId = categoryId;
                _unitOfWork.tb_Products.Update(product);

                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success($"Create category for {product.Name} success");

            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            try
            {
                var newDiscount = _mapper.Map<Categories>(categoryCreateDto);
                await _unitOfWork.tb_Categories.AddAsync(newDiscount);
                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success("Created success");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> DeleteCategory(Guid id)
        {
            try
            {
                var category = await _unitOfWork.tb_Categories.FindByIdAsync(id);
                if (category is null)
                {
                    return new BaseResponse<string>().Success(ErrorMessage.RECORD_NOT_FOUND);
                }
                _unitOfWork.tb_Categories.Delete(category);
                
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<string>().Success("Delete Category success");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<CategoryDto>>> GetAsync()
        {
            try
            {
                var data = await _unitOfWork.tb_Categories.GetAllAsync();
                if (data.Count == 0)
                {
                    return new BaseResponse<List<CategoryDto>>().BadRequest(ErrorMessage.EMPTY_RECORD);
                }
                var datadtos = _mapper.Map<List<CategoryDto>>(data);

                return new BaseResponse<List<CategoryDto>>().Success(datadtos);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CategoryDto>>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<CategoryDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.tb_Categories.FindByIdAsync(id);
                if (result is null)
                {
                    return new BaseResponse<CategoryDto>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }
                var dtomaps = _mapper.Map<CategoryDto>(result);
                return new BaseResponse<CategoryDto>().Success(dtomaps);
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryDto>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> UpdateCaregory(Guid categoryId, CategoryCreateDto categoryCreateDto)
        {
            try
            {
                var category = await _unitOfWork.tb_Categories.FindByIdAsync(categoryId);
                if (category is null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }

                category.Name = categoryCreateDto.Name;
                category.Description = categoryCreateDto.Description;
                category.UpdatedAt = categoryCreateDto.UpdatedAt;
                _unitOfWork.tb_Categories.Update(category);


                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success($"Update discount for {category.Id} success");

            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }
    }
}
