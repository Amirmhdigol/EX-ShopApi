using Common.Query;

namespace Shop.Query.Users.DTOs;
public class UserTokenDTO : BaseDTO
{
    public long UserId { get; set; }
    public string HashedJwtToken { get; set; }
    public string HashedRefreshToken { get; set; }
    public DateTime TokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
    public string Device { get; set; }
}