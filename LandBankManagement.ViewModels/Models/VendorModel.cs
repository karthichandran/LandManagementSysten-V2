﻿using LandBankManagement.Services;
using System;
using System.Collections.ObjectModel;

namespace LandBankManagement.Models
{
   public class VendorModel : ObservableObject
    {
        static public VendorModel CreateEmpty() => new VendorModel { VendorId = -1, IsEmpty = true };
        public int VendorId { get; set; }
        public Guid VendorGuid { get; set; }
        public string VendorSalutation { get; set; }
        public string VendorLastName { get; set; }
        public string VendorName { get; set; }
        public string VendorAlias { get; set; }
        public string RelativeSalutation { get; set; }
        public string RelativeName { get; set; }
        public string RelativeLastName { get; set; }
        public string ContactPerson { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string PhoneNoIsdCode { get; set; }
        public string PhoneNo { get; set; }
        public string email { get; set; }
        public string PAN { get; set; }
        public string AadharNo { get; set; }
        public string GSTIN { get; set; }
        public bool IsVendorActive { get; set; }
        public string SalutationType { get; set; }
        public string GroupId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSCCode { get; set; }
        public string AccountNumber { get; set; }
        public string GroupName { get; set; }
        public ObservableCollection<ImagePickerResult> VendorDocuments { get; set; }
        public bool IsNew => VendorId <= 0;
        public override void Merge(ObservableObject source)
        {
            if (source is VendorModel model)
            {
                Merge(model);
            }
        }

        public void Merge(VendorModel source)
        {
            if (source != null)
            {
                VendorId = source.VendorId;
                VendorGuid = source.VendorGuid;
                VendorSalutation = source.VendorSalutation;
                VendorLastName = source.VendorLastName;
                VendorName = source.VendorName;
                VendorAlias = source.VendorAlias;
                RelativeSalutation = source.RelativeSalutation;
                RelativeName = source.RelativeName;
                RelativeLastName = source.RelativeLastName;
                AddressLine2 = source.AddressLine2;
                City = source.City;
                ContactPerson = source.ContactPerson;
                AddressLine1 = source.AddressLine1;
                PinCode = source.PinCode;
                PhoneNoIsdCode = source.PhoneNoIsdCode;
                PhoneNo = source.PhoneNo;
                email = source.email;
                PAN = source.PAN;
                AadharNo = source.AadharNo;
                GSTIN = source.GSTIN;
                IsVendorActive = source.IsVendorActive;
                VendorDocuments = source.VendorDocuments;
                SalutationType = source.SalutationType;
                GroupId = source.GroupId;
                GroupName = source.GroupName;
                BankName = source.BankName;
                BranchName = source.BranchName;
                IFSCCode = source.IFSCCode;
                AccountNumber = source.AccountNumber;
            }
        }
    }
}
