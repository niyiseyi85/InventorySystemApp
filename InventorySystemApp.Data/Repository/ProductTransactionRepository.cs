using InventorySystemApp.Data.IRepository;
using InventorySystemApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Data.Repository
{
  public class ProductTransactionRepository : RepositoryGeneric<ProductTransaction>, IProductTransactionRepository
  {
    private readonly DataContext _context;

    public ProductTransactionRepository(DataContext context) : base(context) 
    {
      _context = context;
    }
  }
}
