﻿using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.AddTokens;

public class AddUserTokenCommandHandler : IBaseCommandHandler<AddUserTokenCommand>
{
    private readonly IUserRepository _repository;

    public AddUserTokenCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(AddUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        user.AddToken(request.HashedJwtToken, request.HashedRefreshToken, request.TokenExpireDate,
            request.RefreshTokenExpireDate, request.Device);
        await _repository.Save();
        return OperationResult.Success();
    }
}