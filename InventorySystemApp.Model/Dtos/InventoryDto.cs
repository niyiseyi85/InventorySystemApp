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
  public class InventoryDto
  {
    public string? InventoryName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
  }
  public class InventoryDtoValidator : AbstractValidator<InventoryDto>
  {
    public InventoryDtoValidator()
    {
      RuleFor(x => x.InventoryName)
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
  public class InventoryDtoRequestMappingConfig : Profile
  {
    public InventoryDtoRequestMappingConfig()
    {
      CreateMap<Inventory, InventoryDto>().ReverseMap();
    }
  }
}
