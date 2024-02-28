using CSharpFunctionalExtensions;
using FluentValidation;
using InventorySystemApp.Model.Dtos;
using InventorySystemApp.Service.IServices;
using InventorySystemApp.Service.Service;
using InventorySystemApp.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystemApp.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
   
    private readonly IProductService _productService;
    private readonly IValidator<ProductDto> _productDtoRequestValidator;

    public ProductController(IProductService ProductService, IValidator<ProductDto> ProductDtoRequestValidator)
    {
      _productService = ProductService;
      _productDtoRequestValidator = ProductDtoRequestValidator;
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllProduct()
    {
      var response = await _productService.GetAllProductAsync();
      Result res = Result.Combine(response);
      if (res.IsFailure)
        if (response.Equals(null))
          return BadRequest();
      return Ok(response.Value);
    }

    [HttpGet("getProduct")]
    [ProducesDefaultResponseType(typeof(ProductDto))]
    public async Task<IActionResult> GetProduct(int id)
    {
      var response = await _productService.GetProductByIdAsync(id);
      Result res = Result.Combine(response);
      if (res.IsFailure)
        if (response.Equals(null))
          return BadRequest();
      return Ok(response.Value);
    }

    [HttpPut("updateProduct/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDto request)
    {
      var validateModel = await _productDtoRequestValidator.ValidateAsync(request);
      if (!validateModel.IsValid)
      {
        return BadRequest(validateModel.ToString());
      }
      var response = await _productService.UpdateProductAsync(id, request);
      Result res = Result.Combine(response);
      if (res.IsFailure)
        return BadRequest(res.Error);
      return Ok(response.Value);
    }
    [HttpPost("createProduct")]
    public async Task<IActionResult> Update( [FromBody] ProductDto request)
    {
      var validateModel = await _productDtoRequestValidator.ValidateAsync(request);
      if (!validateModel.IsValid)
      {
        return BadRequest(validateModel.ToString());
      }
      var response = await _productService.AddProductAsync(request);
      Result res = Result.Combine(response);
      if (res.IsFailure)
        return BadRequest(res.Error);
      return Ok(response.Value);
    }

    [HttpDelete("deleteProduct/{userId:int}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
      return Ok(await _productService.DeleteProductAsync(userId));
    }
  }
}
