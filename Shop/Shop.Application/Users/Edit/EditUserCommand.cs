using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg;
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
        public long UserId { get; set; }
        public IFormFile? Avatar { get; set; }
        public string? Name { get; set; }
        public string? Family { get; set; }
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public long RoleId { get; set; }
    }
}
