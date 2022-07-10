using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByPhoneNumber;

internal class GetUserByPhoneNumberQueryHandler : IQueryHandler<GetUserByPhoneNumberQuery, UserDTO?>
{
    private readonly ShopContext _context;
    public GetUserByPhoneNumberQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<UserDTO?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.PhoneNumber == request.PhoneNumber, cancellationToken);
        if (user == null) return null;
        return user.Map();
    }
}