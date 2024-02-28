using CSharpFunctionalExtensions;
using InventorySystemApp.Common.Helper;
using InventorySystemApp.Data;
using InventorySystemApp.Data.IRepository;
using InventorySystemApp.Data.Repository;
using InventorySystemApp.Model.Dtos;
using InventorySystemApp.Model.Models;
using InventorySystemApp.Model.ResponseModels;
using InventorySystemApp.Service.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Service.Service
{
  public class InventoryTransactionService : IInventoryTransactionService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly DataContext db;

    public InventoryTransactionService(IUnitOfWork unitOfWork, DataContext db)
    {
      _unitOfWork = unitOfWork;
      this.db = db;
    }
    public async Task<Result<ResponseModel<IEnumerable<InventoryTransaction>>>> GetInventoryTransactionAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? type)
    {
      var response = new ResponseModel<IEnumerable<InventoryTransaction>>();

      DateTime dt = dateTo.Value.AddDays(1);
      var query = from it in db.InventoryTransactions
                  join inv in db.Inventory on it.InventoryId equals inv.InventoryId
                  where (string.IsNullOrWhiteSpace(inventoryName) ||
                  inv.InventoryName.ToLower().IndexOf(inventoryName.ToLower()) >= 0 &&
                  (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date) &&
                  (!dateTo.HasValue || it.TransactionDate <= dt.Date) &&
                  (!type.HasValue || it.ActivtyType == type))
                  select it;

      var inventoryList =  await query.Include(x => x.Inventory).ToListAsync();
      response.Data = inventoryList;

      return response;

    }
    public async Task<Result<ResponseModel>> PurchaseAsync(PurchaseInventoryDto request)
    {
      var response = new ResponseModel();

      var inventory = await _unitOfWork.InventoryRepository.FirstOrDefault(x => x.InventoryName.ToLower() == request.inventoryName.ToLower());

      if (inventory == null)
      {
        try
        {
          db.InventoryTransactions.Add(new InventoryTransaction
          {
            PONumber = request.poNumber,
            InventoryId = inventory.InventoryId,
            QualityBefore = inventory.Quantity,
            ActivtyType = InventoryTransactionType.PurchaseInventory,
            QuantityAfter = inventory.Quantity + request.quantity,
            TransactionDate = DateTime.Now,
            DoneBy = request.doneBy,
            UnitPrice = request.price * request.quantity
          });

          inventory.Quantity += request.quantity;
          _unitOfWork.InventoryRepository.Update(inventory);

          await _unitOfWork.SaveAsync();

        }
        catch (Exception ex)
        {
          return Result.Failure<ResponseModel>($"{InventoryResponseModel.ErrorMessages.PurchaseError} - {ex.Message} : {ex.InnerException}");
        }
      }
      else
      {
        return Result.Failure<ResponseModel>($"{InventoryResponseModel.ErrorMessages.InventoryNotFoundError}");
      }

      return response;
    }
  }
}
