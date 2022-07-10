using MediatR;
using Shop.Domain.UserAgg.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.EventHandlers.Users;
public class UserCreatedSetRoleEventHandler : INotificationHandler<UserCreated>
{
    public Task Handle(UserCreated notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}