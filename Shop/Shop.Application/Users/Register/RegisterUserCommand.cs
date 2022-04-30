using Common.Application;
using Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.Register
{
    public class RegisterUserCommand : IBaseCommand
    {
        public RegisterUserCommand(PhoneNumber phonenumber, string password)
        {
            Phonenumber = phonenumber;
            Password = password;
        }
        public PhoneNumber Phonenumber { get; set; }
        public string Password { get; set; }
    }
}
