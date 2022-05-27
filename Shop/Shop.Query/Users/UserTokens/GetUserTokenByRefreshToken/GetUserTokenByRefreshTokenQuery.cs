using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.UserTokens.GetUserTokenByRefreshToken;
public record GetUserTokenByRefreshTokenQuery(string HashedRefreshToken) : IQuery<UserTokenDTO?>;
