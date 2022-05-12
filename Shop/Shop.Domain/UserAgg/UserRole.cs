﻿using Common.Domain.Bases;

namespace Shop.Domain.UserAgg
{
    public class UserRole : BaseEntity
    {
        private UserRole()
        {

        }
        public UserRole(long roleId)
        {
            RoleId = roleId;
        }

        public long UserId { get; internal set; }
        public long RoleId { get; private set; }
    }
}
