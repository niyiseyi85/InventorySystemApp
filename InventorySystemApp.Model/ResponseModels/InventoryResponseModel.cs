using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Model.ResponseModels
{
  public class InventoryResponseModel
  {
    public static class Messages
    {
      public const string InventoryCreatedSuccessful = "Inventory Created";
      public const string DeleteInventorySuccessful = "Inventory Deleted";
      public const string InventoryUpdatedSuccessful = "Inventory Updated";
      public const string InventoryByIdSuccessful = "Inventory successfull";
    }
    public static class ErrorMessages
    {
      public const string InventoryCreationFailed = "Failed To Create Inventory";
      public const string InventoryNotExist = "Inventory Not exist";
      public const string InventoryByIdError = "Inventory Error";
      public const string InventoryUpdateFailed = "Failed To Update Inventory";
      public const string PurchaseError = "Failed to Purchase Inventory";
      public const string InventoryNotFoundError = "Inventory Not Found";
    }
  }
}
