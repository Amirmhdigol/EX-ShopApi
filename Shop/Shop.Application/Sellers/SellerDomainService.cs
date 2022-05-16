using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Application.Sellers
{
    public class SellerDomainService : ISellerDomainService
    {
        private readonly ISellerRepository _repository;
        public SellerDomainService(ISellerRepository repository)
        {
            _repository = repository;
        }

        public bool CheckSellerInfo(Seller seller)
        {
            var Exists = _repository.Exists(n => n.NationalCode == seller.NationalCode || n.UserId == seller.UserId);
            return !Exists;
        }

        public bool NationalCodeExists(string code)
        {
            return _repository.Exists(nc => nc.NationalCode == code);
        }
    }
}
