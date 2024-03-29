﻿using Common.Domain;
using Common.Domain.Bases;
using Common.Domain.Exceptions;
using Shop.Domain.SellerAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.SellerAgg
{
    public class Seller : BaseAggregate
    {
        private Seller()
        {

        }
        public Seller(long userId, string shopName, string nationalCode, ISellerDomainService domainService)
        {
            Guard(shopName, nationalCode);
            UserId = userId;
            ShopName = shopName;
            NationalCode = nationalCode;
            Inventories = new List<SellerInventory>();

            if (!domainService.CheckSellerInfo(this))
                throw new InvalidDomainDataException("Information Is not valid");
        }

        public long UserId { get; private set; }
        public string ShopName { get; private set; }
        public string NationalCode { get; private set; }
        public SellerStatus Status { get; private set; }
        public DateTime LastUpdate { get; internal set; }
        public List<SellerInventory> Inventories { get; private set; }

        public void ChangeStatus(SellerStatus status)
        {
            Status = status;
            LastUpdate = DateTime.Now;
        }
        public void Edit(string shopName, string nationalCode, ISellerDomainService domainService)
        {
            Guard(shopName, nationalCode);

            if (nationalCode != NationalCode)
                if (domainService.NationalCodeExists(nationalCode))
                    throw new InvalidDomainDataException("NCode Belongs to other person");

            ShopName = shopName;
            NationalCode = nationalCode;
        }
        public void AddInventory(SellerInventory inventory)
        {
            if (Inventories.Any(a => a.ProductId == inventory.ProductId))
                throw new InvalidDomainDataException("This product already exists");

            Inventories.Add(inventory);
        }
        public void EditInventory(long inventoryId, int count, int price, int? discountPercentage)
        {
            var currentInventory = Inventories.FirstOrDefault(a => a.Id == inventoryId);
            if (currentInventory == null)
                throw new NullOrEmptyDomainDataException();

            //TODO CheckInventory
            currentInventory.Edit(count, price, discountPercentage);
        }
        //public void DeleteInventory(long InventoryId)
        //{
        //    var currentInventory = Inventories.FirstOrDefault(a => a.Id == InventoryId);
        //    if (currentInventory == null)
        //        throw new NullOrEmptyDomainDataException("not founded");
        //    Inventories.Remove(currentInventory);
        //}
        public void Guard(string shopName, string nationalCode)
        {
            NullOrEmptyDomainDataException.CheckString(shopName, nameof(shopName));
            NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));

            if (!nationalCode.NCodeIsValid())
                throw new InvalidDomainDataException("National code is invalid");
        }
    }
}
