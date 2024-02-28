using InventorySystemApp.Data.IRepository;
using InventorySystemApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Data.Repository
{
  public class InventoryTransactionRepository : RepositoryGeneric<InventoryTransaction>, IInventoryTransactionRepository
  {
    private readonly DataContext _context;

    public InventoryTransactionRepository(DataContext context) : base(context)
    {
      _context = context;
    }
  }
}
