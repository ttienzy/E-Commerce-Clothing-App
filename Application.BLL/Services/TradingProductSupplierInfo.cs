using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Common;
using Application.DAL.Shared.Dtos.InfoProviderDto;
using Application.DAL.Shared.Dtos.ProductDto;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Services
{
    public class TradingProductSupplierInfo : ITradingProductSupplierInfo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICloudinary _cloudinary;
        public TradingProductSupplierInfo(IUnitOfWork unitOfWork,IMapper mapper, ICloudinary cloudinary)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<BaseResponse<string>> CancelOrder(Guid paymentId)
        {
            try
            {
                var payment = await _unitOfWork.tb_OrderPayments.FindByIdAsync(paymentId);
                if (payment == null)
                {
                    return new BaseResponse<string>().NotFound("NOT FOUND");
                }
                payment.OrderInfo = "canceled";
                _unitOfWork.tb_OrderPayments.Update(payment);
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<string>().Success(payment.OrderInfo);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> CreateProviderAsync(ProviderDataDto providerDataDto)
        {
            try
            {
                var result = _mapper.Map<Providers>(providerDataDto);
                await _unitOfWork.tb_Providers.AddAsync(result);
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<string>().Success("Added infomation to the supplier");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().BadRequest(ex.Message);
            }
        }

        public async Task<BaseResponse<ProviderDataDto?>> FindProviderAsync(string PhoneNumber)
        {
            try
            {
                var result = await _unitOfWork.tb_Providers.FindByTelNoAsync(PhoneNumber);
                if (result is null)
                {
                    return new BaseResponse<ProviderDataDto?>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }
                return new BaseResponse<ProviderDataDto?>().Success(_mapper.Map<ProviderDataDto>(result));
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProviderDataDto?>().InternalServerError(ex.Message);
            }
        }
        public async Task<BaseResponse<List<ProviderDataDto>>> ListProviderAsync()
        {
            try
            {
                var data = await _unitOfWork.tb_Providers.GetAllAsync();
                if(data.Count is 0 )
                {
                    return new BaseResponse<List<ProviderDataDto>>().NotFound(ErrorMessage.EMPTY_RECORD);
                }
                return new BaseResponse<List<ProviderDataDto>>().Success(_mapper.Map<List<ProviderDataDto>>(data));
            }
            catch(Exception e)
            {
                return new BaseResponse<List<ProviderDataDto>>().InternalServerError(e.Message);
            }
        }

        public async Task<BaseResponse<int>> PaymentUnPaidById(Guid UserId)
        {
            try
            {
                var data = await _unitOfWork.tb_OrderPayments.GetCountUnPaidById(UserId);
                return new BaseResponse<int>().Success(data);
            }
            catch(Exception ex)
            {
                return new BaseResponse<int>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<ProviderDataDto>>> SearchProviderByNameAsync(string nameProvider)
        {
            try
            {
                var data = await _unitOfWork.tb_Providers.FindByCondition(c => c.NameProvider.Contains(nameProvider)).ToListAsync();
                if (!data.Any())
                {
                    return new BaseResponse<List<ProviderDataDto>>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }
                return new BaseResponse<List<ProviderDataDto>>().Success(_mapper.Map<List<ProviderDataDto>>(data));
            }
            catch(Exception e)
            {
                return new BaseResponse<List<ProviderDataDto>>().InternalServerError(e.Message);
            }
        }

        public async Task<BaseResponse<string>> TransactionClient(ProductShipDto productShipDto)
        {
            try
            {
                var product = await _unitOfWork.tb_Products.FindByIdAsync(productShipDto.ProductId);
                if (product is null)
                {
                    return new BaseResponse<string>().NotFound("NOT FOUNT Product");
                }
                product.Quantity -= productShipDto.quatity;
                _unitOfWork.tb_Products.Update(product);
                var orders = new Orders
                {
                    UserId = productShipDto.UserId,
                    TotalOrderMoney = productShipDto.price * productShipDto.quatity
                };
                await _unitOfWork.tb_Orders.AddAsync(orders);
                var orderItem = new OrderItems
                {
                    OrderId = orders.Id,
                    ProductId = productShipDto.ProductId,
                    QuantityProductOrder = productShipDto.quatity,
                    Price = product.Price,
                };
                await _unitOfWork.tb_OrderItems.AddAsync(orderItem);
                var payment = new OrderPayment
                {
                    ProviderPayment = "Offline",
                    Amount = productShipDto.price,
                    OrderInfo = "unpaid",
                    CreatedAt = DateTime.Now,
                    OrderId = orders.Id,
                };
                await _unitOfWork.tb_OrderPayments.AddAsync(payment);

                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<string>().Success("Success");
                
            }
            catch(Exception e)
            {
                return new BaseResponse<string>().InternalServerError(e.Message);
            }
        }

        public async Task<BaseResponse<InfoTransactionProviderDto>> TransactionProviderAsync(InfoTransactionProviderDto infoTransactionProviderDto)
        {
            IDbContextTransaction transaction = default!;
            try
            {
                using (transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    var provider = await _unitOfWork.tb_Providers.FindByTelNoAsync(infoTransactionProviderDto.TelNo);
                    if (provider is null)
                    {
                        return new BaseResponse<InfoTransactionProviderDto>().NotFound(ErrorMessage.NO_EXIST_PROVIDER);
                    }

                    Receipts newReceipt = new Receipts()
                    {
                        ProviderId = provider.Id,
                        TotalReceiptMoney = infoTransactionProviderDto.Quantity * infoTransactionProviderDto.UnitPrice
                    };
                    await _unitOfWork.tb_Receipts.AddAsync(newReceipt);

                    Products newProduct = null;
                    var product = await _unitOfWork.tb_Products.FindByNameAsync(infoTransactionProviderDto.NameProduct);
                    if (product is null)
                    {
                        newProduct = new Products()
                        {
                            Id = Guid.NewGuid(),
                            Name = infoTransactionProviderDto.NameProduct,
                            ImageProducts = LinkUploadImage(infoTransactionProviderDto.formFile),
                            Quantity = infoTransactionProviderDto.Quantity,
                            Price = infoTransactionProviderDto.UnitPrice ,
                            CategoryId = infoTransactionProviderDto.CategoryId,
                            DiscountId = infoTransactionProviderDto.DiscountId
                        };
                        await _unitOfWork.tb_Products.AddAsync(newProduct);
                    }
                    var receiptItems = new ReceiptItems()
                    {
                        ReceiptId = newReceipt.Id,
                        ProductId = (newProduct is null) ? product.Id : newProduct.Id,
                        Quantity = infoTransactionProviderDto.Quantity,
                        UnitPrice = infoTransactionProviderDto.UnitPrice,
                    };
                    await _unitOfWork.tb_ReceiptsItems.AddAsync(receiptItems);

                    if (newProduct is null)
                    {
                        product.Quantity += infoTransactionProviderDto.Quantity;
                        product.Price = infoTransactionProviderDto.UnitPrice;
                        _unitOfWork.tb_Products.Update(product);
                    }
                    await _unitOfWork.SaveChangeAsync();

                    await transaction.CommitAsync();
                    return new BaseResponse<InfoTransactionProviderDto>().Success(infoTransactionProviderDto);

                }
            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();
                return new BaseResponse<InfoTransactionProviderDto>().InternalServerError(e.InnerException.Message);
            }
        }

        public async Task<BaseResponse<string>> UpdateProviderAsync(string PhoneNumber)
        {
            try
            {
                var data = await _unitOfWork.tb_Providers.FindByTelNoAsync(PhoneNumber);
                if (data is null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }
                _unitOfWork.tb_Providers.Update(data);
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<string>().Success("Updated success");
            }
            catch (Exception e)
            {
                return new BaseResponse<string>().InternalServerError(e.Message);
            }
        }
        private string LinkUploadImage(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = true
                };

                var uploadResult = _cloudinary.Upload(uploadParams);
                return uploadResult.SecureUrl.ToString();
            }
        }
    }
}
