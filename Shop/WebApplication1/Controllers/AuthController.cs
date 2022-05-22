using Common.Application;
using Common.Application.SecurityUtil;
using Common.AspNetCore;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.JWT.Util;
using Shop.Api.ViewModels.Auth;
using Shop.Application.Users.Register;
using Shop.Presentation.Facade.Users;

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
    public async Task<ApiResult<string>> Login(LoginViewModel viewModel)
    {
        var user = await _userFacade.GetUserByPhoneNumber(viewModel.PhoneNumber);
        if (user == null)
            return CommandResult(OperationResult<string>.Error("user not found with this info"));

        if (!Sha256Hasher.IsCompare(user.Password, viewModel.Password))
            return CommandResult(OperationResult<string>.Error("user not found with this info"));

        if (!user.IsActive)
            return CommandResult(OperationResult<string>.Error("YOur account is not activated"));

        var token = JWTTokenBuilder.BuildToken(user, _configuration);
        return new ApiResult<string>()
        {
            Data = token,
            IsSuccess = true,
            MetaData = new()
        };
    }

    [HttpPost("Register")]
    public async Task<ApiResult> Register(RegisterViewModel viewModel)
    {
        var result = await _userFacade.RegisterUser(new RegisterUserCommand(new PhoneNumber(viewModel.PhoneNumber), viewModel.Password));
        return CommandResult(result);
    }
}