﻿using Common.Domain;
using Common.Domain.Bases;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg
{
    public class UserAddress : BaseEntity
    {
        private UserAddress()
        {

        }
        public UserAddress(string provice, string city, string name, string family, string postalAddress
            , string postalCode, string nationalCode, PhoneNumber phoneNumber)
        {
            Guard(provice, city, name, family, postalAddress, postalCode, nationalCode, phoneNumber);
            Provice = provice;
            City = city;
            Name = name;
            Family = family;
            PostalAddress = postalAddress;
            PostalCode = postalCode;
            NationalCode = nationalCode;
            PhoneNumber = phoneNumber;
            ActiveAddress = false;
        }

        public long UserId { get; internal set; }
        public string Provice { get; private set; }
        public string City { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PostalAddress { get; private set; }
        public string PostalCode { get; private set; }
        public string NationalCode { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public bool ActiveAddress { get; private set; }

        public void Edit(string provice, string city, string name, string family, string postalAddress, string postalCode
            , string nationalCode, PhoneNumber phoneNumber)
        {
            Guard(provice, city, name, family, postalAddress, postalCode, nationalCode, phoneNumber);
            Provice = provice;
            City = city;
            Name = name;
            Family = family;
            PostalAddress = postalAddress;
            PostalCode = postalCode;
            NationalCode = nationalCode;
            PhoneNumber = phoneNumber;
        }
        public void SetActive()
        {
            ActiveAddress = true;
        }
        public void Guard(string provice, string city, string Name, string family, string postalAddress, string postalCode
            , string nationalCode, PhoneNumber phoneNumber)
        {
            if (phoneNumber == null)
                throw new NullOrEmptyDomainDataException("");
            NullOrEmptyDomainDataException.CheckString(provice, nameof(provice));
            NullOrEmptyDomainDataException.CheckString(city, nameof(city));
            NullOrEmptyDomainDataException.CheckString(Name, nameof(Name));
            NullOrEmptyDomainDataException.CheckString(family, nameof(family));
            NullOrEmptyDomainDataException.CheckString(postalAddress, nameof(postalAddress));
            NullOrEmptyDomainDataException.CheckString(postalCode, nameof(postalCode));
            NullOrEmptyDomainDataException.CheckString(nationalCode, nameof(nationalCode));

            if (!nationalCode.NCodeIsValid())
                throw new InvalidDomainDataException("Your national code is invalid");
        }
    }
}
