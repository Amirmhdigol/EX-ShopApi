using Common.Application;
using Common.Application.SecurityUtil;
using Common.CacheHelper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Users.AddTokens;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.EditCurrent;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Application.Users.RemoveUser;
using Shop.Application.Users.SetUserRole;
using Shop.Query.Roles.DTOs;
using Shop.Query.Users.DTOs;
using Shop.Query.Users.GetByFilter;
using Shop.Query.Users.GetById;
using Shop.Query.Users.GetByPhoneNumber;
using Shop.Query.Users.GetNameById;
using Shop.Query.Users.GetUserRoleId;
using Shop.Query.Users.GetUserRoles;
using Shop.Query.Users.UserTokens;
using Shop.Query.Users.UserTokens.GetTokenByJwtToken;
using Shop.Query.Users.UserTokens.GetUserTokenByRefreshToken;

namespace Shop.Presentation.Facade.Users;

internal class UserFacade : IUserFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public UserFacade(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    public async Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ChangeUserPassword(ChangePasswordCommand command)
    {
        var res = await _mediator.Send(command);
        if (res.Status == OperationResultStatus.Success)
            await _cache.RemoveAsync(CacheKeys.User(command.UserId));
        return res;
    }

    public async Task<OperationResult> CreateUser(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditCurrentUser(EditCurrentUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditUser(EditUserCommand command)
    {
        var res = await _mediator.Send(command);
        if (res.Status == OperationResultStatus.Success)
            await _cache.RemoveAsync(CacheKeys.User(command.UserId));
        return res;
    }

    public async Task<UserTokenDTO?> GetTokenByJwtToken(string jwtToken)
    {
        var HashedJwtToken = Sha256Hasher.Hash(jwtToken);
        return await _cache.GetOrSet(CacheKeys.UserToken(HashedJwtToken), () =>
        {
            return _mediator.Send(new GetTokenByJwtTokenQuery(HashedJwtToken));
        });
    }

    public async Task<UserTokenDTO?> GetTokenByRefreshToken(string refreshToken)
    {
        var HashedRefreshToken = Sha256Hasher.Hash(refreshToken);
        return await _mediator.Send(new GetUserTokenByRefreshTokenQuery(HashedRefreshToken));
    }

    public async Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams)
    {
        return await _mediator.Send(new GetUserByFilterQuery(filterParams));
    }

    public async Task<UserDTO?> GetUserById(long userId)
    {
        return await _cache.GetOrSet(CacheKeys.User(userId), () =>
        {
            return _mediator.Send(new GetUserByIdQuery(userId));
        });
    }

    public async Task<UserDTO?> GetUserByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
    }

    public async Task<string> GetUserNameById(long userId)
    {
        return await _mediator.Send(new GetUserNameByIdQuery(userId));
    }

    public async Task<LlongRoleId> GetUserRoleId(long userId)
    {
        return await _mediator.Send(new GetUserRoleIdQuery(userId));
    }

    public async Task<List<RoleDTO>> GetUsersRoleById(long userId)
    {
        return await _mediator.Send(new GetUserRolesByIdQuery(userId));
    }

    public async Task<OperationResult> RegisterUser(RegisterUserCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
    {
        var res = await _mediator.Send(command);

        if (res.Status == OperationResultStatus.Success)
            return OperationResult.Error();

        await _cache.RemoveAsync(CacheKeys.UserToken(res.Data));
        return OperationResult.Success();
    }

    public async Task<OperationResult> SetUserRole(SetUserRoleCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SoftDeleteUser(long userId)
    {
        return await _mediator.Send(new DeleteUserByIdCommand(userId));
    }
}