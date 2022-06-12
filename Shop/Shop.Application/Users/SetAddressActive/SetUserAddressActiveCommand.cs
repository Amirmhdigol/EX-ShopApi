using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.SetAddressActive;
public record SetUserAddressActiveCommand(long UserId, long AddressId) : IBaseCommand;

public class SetUserAddressActiveCommandHandler : IBaseCommandHandler<SetUserAddressActiveCommand>
{
    private readonly IUserRepository _userRepository;
    public SetUserAddressActiveCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(SetUserAddressActiveCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null) return OperationResult.Error();

        user.SetAddressActive(request.AddressId);
        await _userRepository.Save();
        return OperationResult.Success();
    }
}