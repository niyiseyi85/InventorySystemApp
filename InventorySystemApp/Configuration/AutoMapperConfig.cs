using InventorySystemApp.Model.Dtos;
using System.Reflection;

namespace InventorySystemApp.API.Configuration
{
  public class AutoMapperConfig
  {
    public static void Configure (IServiceCollection services, params Assembly[] assemblies)
    {
      services.AddAutoMapper(assemblies);
      services.AddAutoMapper(typeof(InventoryDtoRequestMappingConfig));
      services.AddAutoMapper(typeof(ProductDtoRequestMappingConfig));

    }
  }
}
