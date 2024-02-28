using Azure.Core;
using CSharpFunctionalExtensions;
using FluentValidation;
using InventorySystemApp.Data.IRepository;
using InventorySystemApp.Model.Dtos;
using InventorySystemApp.Service.IService;
using InventorySystemApp.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemApp.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TransactionController : ControllerBase
  {
    private readonly IInventoryTransactionService _inventoryTransactionService;
    private readonly IProductTransactionService _productTransactionService;
    private readonly IValidator<ProduceProductDto> _produceProductDtoValidator;
    private readonly IValidator<SellProductDto> _sellProductDtoValidator;
    private readonly IValidator<PurchaseInventoryDto> _purchaseInventoryDtoValidator;

    public TransactionController(IInventoryTransactionService inventoryTransactionService, IProductTransactionService productTransactionService, IValidator<ProduceProductDto> produceProductDtoValidator,
       IValidator<SellProductDto> sellProductDtoValidator, IValidator<PurchaseInventoryDto> purchaseInventoryDtoValidator)
    {
      _inventoryTransactionService = inventoryTransactionService;
      _productTransactionService = productTransactionService;
      _produceProductDtoValidator = produceProductDtoValidator;
      _sellProductDtoValidator = sellProductDtoValidator;
      _purchaseInventoryDtoValidator = purchaseInventoryDtoValidator;
    }
    
    [HttpPost("produceProduct")]
    public async Task<IActionResult> ProduceProduct(ProduceProductDto request)
    {
      var validateModel = await _produceProductDtoValidator.ValidateAsync(request);
      if (!validateModel.IsValid)
      {
        return BadRequest(validateModel.ToString());
      }
      var response = await _productTransactionService.ProduceAsync(request);
      Result res = Result.Combine(response);
      if (res.IsFailure)
        return BadRequest(res.Error);
      return Ok(response.Value);
    }

    [HttpPost("sellProduct")]
    public async Task<IActionResult> SellProduct(SellProductDto request)
    {
      var validateModel = await _sellProductDtoValidator.ValidateAsync(request);
      if (!validateModel.IsValid)
      {
        return BadRequest(validateModel.ToString());
      }
      var response = await _productTransactionService.SellProduct(request);
      Result res = Result.Combine(response);
      if (res.IsFailure)
        return BadRequest(res.Error);
      return Ok(response.Value);
    }

    [HttpPost("PurchaseInventory")]
    public async Task<IActionResult> PurchaseInventory(PurchaseInventoryDto request)
    {
      var validateModel = await _purchaseInventoryDtoValidator.ValidateAsync(request);
      if (!validateModel.IsValid)
      {
        return BadRequest(validateModel.ToString());
      }
      var response = await _inventoryTransactionService.PurchaseAsync(request);
      Result res = Result.Combine(response);
      if (res.IsFailure)
        return BadRequest(res.Error);
      return Ok(response.Value);
    }
  }
}
