using Common.Application;
using Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.AddAddress
{
    public class AddUserAddressCommand : IBaseCommand
    {
        public AddUserAddressCommand(long userId, string provice, string city, string name, string family
            , string postalAddress, string postalCode, string nationalCode, PhoneNumber phoneNumber)
        {
            UserId = userId;
            Province = provice;
            City = city;
            Name = name;
            Family = family;
            PostalAddress = postalAddress;
            PostalCode = postalCode;
            NationalCode = nationalCode;
            PhoneNumber = phoneNumber;
        }

        public long UserId { get; set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PostalAddress { get; private set; }
        public string PostalCode { get; private set; }
        public string NationalCode { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
    }
}
