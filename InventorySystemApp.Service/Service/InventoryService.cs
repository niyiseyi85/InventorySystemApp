using AutoMapper;
using CSharpFunctionalExtensions;
using InventorySystemApp.Common.Helper;
using InventorySystemApp.Data;
using InventorySystemApp.Data.IRepository;
using InventorySystemApp.Data.Repository;
using InventorySystemApp.Model.Dtos;
using InventorySystemApp.Model.Models;
using InventorySystemApp.Model.ResponseModels;
using InventorySystemApp.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Service.Service
{
  public class InventoryService : IInventoryService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InventoryService(IUnitOfWork unitOfWork, IMapper mapper, DataContext context)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }
    public async Task<Result<ResponseModel>> AddInventoryAsync(InventoryDto request)
    {
      var response = new ResponseModel();
      var inventoryExist = await _unitOfWork.InventoryRepository.FirstOrDefault(x => x.InventoryName ==  request.InventoryName);
        //await _context.Inventorys.FirstOrDefaultAsync(x => x. == request.InventoryEmail);
      if (inventoryExist == null)
      {
        var inventory = _mapper.Map<Inventory>(request);
        await _unitOfWork.InventoryRepository.Add(inventory);
        try
        {
          await _unitOfWork.SaveAsync();
          response.IsSuccessful = true;
          response.Message = InventoryResponseModel.Messages.InventoryCreatedSuccessful;
        }
        catch (Exception ex)
        {
          return Result.Failure<ResponseModel>($"{InventoryResponseModel.ErrorMessages.InventoryCreationFailed} - {ex.Message} : {ex.InnerException}");
        }
      }
      else
      {
        return Result.Failure<ResponseModel>($"{InventoryResponseModel.ErrorMessages.InventoryCreationFailed}");
      }
      return response;
    }

    public async Task<Result<ResponseModel>> DeleteInventoryAsync(int id)
    {
      var response = new ResponseModel();
      var Inventory = await _unitOfWork.InventoryRepository.FirstOrDefault(Id => Id.InventoryId == id);
      if (Inventory == null)
      {
        response.Message = InventoryResponseModel.ErrorMessages.InventoryNotExist;
      }
      try
      {
        //Inventory.IsDeleted = true;
        //await UpdateInventoryAsync(id, updateRequest);
        await _unitOfWork.SaveAsync();
        response.IsSuccessful = true;
        response.Message = $"Inventory with ID:{id} " + InventoryResponseModel.Messages.DeleteInventorySuccessful;
      }
      catch (Exception ex)
      {
        return Result.Failure<ResponseModel>($"{InventoryResponseModel.ErrorMessages.InventoryByIdError} - {ex.Message} : {ex.InnerException}");
      }
      return response;
    }

    public async Task<Result<ResponseModel<List<InventoryDto>>>> GetAllInventoryAsync()
    {
      var response = new ResponseModel<List<InventoryDto>>();
      try
      {
        var Inventory = await _unitOfWork.InventoryRepository.GetAll();
        var result = _mapper.Map<List<InventoryDto>>(Inventory);
        response.IsSuccessful = true;
        response.Data = result;
      }
      catch (Exception ex)
      {
        return Result.Failure<ResponseModel<List<InventoryDto>>>($"{InventoryResponseModel.ErrorMessages.InventoryByIdError} - {ex.Message} : {ex.InnerException}");
      }
      return response;
    }

    public async Task<Result<ResponseModel<InventoryDto>>> GetInventoryByIdAsync(int id)
    {
      var response = new ResponseModel<InventoryDto>();
      var Inventory = await _unitOfWork.InventoryRepository.FirstOrDefault(query => query.InventoryId == id);

      if (Inventory == null)
      {
        response.IsSuccessful = false;
        response.Message = InventoryResponseModel.ErrorMessages.InventoryNotExist;
      }
      else
      {
        try
        {
          var result = _mapper.Map<InventoryDto>(Inventory);
          response.IsSuccessful = true;
          response.Message = InventoryResponseModel.Messages.InventoryByIdSuccessful;
          response.Data = result;
        }
        catch (Exception ex)
        {
          return Result.Failure<ResponseModel<InventoryDto>>($"{InventoryResponseModel.ErrorMessages.InventoryByIdError} - {ex.Message} : {ex.InnerException}");
        }
      }
      return response;
    }

    public async Task<Result<ResponseModel>> UpdateInventoryAsync(int id, InventoryDto request)
    {
      var response = new ResponseModel();
      try
      {
        var Inventory = await _unitOfWork.InventoryRepository.FirstOrDefault(query => query.InventoryId == id);
        if (Inventory != null)
        {

          _mapper.Map(request, Inventory);
          _unitOfWork.InventoryRepository.Update(Inventory);
          await _unitOfWork.SaveAsync();
          response.IsSuccessful = true;
          response.Message = InventoryResponseModel.Messages.InventoryUpdatedSuccessful;
        }
      }
      catch (Exception ex)
      {
        return Result.Failure<ResponseModel>($"{InventoryResponseModel.ErrorMessages.InventoryUpdateFailed} - {ex.Message} : {ex.InnerException}");
      }
      return response;
    }
  }
}
