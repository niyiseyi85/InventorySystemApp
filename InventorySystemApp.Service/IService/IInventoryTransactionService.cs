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
  public interface IInventoryTransactionService
  {
    Task<Result<ResponseModel<IEnumerable<InventoryTransaction>>>> GetInventoryTransactionAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? type);
    Task<Result<ResponseModel>> PurchaseAsync(PurchaseInventoryDto request);
  }
}
