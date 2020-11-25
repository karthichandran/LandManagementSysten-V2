﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandBankManagement.Data
{
	public class CashAccount
    {
        [Key]
        public int CashAccountId { get; set; }
        public Guid CashAccountGuid { get; set; }
        public int AccountTypeId { get; set; }
        public string CashAccountName { get; set; }
        public bool IsCashAccountActive { get; set; }

        [NotMapped]
        public string SearchTerms { get; set; }
        public string BuildSearchTerms() => $"{CashAccountName}".ToLower();

    }
}
