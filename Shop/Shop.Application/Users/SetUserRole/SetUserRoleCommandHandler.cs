using Common.Application;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.SetUserRole;

public class SetUserRoleCommandHandler : IBaseCommandHandler<SetUserRoleCommand>
{
    private readonly IUserRepository _userRepository;
    public SetUserRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId);
        if (user == null) return OperationResult.NotFound();


        await _userRepository.Save();
        return OperationResult.Success();
    }
}