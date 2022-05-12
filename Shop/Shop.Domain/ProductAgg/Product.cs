using Common.Domain.Bases;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg
{
    public class Product : BaseAggregate
    {
        private Product()
        {

        }
        public Product(string title, string imageName, string description, long categoryId, long subCategoryId
            , long? secondrySubCategoryId, string slug, SeoData seoData, IProductDomainService domainService)
        {
            Guard(title, slug, description, domainService);
            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));

            Title = title;
            ImageName = imageName;
            Description = description;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            SecondrySubCategoryId = secondrySubCategoryId;
            Slug = slug.ToSlug();
            SeoData = seoData;
        }

        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public string Description { get; private set; }
        public long CategoryId { get; private set; }
        public long SubCategoryId { get; private set; }
        public long? SecondrySubCategoryId { get; private set; }
        public string Slug { get; private set; }
        public SeoData SeoData { get; private set; }
        public List<ProductImage> Images { get; private set; }
        public List<ProductSpecification> Specifications { get; private set; }

        public void EditProduct(string title, string description, long categoryId, long subCategoryId
           , long secondrySubCategoryId, string slug, SeoData seoData, IProductDomainService domainService)
        {
            Guard(title,slug,description,domainService);
            Title = title;
            Description = description;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            SecondrySubCategoryId = secondrySubCategoryId;
            Slug = slug.ToSlug();
            SeoData = seoData;
        }
        public void SetProductImage(string imageName)
        {
            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
            ImageName = imageName;
        }
        public void AddImage(ProductImage image)
        {
            image.ProductId = Id;
            Images.Add(image);
        }
        public string RemoveImage(long imageId)
        {
            var Image = Images.FirstOrDefault(a => a.Id == imageId);
            if (Image == null)
                throw new NullOrEmptyDomainDataException("Image Not Found");
            Images.Remove(Image);
            return Image.ImageName;
        }
        public void SetSpecification(List<ProductSpecification> specifications)
        {
            specifications.ForEach(a => a.ProductId = Id);
            Specifications = specifications;
        } 
        private void Guard(string title, string slug, string description, IProductDomainService domainService)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            NullOrEmptyDomainDataException.CheckString(description, nameof(description));
            NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

            if (slug != Slug)
                if (domainService.SlugExists(slug.ToSlug()))
                    throw new SlugAlreadyExistsException();
        }
    }
}
