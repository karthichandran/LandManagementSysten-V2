﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandBankManagement.Data;
using LandBankManagement.Models;

namespace LandBankManagement.Services
{
   public interface IHobliService
    {
        Task<HobliModel> AddHobliAsync(HobliModel model);
        Task<HobliModel> GetHobliAsync(long id);
        Task<IList<HobliModel>> GetHoblisAsync(DataRequest<Hobli> request);
        Task<IList<HobliModel>> GetHoblisAsync(int skip, int take, DataRequest<Hobli> request);
        Task<int> GetHoblisCountAsync(DataRequest<Hobli> request);
        Task<HobliModel> UpdateHobliAsync(HobliModel model);
        Task<Result> DeleteHobliAsync(HobliModel model);       
    }
}
