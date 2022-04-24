using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products.Create
{
    internal class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
    {
        private readonly IProductDomainService _domainService;
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public CreateProductCommandHandler(IProductDomainService domainService, IProductRepository repository, IFileService fileService)
        {
            _domainService = domainService;
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var ImageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);

            var product = new Product(request.Title, ImageName, request.Description, request.CategoryId, request.SubCategoryId
                , request.SecondrySubCategoryId, request.Slug, request.SeoData, _domainService);

            _repository.Add(product);

            var Specifications = new List<ProductSpecification>();
            request.Specifications.ToList().ForEach(specification =>
            {
                Specifications.Add(new ProductSpecification(specification.Key,specification.Value));
            });
            product.SetSpecification(Specifications);
            await _repository.Save();
            return OperationResult.Success();
        }
    }
}
