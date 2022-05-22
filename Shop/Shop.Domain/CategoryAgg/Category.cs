    using Common.Domain.Bases;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;
using Shop.Domain.CategoryAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.CategoryAgg
{
    public class Category : BaseAggregate
    {
        private Category()
        {
            Childs = new List<Category>();

        }
        public Category(string title, SeoData seoData, string slug, ICategoryDomainService domainService)
        {
            slug = slug?.ToSlug();
            Guard(title, slug, domainService);
            Title = title;
            SeoData = seoData;
            Slug = slug;
            Childs = new List<Category>();
        }

        public string Title { get; private set; }
        public SeoData SeoData { get; private set; }
        public string Slug { get; private set; }
        public long? ParentId { get; private set; }
        public List<Category> Childs { get; set; }

        public void Edit(string title, SeoData seoData, string slug, ICategoryDomainService domainService)
        {
            slug = slug?.ToSlug();
            Guard(title,slug,domainService);
            Title = title;
            SeoData = seoData;
            Slug = slug;
        }
        public void AddChild(string title, SeoData seoData, string slug, ICategoryDomainService domainService)
        {
            Childs.Add(new Category(title, seoData, slug, domainService)
            {
                ParentId = Id
            });
        }
        public void Guard(string title, string slug, ICategoryDomainService domainService)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

            if (Slug != slug)
                if (domainService.SlugExists(slug))
                    throw new SlugAlreadyExistsException(slug);
        }
    }
}
