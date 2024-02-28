using InventorySystemApp.Data.IRepository;
using InventorySystemApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Data.Repository
{
  public class InventoryRepository : RepositoryGeneric<Inventory>, IInventoryRepository
  {
    private readonly DataContext _db;

    public InventoryRepository(DataContext db) : base(db)
        {
      _db = db;
    }
    }
}
