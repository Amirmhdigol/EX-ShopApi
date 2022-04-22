using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders.CheckOut
{
    public class ChechOutOrderCommand : IBaseCommand
    {
        public ChechOutOrderCommand(long userId, string provice, string city, string name, string family, string postalAddress, string postalCode, string nationalCode, string phoneNumber)
        {
            UserId = userId;
            Provice = provice;
            City = city;
            Name = name;
            Family = family;
            PostalAddress = postalAddress;
            PostalCode = postalCode;
            NationalCode = nationalCode;
            PhoneNumber = phoneNumber;
        }

        public long UserId { get; private set; }
        public string Provice { get; private set; }
        public string City { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PostalAddress { get; private set; }
        public string PostalCode { get; private set; }
        public string NationalCode { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
