using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Model.Models
{
  public class Inventory
  {
    public int InventoryId { get; set; }
    public string? InventoryName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public List<ProductInventory>? ProductInventories { get; set; }
  }
}
