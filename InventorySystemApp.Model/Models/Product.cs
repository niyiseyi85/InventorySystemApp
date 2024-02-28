using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Model.Models
{
  public class Product
  {
    public int ProductId { get; set; }
    
    public string? ProductName { get; set; }
    
    public int Quantity { get; set; }
    
    //[Product_PricesGreaterThanInventories]
    public double Price { get; set; }
    public bool IsActive { get; set; } = true;
    public List<ProductInventory>? ProductInventories { get; set; }
  }
}
