using Common.Application;
using Shop.Domain.UserAgg.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users.ChargeWallet
{
    public class ChargeUserWalletCommand : IBaseCommand
    {
        public ChargeUserWalletCommand(int price, WalletType type, string description, long userId, bool isFinally)
        {
            Price = price;
            Type = type;
            Description = description;
            UserId = userId;
            IsFinally = isFinally;
        }
        public int Price { get; private set; }
        public WalletType Type { get; private set; }
        public string Description { get; private set; }
        public long UserId { get; internal set; }
        public bool IsFinally { get; private set; }
    }
}
