using AutoMapper;
using FluentValidation;
using InventorySystemApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Model.Dtos
{
  public class ProductDto
  {
    public string? ProductName { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }
    public bool IsActive { get; set; } = true;
  }

  public class ProductDtoValidator : AbstractValidator<ProductDto>
  {
    public ProductDtoValidator()
    {
      RuleFor(x => x.ProductName)
        .NotEmpty().WithMessage("{PropertyName} is required")
        .MaximumLength(30)
        .NotNull();
      RuleFor(x => x.Quantity)
        .NotEmpty().WithMessage("{PropertyName}  must be greater or equal to {0}")
        .NotNull();
      RuleFor(x => x.Price)
      .NotEmpty().WithMessage("{PropertyName} must be greater or equal to {0}")
      .NotNull();
    }
  }
  public class ProductDtoRequestMappingConfig : Profile
  {
    public ProductDtoRequestMappingConfig()
    {
      CreateMap<ProductDto, Product>().ReverseMap();
    }
  }
}
