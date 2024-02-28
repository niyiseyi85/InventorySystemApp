using InventorySystemApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Model.Dtos
{
  public class GetInventoryTransactionDto
  {
    public string InventoryName { get; set; }
     public  DateTime? dateFrom { get; set; }
    public DateTime? dateTo { get; set; }
    public InventoryTransactionType? type { get; set; }
  }
}
