using Common.Query;
using Shop.Query.Users.DTOs;
namespace Shop.Query.Users.UserTokens.GetTokenByJwtToken;
public record GetTokenByJwtTokenQuery(string HashJwtToken) : IQuery<UserTokenDTO?>;
