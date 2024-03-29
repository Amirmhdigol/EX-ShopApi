﻿using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDTO>
{
    private readonly ShopContext _context;
    public GetUserByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }
    public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == request.UserId, cancellationToken);
        if (user == null) return null;
        return user.Map();
    }
}