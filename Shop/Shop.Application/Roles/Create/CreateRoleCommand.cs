using Common.Application;
using Shop.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Roles.Create
{
    public record CreateRoleCommand(string title, List<Permission> Permissions) : IBaseCommand;
}
