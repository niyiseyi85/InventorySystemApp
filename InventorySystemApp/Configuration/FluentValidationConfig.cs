using FluentValidation;

namespace InventorySystemApp.API.Configuration
{
  public static class FluentValidationConfig
  {
    public static void Configure()
    {
      ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
    }
  }
}
