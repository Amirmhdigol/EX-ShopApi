using Common.Application;
using Shop.Domain.UserAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.RemoveUser;
public record DeleteUserByIdCommand(long UserId) : IBaseCommand;

public class DeleteUserByIdCommandHandler : IBaseCommandHandler<DeleteUserByIdCommand>
{
    private readonly IUserRepository _userRepository;
    public DeleteUserByIdCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null) return OperationResult.NotFound();

        user.SoftDelete(user.Id);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}