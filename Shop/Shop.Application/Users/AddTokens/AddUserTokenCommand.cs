using Common.Application;

namespace Shop.Application.Users.AddTokens;
public class AddUserTokenCommand : IBaseCommand
{
    public AddUserTokenCommand(long userId, string hashedJwtToken, string hashedRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        UserId = userId;
        HashedJwtToken = hashedJwtToken;
        HashedRefreshToken = hashedRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
    }
    public long UserId { get; private set; }
    public string HashedJwtToken { get; private set; }
    public string HashedRefreshToken { get; private set; }
    public DateTime TokenExpireDate { get; private set; }
    public DateTime RefreshTokenExpireDate { get; private set; }
    public string Device { get; private set; }
}
