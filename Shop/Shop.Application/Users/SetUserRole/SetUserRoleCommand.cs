using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.SetUserRole;
public record SetUserRoleCommand(long RoleId, long UserId) : IBaseCommand;
