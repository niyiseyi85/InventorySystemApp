using CSharpFunctionalExtensions;
using InventorySystemApp.Common.Helper;
using InventorySystemApp.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Service.IServices
{
  public interface IInventoryService
  {
    Task<Result<ResponseModel>> UpdateInventoryAsync(int id, InventoryDto request);
    Task<Result<ResponseModel<InventoryDto>>> GetInventoryByIdAsync(int id);
    Task<Result<ResponseModel>> DeleteInventoryAsync(int id);
    Task<Result<ResponseModel>> AddInventoryAsync(InventoryDto request);
    Task<Result<ResponseModel<List<InventoryDto>>>> GetAllInventoryAsync();
  }
}
