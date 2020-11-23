﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace LandBankManagement.Data.Services
{
    partial class DataServiceBase
    {
        public async Task<Company> GetCompanyAsync(long id)
        {
            return await _dataSource.Companies.Where(r => r.CompanyID == id).FirstOrDefaultAsync();
        }

        public async Task<IList<Company>> GetCompaniesAsync(int skip, int take, DataRequest<Company> request)
        {
            IQueryable<Company> items = GetCompanies(request);
            var records = await items.Skip(skip).Take(take)
                .Select(source => new Company
                {
                    CompanyID = source.CompanyID,
                    CompanyGuid = source.CompanyGuid,
                    City = source.City,
                    GSTIN = source.GSTIN,
                    IsActive = source.IsActive,
                    Name = source.Name,
                    PAN = source.PAN,
                    PhoneNo = source.PhoneNo,
                    AddressLine1 = source.AddressLine1,
                    AddressLine2 = source.AddressLine2,
                    PhoneNoIsdCode = source.PhoneNoIsdCode,
                    Pincode = source.Pincode,
                    Email = source.Email,
                })
                .AsNoTracking()
                .ToListAsync();

            return records;
        }

        private IQueryable<Company> GetCompanies(DataRequest<Company> request)
        {
            IQueryable<Company> items = _dataSource.Companies;

            // Query
            if (!String.IsNullOrEmpty(request.Query))
            {
                items = items.Where(r => r.SearchTerms.Contains(request.Query.ToLower()));
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

        //todo understand the need for this method
        public async Task<IList<Company>> GetCompanyKeysAsync(int skip, int take, DataRequest<Company> request)
        {
            IQueryable<Company> items = GetCompanies(request);

            // Execute
            var records = await items.Skip(skip).Take(take)
                .Select(r => new Company
                {
                    CompanyID = r.CompanyID,
                })
                .AsNoTracking()
                .ToListAsync();

            return records;
        }

        public async Task<int> GetCompaniesCountAsync(DataRequest<Company> request)
        {
            IQueryable<Company> items = _dataSource.Companies;

            // Query
            if (!String.IsNullOrEmpty(request.Query))
            {
                items = items.Where(r => r.SearchTerms.Contains(request.Query.ToLower()));
            }

            // Where
            if (request.Where != null)
            {
                items = items.Where(request.Where);
            }

            return await items.CountAsync();
        }

        public async Task<int> UpdateCompanyAsync(Company company)
        {
            if (company.CompanyID > 0)
            {
                _dataSource.Entry(company).State = EntityState.Modified;
            }
            else
            {
                company.CompanyGuid = Guid.NewGuid();
                //Company.CreatedOn = DateTime.UtcNow;
                _dataSource.Entry(company).State = EntityState.Added;
            }
            // Company.LastModifiedOn = DateTime.UtcNow;
            company.SearchTerms = company.BuildSearchTerms();
            int res = await _dataSource.SaveChangesAsync();
            return res;
        }

        public async Task<int> DeleteCompanyAsync(params Company[] company)
        {
            _dataSource.Companies.RemoveRange(company);
            return await _dataSource.SaveChangesAsync();
        }
    }
}
