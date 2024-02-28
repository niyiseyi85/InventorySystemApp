using InventorySystemApp.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Data.Repository
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly DataContext _context;
    private IInventoryRepository _inventoryRepository;
    private IProductRepository _productRepository;
    private IInventoryTransactionRepository _inventoryTransactionRepository;
    private IProductTransactionRepository _productTransactionRepository;

    public UnitOfWork(DataContext context, IProductRepository productRepository, IInventoryRepository inventoryRepository,
      IInventoryTransactionRepository inventoryTransactionRepository, IProductTransactionRepository productTransactionRepository) 
    {
      _context = context;
      _inventoryRepository = inventoryRepository;
      _inventoryTransactionRepository = inventoryTransactionRepository;
      _productTransactionRepository = productTransactionRepository;
      _productRepository = productRepository;
    }

    public IInventoryRepository InventoryRepository => _inventoryRepository ??= new InventoryRepository(_context);

    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

    public IInventoryTransactionRepository InventoryTransactionRepository => _inventoryTransactionRepository ??= new InventoryTransactionRepository(_context);

    public IProductTransactionRepository productTransactionRepository => _productTransactionRepository ??= new ProductTransactionRepository(_context);
       
    public void Dispose()
    {
      _context.Dispose();
      GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
