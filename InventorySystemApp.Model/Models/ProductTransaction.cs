using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Model.Models
{
  public class ProductTransaction
  {
    public int ProductTransactionId { get; set; }
    
    public int ProductId { get; set; }
    
    public int QualityBefore { get; set; }


    public ProductTransactionType ActivtyType { get; set; }
    
    public int QuantityAfter { get; set; }

    public string? ProductNumber { get; set; }
    public string? SaleOrderNumber { get; set; }
    public double? UnitPrice { get; set; }
    
    public DateTime TransactionDate { get; set; }
    
    public string DoneBy { get; set; }

    public Product product { get; set; }
  }
  public enum ProductTransactionType
  {
    produceProduct = 1,
    sellProduct = 2
  }
}
