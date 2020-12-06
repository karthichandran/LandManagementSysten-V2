﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace LandBankManagement.Data.Services
{
    partial class DataServiceBase
    {
        public async Task<int> AddUserInfoAsync(UserInfo model)
        {
            if (model == null)
                return 0;

            var entity = new UserInfo()
            {
                UserName = model.UserName,
                loginName = model.loginName,
                UserPassword = model.UserPassword,
                Code = model.Code,
                Email = model.Email,
                MobileNo = model.MobileNo,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                IsActive = model.IsActive,
                IsAgent = model.IsAgent,
                IsAdmin = model.IsAdmin,
                Created = model.Created,
                CreatedBy = model.CreatedBy,
                Updated = model.Updated,
                UpdatedBy = model.UpdatedBy,

            };
            _dataSource.Entry(entity).State = EntityState.Added;
            int res = await _dataSource.SaveChangesAsync();
            return res;
        }

        public async Task<UserInfo> GetUserInfoAsync(long id)
        {

            return await _dataSource.UserInfos
                .Where(x => x.UserInfoId == id)
                .FirstOrDefaultAsync();

        }

        public async Task<IList<UserInfo>> GetUserInfosAsync(DataRequest<UserInfo> request)
        {
            IQueryable<UserInfo> items = GetUsers(request);
            return await items.ToListAsync();
        }

        public async Task<IList<UserInfo>> GetUserInfosAsync(int skip, int take, DataRequest<UserInfo> request)
        {
            IQueryable<UserInfo> items = GetUsers(request);
            var records = await items.Skip(skip).Take(take)
                .Select(source => new UserInfo
                {
                    UserInfoId = source.UserInfoId,
                    UserName = source.UserName,
                    loginName = source.loginName,
                    UserPassword = source.UserPassword,
                    Code = source.Code,
                    Email = source.Email,
                    MobileNo = source.MobileNo,
                    FromDate = source.FromDate,
                    ToDate = source.ToDate,
                    IsActive = source.IsActive,
                    IsAgent = source.IsAgent,
                    IsAdmin = source.IsAdmin,
                    Created = source.Created,
                    CreatedBy = source.CreatedBy,
                    Updated = source.Updated,
                    UpdatedBy = source.UpdatedBy,
                })
                .AsNoTracking()
                .ToListAsync();

            return records;
        }

        private IQueryable<UserInfo> GetUsers(DataRequest<UserInfo> request)
        {
            IQueryable<UserInfo> items = _dataSource.UserInfos;

            // Query
            if (!String.IsNullOrEmpty(request.Query))
            {
                items = items.Where(r => r.BuildSearchTerms().Contains(request.Query.ToLower()));
            }

            // Where
            if (request.Where != null)
            {
                items = items.Where(request.Where);
            }

            // Order By
            if (request.OrderBy != null)
            {
                items = items.OrderBy(request.OrderBy);
            }
            if (request.OrderByDesc != null)
            {
                items = items.OrderByDescending(request.OrderByDesc);
            }

            return items;
        }


        public async Task<int> GetUserInfosCountAsync(DataRequest<UserInfo> request)
        {
            IQueryable<UserInfo> items = _dataSource.UserInfos;

            // Query
            if (!String.IsNullOrEmpty(request.Query))
            {
                items = items.Where(r => r.BuildSearchTerms().Contains(request.Query.ToLower()));
            }

            // Where
            if (request.Where != null)
            {
                items = items.Where(request.Where);
            }

            return await items.CountAsync();
        }

        public async Task<int> UpdateUserInfoAsync(UserInfo model)
        {
            _dataSource.Entry(model).State = EntityState.Modified;
            int res = await _dataSource.SaveChangesAsync();
            return res;
        }

        public async Task<int> DeleteUserInfoAsync(UserInfo model)
        {
            _dataSource.UserInfos.Remove(model);
            return await _dataSource.SaveChangesAsync();
        }
    }
}
