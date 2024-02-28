﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Common.Helper
{
  public class ResponseModel
  {
    public bool IsSuccessful { get; set; }
    public string? Message { get; set; }
    public bool Data { get; set; }
  }
  public class ResponseModel<T> : ResponseModel
  {
    public T Data { get; set; }
  }
}
