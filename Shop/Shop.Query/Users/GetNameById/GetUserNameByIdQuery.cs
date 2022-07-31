using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Users.GetNameById;
public record GetUserNameByIdQuery(long UserId) : IQuery<string>;
