using Common.Domain;
using Common.Domain.Bases;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Events;
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
        private User()
        {

        }
        public User(string name, string family, string password, string email, Gender gender, string phoneNumber, long roleId
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
            IsActive = true;
            IsDelete = false;
            RoleId = roleId;
            Addresses = new();
            Wallets = new();
            UserTokens = new();
            AddDomainEvent(new UserCreated(Id));
        }

        public string Name { get; private set; }
        public string Family { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string UserAvatar { get; private set; }
        public Gender Gender { get; private set; }
        public string PhoneNumber { get; private set; }
        public long RoleId { get; private set; }

        public List<UserAddress> Addresses { get; }
        public List<Wallet> Wallets { get; }
        public List<UserToken> UserTokens { get; }

        public void Edit(string name, string family, string email, Gender gender, string phoneNumber
            , long roleId, IUserDomainService domainServices)
        {
            Guard(phoneNumber, email, domainServices);
            Name = name;
            Family = family;
            Email = email;
            Gender = gender;
            PhoneNumber = phoneNumber;
            RoleId = roleId;
        }
        public void EditCurrent(string name, string family, string email, Gender gender, string phoneNumber
            , IUserDomainService domainServices)
        {
            Guard(phoneNumber, email, domainServices);
            Name = name;
            Family = family;
            Email = email;
            Gender = gender;
            PhoneNumber = phoneNumber;
        }
        public void ChangePassword(string newPassword)
        {
            NullOrEmptyDomainDataException.CheckString(newPassword, nameof(newPassword));

            Password = newPassword;
        }
        public static User RegisterUser(string phoneNumber, string password, IUserDomainService domainServices)
        {
            return new User("", "", password, null, Gender.None, phoneNumber, 3, domainServices);
        }
        public void AddAddress(UserAddress address)
        {
            address.UserId = Id;
            Addresses.Add(address);
        }
        public void SoftDelete(long userId)
        {
            if (userId == Id) IsDelete = true;
        }
        public void DeleteAddress(long addressId)
        {
            var ExistingAdderss = Addresses.FirstOrDefault(a => a.Id == addressId);
            if (ExistingAdderss == null)
                throw new NullOrEmptyDomainDataException("Address Not Found");

            Addresses.Remove(ExistingAdderss);
        }
        public void EditAddress(UserAddress address, long addressId)
        {
            var OldAddress = Addresses.FirstOrDefault(A => A.Id == addressId);
            if (OldAddress == null)
                throw new NullOrEmptyDomainDataException("Address Not Found");

            OldAddress.Edit(address.Province, address.City, address.Name, address.Family, address.PostalAddress
                , address.PostalCode, address.NationalCode, address.PhoneNumber);
        }
        public void SetAddressActive(long addressId)
        {
            var address = Addresses.FirstOrDefault(A => A.Id == addressId);
            if (address == null)
                throw new NullOrEmptyDomainDataException("Address Not Found");

            foreach (var userAddress in Addresses)
            {
                userAddress.SetDeActive();
            }
            address.SetActive();
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
        public void AddToken(string hashedJwtToken, string hashedRefreshToken, DateTime tokenExpireDate
            , DateTime refreshTokenExpireDate, string device)
        {
            var activeTokensCount = UserTokens.Count(c => c.RefreshTokenExpireDate > DateTime.Now);
            if (activeTokensCount == 3)
                throw new InvalidDomainDataException("You cannot use 4 devices at time");

            var token = new UserToken(hashedJwtToken, hashedRefreshToken, tokenExpireDate, refreshTokenExpireDate, device)
            {
                UserId = Id
            };
            UserTokens.Add(token);
        }
        public string RemoveToken(long tokenId)
        {
            var token = UserTokens.FirstOrDefault(a => a.Id == tokenId);
            if (token == null) throw new InvalidDomainDataException("Invalid Token id");
            UserTokens.Remove(token);
            return token.HashedJwtToken;
        }
        public void Guard(string phoneNumber, string eMail, IUserDomainService domainServices)
        {
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));

            if (phoneNumber.Length != 11)
                throw new InvalidDomainDataException("Phone number is invalid");

            if (!string.IsNullOrWhiteSpace(eMail))
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