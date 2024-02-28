using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Model.Models
{
  public class InventoryTransaction
  {
    public int InventoryTransactionId { get; set; }
    
    public int InventoryId { get; set; }
    
    public int QualityBefore { get; set; }


    public InventoryTransactionType ActivtyType { get; set; }


    public int QuantityAfter { get; set; }
    public string? PONumber { get; set; }
    public string? ProductNumber { get; set; }
    public double UnitPrice { get; set; }
    
    public DateTime TransactionDate { get; set; }

    public string? DoneBy { get; set; }

    public Inventory? Inventory { get; set; }
  }
  public enum InventoryTransactionType
  {
    PurchaseInventory = 1,
    ProduceProduct = 2
  }
}
