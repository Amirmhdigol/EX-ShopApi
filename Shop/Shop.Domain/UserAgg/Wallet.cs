using Common.Domain.Bases;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;

namespace Shop.Domain.UserAgg
{
    public class Wallet : BaseEntity
    {
        private Wallet()
        {

        }
        public Wallet(int price, WalletType type, string description, long userId, bool isFinally)
        {
            if (price < 500)
                throw new InvalidDomainDataException();

            Price = price;
            Type = type;
            Description = description;
            UserId = userId;
            IsFinally = isFinally;
            if(isFinally)
                FinallyDate = DateTime.Now;
        }
        public int Price { get; private set; }
        public WalletType Type { get; private set; }
        public string Description { get; private set; }
        public long UserId { get; internal set; }
        public bool IsFinally { get; private set; }
        public DateTime? FinallyDate { get; private set; }
    
        public void Finally(string refCode)
        {
            IsFinally = true;
            FinallyDate = DateTime.Now;
            Description += $"Ref code is {refCode}";
        }
        public void Finally()
        {
            IsFinally = true;
            FinallyDate = DateTime.Now;
        }
    }
}
