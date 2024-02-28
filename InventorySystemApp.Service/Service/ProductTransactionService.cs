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
  public class ProductTransactionService : IProductTransactionService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly DataContext db;

    public ProductTransactionService(IUnitOfWork unitOfWork, DataContext db)
    {
      _unitOfWork = unitOfWork;
      this.db = db;
    }
    public async Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? type)
    {
      if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);
      var query = from pt in db.ProductTransactions
                  join prod in db.Products on pt.ProductId equals prod.ProductId
                  where
                  (string.IsNullOrWhiteSpace(productName) || prod.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0) &&
                  (!dateFrom.HasValue || pt.TransactionDate >= dateFrom.Value.Date) &&
                  (!dateTo.HasValue || pt.TransactionDate <= dateTo.Value.Date) &&
                  (!type.HasValue || pt.ActivtyType == type)
                  select pt;

      return await query.Include(x => x.product).ToListAsync();
    }

    public async Task<Result<ResponseModel>> ProduceAsync(ProduceProductDto request)
    {
      var response = new ResponseModel();

      try
      {
        //var prod = await _unitOfWork.ProductRepository.Get(request.product.ProductId);
         
         var prod = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.ProductName.ToLower() == request.productName.ToLower());
        //var prod = await db.products.Include(x => x.ProductInventories).
        //    ThenInclude(x => x.Inventory)
        //    .FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
        if (prod != null)
        {
          foreach (var pi in prod.ProductInventories)
          {
            int qtyBefore = pi.Inventory.Quantity;
            pi.InventoryQuantity = request.quantity + pi.InventoryQuantity;

            await _unitOfWork.InventoryTransactionRepository.Add(new InventoryTransaction
            {
              ProductNumber = request. productNumber,
              InventoryId = pi.Inventory.InventoryId,
              QualityBefore = qtyBefore,
              ActivtyType = InventoryTransactionType.ProduceProduct,
              QuantityAfter = pi.Inventory.Quantity + request.quantity,
              TransactionDate = DateTime.Now,
              DoneBy = request.doneBy,
              UnitPrice = request.price * request.quantity
            });
          }
        }
        else
        {
          return Result.Failure<ResponseModel>($"{ProductResponseModels.ErrorMessages.ProductNotFoundError}  ");
        }


        await _unitOfWork.productTransactionRepository.Add(new ProductTransaction
        {
          ProductNumber = request.productNumber,
          ProductId = prod.ProductId,
          QualityBefore = prod.Quantity,
          ActivtyType = ProductTransactionType.produceProduct,
          QuantityAfter = prod.Quantity + request.quantity,
          TransactionDate = DateTime.Now,
          DoneBy =  request.doneBy,
          UnitPrice = request.price
        });



        prod.Quantity += request.quantity;
        _unitOfWork.ProductRepository.Update(prod);

        await _unitOfWork.SaveAsync();
      }
      catch(Exception ex)
      {
        return Result.Failure<ResponseModel>($"{ProductResponseModels.ErrorMessages.ProduceError} - {ex.Message} : {ex.InnerException}");
      }


      return response;
    }

    public async Task<Result<ResponseModel>> SellProduct(SellProductDto request)
    {
      var response = new ResponseModel();

      var product = await _unitOfWork.ProductRepository.FirstOrDefault(x => x.ProductName.ToLower() == request.productName.ToLower());

      try
      {
        await _unitOfWork.productTransactionRepository.Add(new ProductTransaction
        {
          SaleOrderNumber = request.saleOrderNo,
          ProductId = product.ProductId,
          QualityBefore = product.Quantity,
          QuantityAfter = product.Quantity - request.quantity,
          TransactionDate = DateTime.Now,
          DoneBy = request.doneBy,
          UnitPrice = request.price,
          ActivtyType = ProductTransactionType.sellProduct
        });


        product.Quantity -= request.quantity;
        _unitOfWork.ProductRepository.Update(product);

        await _unitOfWork.SaveAsync();

        response.Message = "product sold";
      }
      catch(Exception ex)
      {
        return Result.Failure<ResponseModel>($"{ProductResponseModels.ErrorMessages.SellProductError} - {ex.Message} : {ex.InnerException}");
      }

      return response;
    }
    
  }

}
