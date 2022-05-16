using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Users
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _repository;
        public UserDomainService(IUserRepository repository)
        {
            _repository = repository;
        }

        public bool EmailExists(string eMail)
        {
            return _repository.Exists(e => e.Email == eMail);
        }

        public bool PhoneNumberExists(string phoneNumber)
        {
            return _repository.Exists(pn=>pn.PhoneNumber == phoneNumber);
        }
    }
}
