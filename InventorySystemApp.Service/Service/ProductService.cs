using AutoMapper;
using CSharpFunctionalExtensions;
using InventorySystemApp.Common.Helper;
using InventorySystemApp.Data.IRepository;
using InventorySystemApp.Data;
using InventorySystemApp.Model.Dtos;
using InventorySystemApp.Model.Models;
using InventorySystemApp.Model.ResponseModels;
using InventorySystemApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySystemApp.Data.IRepository;
using InventorySystemApp.Model.ResponseModels;

namespace InventorySystemApp.Service.Services
{
  public class ProductService : IProductService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, DataContext context)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
    public async Task<Result<ResponseModel>> AddProductAsync(ProductDto request)
    {
      var response = new ResponseModel();
      var ProductExist = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.ProductName == request.ProductName);
      //await _context.Products.FirstOrDefaultAsync(x => x. == request.ProductEmail);
      if (ProductExist == null)
      {
        var Product = _mapper.Map<Product>(request);
        await _unitOfWork.ProductRepository.Add(Product);
        try
        {
          await _unitOfWork.SaveAsync();
          response.IsSuccessful = true;
          response.Message = ProductResponseModels.Messages.ProductCreatedSuccessful;
        }
        catch (Exception ex)
        {
          return Result.Failure<ResponseModel>($"{ProductResponseModels.ErrorMessages.ProductCreationFailed} - {ex.Message} : {ex.InnerException}");
        }
      }
      else
      {
        return Result.Failure<ResponseModel>($"{ProductResponseModels.ErrorMessages.ProductCreationFailed}");
      }
      return response;
    }

    public async Task<Result<ResponseModel>> DeleteProductAsync(int id)
    {
      var response = new ResponseModel();
      var Product = await _unitOfWork.ProductRepository.FirstOrDefault(Id => Id.ProductId == id);
      if (Product == null)
      {
        response.Message = ProductResponseModels.ErrorMessages.ProductNotExist;
      }
      try
      {
        //Product.IsDeleted = true;
        //await UpdateProductAsync(id, updateRequest);
        await _unitOfWork.SaveAsync();
        response.IsSuccessful = true;
        response.Message = $"Product with ID:{id} " + ProductResponseModels.Messages.DeleteProductSuccessful;
      }
      catch (Exception ex)
      {
        return Result.Failure<ResponseModel>($"{ProductResponseModels.ErrorMessages.ProductByIdError} - {ex.Message} : {ex.InnerException}");
      }
      return response;
    }

    public async Task<Result<ResponseModel<List<ProductDto>>>> GetAllProductAsync()
    {
      var response = new ResponseModel<List<ProductDto>>();
      try
      {
        var Product = await _unitOfWork.ProductRepository.GetAll();
        var result = _mapper.Map<List<ProductDto>>(Product);
        response.IsSuccessful = true;
        response.Data = result;
      }
      catch (Exception ex)
      {
        return Result.Failure<ResponseModel<List<ProductDto>>>($"{ProductResponseModels.ErrorMessages.ProductByIdError} - {ex.Message} : {ex.InnerException}");
      }
      return response;
    }

    public async Task<Result<ResponseModel<ProductDto>>> GetProductByIdAsync(int id)
    {
      var response = new ResponseModel<ProductDto>();
      var Product = await _unitOfWork.ProductRepository.FirstOrDefault(query => query.ProductId == id);

      if (Product == null)
      {
        response.IsSuccessful = false;
        response.Message = ProductResponseModels.ErrorMessages.ProductNotExist;
      }
      else
      {
        try
        {
          var result = _mapper.Map<ProductDto>(Product);
          response.IsSuccessful = true;
          response.Message = ProductResponseModels.Messages.ProductByIdSuccessful;
          response.Data = result;
        }
        catch (Exception ex)
        {
          return Result.Failure<ResponseModel<ProductDto>>($"{ProductResponseModels.ErrorMessages.ProductByIdError} - {ex.Message} : {ex.InnerException}");
        }
      }
      return response;
    }

    public async Task<Result<ResponseModel>> UpdateProductAsync(int id, ProductDto request)
    {
      var response = new ResponseModel();
      try
      {
        var Product = await _unitOfWork.ProductRepository.FirstOrDefault(query => query.ProductId == id);
        if (Product != null)
        {

          _mapper.Map(request, Product);
          _unitOfWork.ProductRepository.Update(Product);
          await _unitOfWork.SaveAsync();
          response.IsSuccessful = true;
          response.Message = ProductResponseModels.Messages.ProductUpdatedSuccessful;
        }
      }
      catch (Exception ex)
      {
        return Result.Failure<ResponseModel>($"{ProductResponseModels.ErrorMessages.ProductUpdateFailed} - {ex.Message} : {ex.InnerException}");
      }
      return response;
    }
  }
}

