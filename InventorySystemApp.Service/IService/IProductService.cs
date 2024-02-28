using CSharpFunctionalExtensions;
using InventorySystemApp.Common.Helper;
using InventorySystemApp.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Service.IServices
{
  public interface IProductService
  {
    Task<Result<ResponseModel<List<ProductDto>>>> GetAllProductAsync();
    Task<Result<ResponseModel<ProductDto>>> GetProductByIdAsync(int id);
    Task<Result<ResponseModel>> UpdateProductAsync(int id, ProductDto request);
    Task<Result<ResponseModel>> DeleteProductAsync(int id);
    Task<Result<ResponseModel>> AddProductAsync(ProductDto request);

  }
}
