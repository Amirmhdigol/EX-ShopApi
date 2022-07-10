using Common.Application;
using Shop.Domain.UserAgg.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.Create
{
    public class CreateUserCommand : IBaseCommand
    {
        public CreateUserCommand(){}

        public string? Name { get; set; }
        public string? Family { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public long RoleId { get; set; }
    }
}
