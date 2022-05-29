﻿using Common.Application;
using Shop.Application.Users.AddTokens;
using Shop.Application.Users.ChangePassword;
using Shop.Application.Users.Create;
using Shop.Application.Users.Edit;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Query.Users.DTOs;

namespace Shop.Presentation.Facade.Users;
public interface IUserFacade
{
    Task<OperationResult> RegisterUser(RegisterUserCommand command);
    Task<OperationResult> EditUser(EditUserCommand command);
    Task<OperationResult> ChangeUserPassword(ChangePasswordCommand command);
    Task<OperationResult> CreateUser(CreateUserCommand command);
    Task<OperationResult> AddToken(AddUserTokenCommand command);
    Task<OperationResult> RemoveToken(RemoveUserTokenCommand command);

    Task<UserDTO?> GetUserByPhoneNumber(string phoneNumber);
    Task<UserDTO?> GetUserById(long userId);
    Task<UserFilterResult> GetUserByFilter(UserFilterParams filterParams);
    Task<UserTokenDTO?> GetTokenByRefreshToken(string refreshToken);
    Task<UserTokenDTO?> GetTokenByJwtToken(string jwtToken);
}
