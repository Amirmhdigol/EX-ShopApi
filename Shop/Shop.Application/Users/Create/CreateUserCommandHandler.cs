﻿using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.RoleAgg.Repository;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.Create
{
    internal class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IUserDomainService _domainService;
        public CreateUserCommandHandler(IUserRepository repository, IUserDomainService domainService)
        {
            _repository = repository;
            _domainService = domainService;
        }
        public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var hPassword = Sha256Hasher.Hash(request.Password);
            var user = new User(request.Name, request.Family, hPassword, request.Email
                , request.Gender, request.PhoneNumber, request.RoleId, _domainService);

            _repository.Add(user);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
