using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Model.ResponseModels
{
  public class ProductResponseModels
  {
    public static class Messages
    {
      public const string ProductCreatedSuccessful = "Product created";
      public const string DeleteProductSuccessful = "Product deleted";
      public const string ProductUpdatedSuccessful = "Product Updated";
      public const string ProductByIdSuccessful = "Product successful";
    }
    public static class ErrorMessages
    {
      public const string ProductCreationFailed = "Product Creation Failed";
      public const string ProductNotExist = "Product Not Exist";
      public const string ProductByIdError = "ProductById Error";
      public const string ProductUpdateFailed = "Update Product Failed";
      public const string ProduceError = "Failed to produce product";
      public const string SellProductError = "Failed to sell product";
      public const string ProductNotFoundError = "product not found";
    }
  }
}
