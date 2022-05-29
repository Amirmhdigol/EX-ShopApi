using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.ChangePassword;

internal class ChangePasswordCommandHandler : IBaseCommandHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _repository;
    public ChangePasswordCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null) return OperationResult.NotFound();

        if (user.Password != Sha256Hasher.Hash(request.CurrentPassword))
            return OperationResult.Error("Current Password is invalid");

        user.ChangePassword(Sha256Hasher.Hash(request.Password));
        
        await _repository.Save();
        return OperationResult.Success();
    }
}