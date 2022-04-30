using Common.Domain;
using Common.Domain.Bases;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg
{
    public class User : BaseAggregate
    {
        public User(string name, string family, string password, string email, Gender gender, string phoneNumber
            , IUserDomainService domainServices)
        {
            Guard(phoneNumber, email, domainServices);
            Name = name;
            Family = family;
            Password = password;
            Email = email;
            UserAvatar = "avatar.png";
            Gender = gender;
            PhoneNumber = phoneNumber;
        }

        public string Name { get; private set; }
        public string Family { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string UserAvatar { get; private set; }
        public Gender Gender { get; private set; }
        public string PhoneNumber { get; private set; }

        public List<UserAddress> Addresses { get; private set; }
        public List<Wallet> Wallets { get; private set; }
        public List<UserRole> UserRoles { get; private set; }

        public void Edit(string name, string family, string email, Gender gender, string phoneNumber
            , IUserDomainService domainServices)
        {
            Guard(phoneNumber, email, domainServices);
            Name = name;
            Family = family;
            Email = email;
            Gender = gender;
            PhoneNumber = phoneNumber;
        }
        public static User RegisterUser(string phoneNumber, string password, IUserDomainService domainServices)
        {
            return new User("", "", password,null, Gender.None, phoneNumber, domainServices);
        }
        public void AddAddress(UserAddress address)
        {
            address.UserId = Id;
            Addresses.Add(address);
        }
        public void DeleteAddress(long addressId)
        {
            var ExistingAdderss = Addresses.FirstOrDefault(a => a.Id == addressId);
            if (ExistingAdderss == null)
                throw new NullOrEmptyDomainDataException("Address Not Found");

            Addresses.Remove(ExistingAdderss);
        }
        public void EditAddress(UserAddress address,long addressId)
        {
            var OldAddress = Addresses.FirstOrDefault(A => A.Id == addressId);
            if (OldAddress == null)
                throw new NullOrEmptyDomainDataException("Address Not Found");

            OldAddress.Edit(address.Provice, address.City, address.Name, address.Family, address.PostalAddress
                , address.PostalCode, address.NationalCode, address.PhoneNumber);
        }
        public void SetImages(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                imageName = "avatar.png";
            UserAvatar = imageName;
        }
        public void ChargeWallet(Wallet wallet)
        {
            wallet.UserId = Id;
            Wallets.Add(wallet);
        }
        public void SetRoles(List<UserRole> userRoles)
        {
            userRoles.ForEach(f => f.UserId = Id);
            UserRoles.Clear();
            UserRoles.AddRange(userRoles);
        }
        public void Guard(string phoneNumber, string eMail, IUserDomainService domainServices)
        {
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
            NullOrEmptyDomainDataException.CheckString(eMail, nameof(eMail));

            if (phoneNumber.Length != 11)
                throw new InvalidDomainDataException("Phone number is invalid");

            if (!eMail.IsValidEmail())
                throw new InvalidDomainDataException("Email is invalid");

            if (phoneNumber != PhoneNumber)
                if (domainServices.PhoneNumberExists(phoneNumber))
                    throw new InvalidDomainDataException("Phone number already exists");

            if (eMail != Email)
                if (domainServices.EmailExists(eMail))
                    throw new InvalidDomainDataException("Email already exists");
        }
    }
}
