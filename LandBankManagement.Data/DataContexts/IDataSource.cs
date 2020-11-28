﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LandBankManagement.Data.Services
{
    public interface IDataSource : IDisposable
    {
        DbSet<Company> Companies { get; }
        DbSet<Vendor> Vendors { get; }
        DbSet<Party> Parties { get; } 
        DbSet<Taluk> Taluks { get; }
        DbSet<Hobli> Hoblis { get; }
        DbSet<Village> Villages { get; }
        DbSet<ExpenseHead> ExpenseHeads { get; }
        DbSet<AccountType> AccountTypes { get; }
        DbSet<BankAccount> BankAccounts { get; }
        DbSet<CashAccount> CashAccounts { get; }
        DbSet<DocumentType> DocumentTypes { get; }
        DbSet<CheckList> CheckLists { get; }
        DbSet<Property> Properties { get; }
        DbSet<PropertyType> PropertyTypes { get; }
        DbSet<PropCheckListMaster> PropCheckListMasters { get; }
        DbSet<FundTransfer> FundTransfers { get; }

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
