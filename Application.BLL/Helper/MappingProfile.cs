using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.AddressDto;
using Application.DAL.Shared.Dtos.CartDTO;
using Application.DAL.Shared.Dtos.CategoryDto;
using Application.DAL.Shared.Dtos.DiscountDto;
using Application.DAL.Shared.Dtos.InfoProviderDto;
using Application.DAL.Shared.Dtos.InventoryDto;
using Application.DAL.Shared.Dtos.ProductDto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateProviderMaps();
            CreateDiscountMaps();
            CreateProductMaps();
            CreateInventoryMaps();
            CreateCategoryMaps();
            CreateAddressMaps();
            CreateCartMaps();
        }
        public void CreateProviderMaps()
        {
            CreateMap<Providers, ProviderDataDto>().ReverseMap();

        }
        public void CreateDiscountMaps()
        {
            CreateMap<Discounts, DiscountDataDto>();
                //.ForMember(dest => dest.products, src => src.MapFrom(s => s.products));
            CreateMap<Discounts, DiscountCreateDto>().ReverseMap();
        }
        public void CreateProductMaps()
        {
            CreateMap<Products, ProductDiscountDto>();
            CreateMap<Products, ProductInventory>()
                .ForMember(dest => dest.NameProduct, src => src.MapFrom(s => s.Name));
            CreateMap<Products, ProductData>().ReverseMap();
            CreateMap<Products, ProductIncludedDiscountDto>()
                .ForMember(dest => dest.DiscountPercent, src => src.MapFrom(x => x.discounts.Discount_percent));
        }
        public void CreateInventoryMaps()
        {
            CreateMap<Inventories, InventoryDataDto>().ReverseMap();
        }
        public void CreateCategoryMaps()
        {
            CreateMap<Categories, CategoryDto>().ReverseMap();
            CreateMap<Categories, CategoryCreateDto>().ReverseMap();
;       }
        public void CreateAddressMaps()
        {
            CreateMap<UserAddress, AddAdressUserDto>().ReverseMap();
            CreateMap<UserAddress, AddressUserData>().ReverseMap();
        }
        public void CreateCartMaps()
        {
            CreateMap<Cart,AddCartDto>().ReverseMap();  
        }
    }
}
