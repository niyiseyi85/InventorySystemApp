using Azure;
using CSharpFunctionalExtensions;
using FluentValidation;
using InventorySystemApp.Common.Helper;
using InventorySystemApp.Model.Dtos;
using InventorySystemApp.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemApp.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InventoryController : ControllerBase
  {
    private readonly IInventoryService _inventoryService;
    private readonly IValidator<InventoryDto> _inventoryDtoRequestValidator;

    public InventoryController(IInventoryService inventoryService, IValidator<InventoryDto> inventoryDtoRequestValidator)
    {
      _inventoryService = inventoryService;
      _inventoryDtoRequestValidator = inventoryDtoRequestValidator;
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllInventory()
    {
      var response = await _inventoryService.GetAllInventoryAsync();
      Result res = Result.Combine(response);
      if (res.IsFailure)
        if (response.Equals(null))
          return BadRequest();
      return Ok(response.Value);
    }
    
    [HttpGet("getInventory")]
    [ProducesDefaultResponseType(typeof(InventoryDto))]
    public async Task<IActionResult> GetInventory(int id)
    {
      var response = await _inventoryService.GetInventoryByIdAsync(id);
      Result res = Result.Combine(response);
      if (res.IsFailure)
        if (response.Equals(null))
          return BadRequest();
      return Ok(response.Value);
    }
    
    [HttpPut("updateInventory/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] InventoryDto request)
    {
      var validateModel = await _inventoryDtoRequestValidator.ValidateAsync(request);
      if (!validateModel.IsValid)
      {
        return BadRequest(validateModel.ToString());
      }
      var response = await _inventoryService.UpdateInventoryAsync(id, request);
      Result res = Result.Combine(response);
      if (res.IsFailure)
        return BadRequest(res.Error);
      return Ok(response.Value);
    }

    [HttpPost("createInventory")]
    public async Task<IActionResult> Create([FromBody] InventoryDto request)
    {
      var validateModel = await _inventoryDtoRequestValidator.ValidateAsync(request);
      if (!validateModel.IsValid)
      {
        return BadRequest(validateModel.ToString());
      }
      var response = await _inventoryService.AddInventoryAsync(request);
      Result res = Result.Combine(response);
      if (res.IsFailure)
        return BadRequest(res.Error);
      return Ok(response.Value);
    }
    [HttpDelete("deleteInventory/{userId:int}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
      return Ok(await _inventoryService.DeleteInventoryAsync(userId));
    }
  }
}
