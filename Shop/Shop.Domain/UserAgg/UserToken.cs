using Common.Domain.Bases;
using Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.UserAgg;
public class UserToken : BaseEntity
{
    private UserToken()
    {

    }
    public UserToken(string hashedJwtToken, string hashedRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        HashedJwtToken = hashedJwtToken;
        HashedRefreshToken = hashedRefreshToken;
        TokenExpireDate = tokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
        Guard();
    }
    public long UserId { get; internal set; }
    public string HashedJwtToken { get; private set; }
    public string HashedRefreshToken { get; private set; }
    public DateTime TokenExpireDate { get; private set; }
    public DateTime RefreshTokenExpireDate { get; private set; }
    public string Device { get; private set; }

    public void Guard()
    {
        NullOrEmptyDomainDataException.CheckString(HashedJwtToken, nameof(HashedJwtToken));
        NullOrEmptyDomainDataException.CheckString(HashedRefreshToken, nameof(HashedRefreshToken));

        if (TokenExpireDate < DateTime.Now)
            throw new InvalidDomainDataException("Expire Date is Invalid");

        if (RefreshTokenExpireDate < TokenExpireDate)
            throw new InvalidDomainDataException("Refresh token Expire Date is Invalid");
    }
}