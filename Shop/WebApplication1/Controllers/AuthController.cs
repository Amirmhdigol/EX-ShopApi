using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.JWT.Util;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.AddTokens;
using Shop.Application.Users.Register;
using Shop.Application.Users.RemoveToken;
using Shop.Presentation.Facade.Users;
using Shop.Query.Users.DTOs;
using UAParser;

namespace Shop.Api.Controllers;
public class AuthController : ApiController
{
    private readonly IUserFacade _userFacade;
    private readonly IConfiguration _configuration;
    public AuthController(IUserFacade facade, IConfiguration configuration)
    {
        _userFacade = facade;
        _configuration = configuration;
    }
    [HttpPost("Login")]
    public async Task<ApiResult<LoginResultDTO?>> Login(LoginViewModel viewModel)
    {
        var user = await _userFacade.GetUserByPhoneNumber(viewModel.PhoneNumber);
        if (user == null)
            return CommandResult(OperationResult<LoginResultDTO>.Error("user not found with this info"));

        if (!Sha256Hasher.IsCompare(user.Password, viewModel.Password))
            return CommandResult(OperationResult<LoginResultDTO>.Error("user not found with this info"));

        if (!user.IsActive)
            return CommandResult(OperationResult<LoginResultDTO>.Error("YOur account is not activated"));

        var loginResult = await AddTokenAndGenerateJwt(user);
        return CommandResult(loginResult);
    }

    [HttpPost("Register")]
    public async Task<ApiResult> Register(RegisterViewModel viewModel)
    {
        var result = await _userFacade.RegisterUser(new RegisterUserCommand(new PhoneNumber(viewModel.PhoneNumber), viewModel.Password));
        return CommandResult(result);
    }

    [HttpPost("RefreshToken")]
    public async Task<ApiResult<LoginResultDTO?>> RefreshToken(string refreshToken)
    {
        var result = await _userFacade.GetTokenByRefreshToken(refreshToken);
        if (result == null) return CommandResult(OperationResult<LoginResultDTO?>.NotFound("Invalid refresh token"));

        if (result.TokenExpireDate > DateTime.Now) return CommandResult(OperationResult<LoginResultDTO?>.Error("Token Has Not Expired Yet"));

        if (result.RefreshTokenExpireDate < DateTime.Now) return CommandResult(OperationResult<LoginResultDTO?>.Error("Refresh Token Has Been Expired"));

        await _userFacade.RemoveToken(new RemoveUserTokenCommand(result.Id, result.UserId));

        var user = await _userFacade.GetUserById(result.UserId);
        var tokenResult = await AddTokenAndGenerateJwt(user);

        return CommandResult(tokenResult);
    }

    private async Task<OperationResult<LoginResultDTO?>> AddTokenAndGenerateJwt(UserDTO user)
    {
        var uaParser = Parser.GetDefault();
        var info = uaParser.Parse(HttpContext.Request.Headers["user-agent"]);

        var device = $"{info.Device.Family}/{info.OS.Family} {info.OS.Major}.{info.OS.Minor} - {info.UA.Family}";

        var token = JWTTokenBuilder.BuildToken(user, _configuration);
        var refreshToken = Guid.NewGuid().ToString();

        var hashJwt = Sha256Hasher.Hash(token);
        var hashRefreshToken = Sha256Hasher.Hash(refreshToken);

        var tokenResult = await _userFacade.AddToken(new AddUserTokenCommand(user.Id, hashJwt, hashRefreshToken
                    , DateTime.Now.AddDays(7), DateTime.Now.AddDays(7), device));

        if (tokenResult.Status != OperationResultStatus.Success) return OperationResult<LoginResultDTO?>.Error();

        return OperationResult<LoginResultDTO?>.Success(new LoginResultDTO
        {
            RefreshToken = refreshToken,
            Token = token,
        });
    }
}