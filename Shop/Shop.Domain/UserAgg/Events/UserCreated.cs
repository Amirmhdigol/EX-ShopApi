using Common.Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg.Events;
public class UserCreated : BaseDomainEvent
{
    public UserCreated(long userId)
    {
        UserId = userId;
    }

    public long UserId { get; set; }

}