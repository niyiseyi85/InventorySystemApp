using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Data.IRepository
{
  public interface IUnitOfWork : IDisposable
  {
    IInventoryRepository InventoryRepository { get; } 
    IProductRepository ProductRepository { get; }
    IInventoryTransactionRepository InventoryTransactionRepository { get; }
    IProductTransactionRepository productTransactionRepository { get; }
    Task SaveAsync();
  }
}
