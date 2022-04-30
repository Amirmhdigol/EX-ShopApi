using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.Edit
{
    public class EditUserCommand : IBaseCommand
    {
        public EditUserCommand(long userId, IFormFile? avatar, string name, string family, string password, string email, Gender gender, string phoneNumber)
        {
            UserId = userId;
            Avatar = avatar;
            Name = name;
            Family = family;
            Password = password;
            Email = email;
            Gender = gender;
            PhoneNumber = phoneNumber;
        }
        public long UserId { get; private set; }
        public IFormFile Avatar { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public Gender Gender { get; private set; }
        public string PhoneNumber { get; private set; }
    }

}
