using InventorySystemApp.Data.IRepository;
using InventorySystemApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Data.Repository
{
  public class ProductRepository : RepositoryGeneric<Product>, IProductRepository
  {
    private readonly DataContext _context;

    public ProductRepository(DataContext context) : base(context)
    {
      _context = context;
    }
  }
}
