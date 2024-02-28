using FluentValidation;
using InventorySystemApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Model.Dtos
{
  public class ProduceProductDto
  {
    public string? productNumber { get; set; }
    public string productName { get; set; }
    public int quantity { get; set; }
    public double price { get; set; }
    public string? doneBy { get; set; }
  }
  public class ProduceProductDtoValidator : AbstractValidator<ProduceProductDto>
  {
    public ProduceProductDtoValidator()
    {
      RuleFor(x => x.productNumber)
       .NotEmpty().WithMessage("{PropertyName} is required")
       .MaximumLength(30)
       .NotNull();
      RuleFor(x => x.quantity)
       .NotEmpty().WithMessage("{PropertyName} is required")
       .NotNull();
      RuleFor(x => x.price)
      .NotEmpty().WithMessage("{PropertyName} is required")
      .NotNull();
      RuleFor(x => x.doneBy)
      .NotEmpty().WithMessage("{PropertyName} is required")
      .MaximumLength(30)
      .NotNull();
    }
  }
}
