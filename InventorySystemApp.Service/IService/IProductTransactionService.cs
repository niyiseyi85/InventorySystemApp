using CSharpFunctionalExtensions;
using InventorySystemApp.Common.Helper;
using InventorySystemApp.Model.Dtos;
using InventorySystemApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Service.IService
{
  public interface IProductTransactionService
  {
    Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? type);
    Task<Result<ResponseModel>> ProduceAsync(ProduceProductDto request);
    Task<Result<ResponseModel>> SellProduct(SellProductDto request);
  }
}
