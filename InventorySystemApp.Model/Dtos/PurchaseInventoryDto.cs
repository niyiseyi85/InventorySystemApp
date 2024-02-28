using FluentValidation;
using InventorySystemApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Model.Dtos
{
  public class PurchaseInventoryDto
  {
    public string poNumber { get; set; }
    public string inventoryName { get; set; }
    public int quantity { get; set; }
    public double price { get; set; }
    public string doneBy { get; set; }
  }
  public class PurchaseInventoryDtoValidator : AbstractValidator<PurchaseInventoryDto>
  {
    public PurchaseInventoryDtoValidator()
    {
      RuleFor(x => x.poNumber)
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
          .NotNull();


    }
  }
}
