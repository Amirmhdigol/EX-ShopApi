using Common.Application;
using Shop.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Roles.Edit;

public record EditRoleCommand(long Id ,string title, List<Permission> Permissions) : IBaseCommand;
