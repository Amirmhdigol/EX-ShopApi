using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProductAgg;
using Shop.Infrastructure.Persistent.Ef;
using Shop.Query.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Products;

public static class ProductMapper
{
    public static ProductDTO? Map(this Product? product)
    {
        if (product == null)
            return null;

        return new ProductDTO
        {
            Id = product.Id,
            Description = product.Description,
            CreationDate = product.CreationDate,
            SeoData = product.SeoData,
            ImageName = product.ImageName,
            Slug = product.Slug,
            Title = product.Title,
            Specifications = product.Specifications.Select(s => new ProductSpecificationsDTO
            {
                Key = s.Key,
                Value = s.Value,
            }).ToList(),
            Images = product.Images.Select(s => new ProductImageDTO
            {
                Id = s.Id,
                ImageName = s.ImageName,
                CreationDate = s.CreationDate,
                ProductId = s.ProductId,
                Sequence = s.Sequence
            }).ToList(),

            Category = new ProductCategoryDTO() { Id = product.CategoryId },
            SubCategory = new ProductCategoryDTO() { Id = product.SubCategoryId },
            SecondrySubCategory = product.SecondrySubCategoryId != null ? new ProductCategoryDTO()
            {
                Id = (long)product.SecondrySubCategoryId
            } : null
        };
    }
    public static ProductFilterData MapListData(this Product product)
    {
        return new ProductFilterData
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            ImageName = product.ImageName,
            Slug = product.Slug,
            Title = product.Title,
        };
    }
    public static async Task SetCategories(this ProductDTO productDTO, ShopContext shopContext)
    {
        var categories = await shopContext.Categories
            .Where(r => r.Id == productDTO.Category.Id || r.Id == productDTO.SubCategory.Id)
            .Select(s => new ProductCategoryDTO()
            {
                Id = s.Id,
                Slug = s.Slug,
                ParentId = s.ParentId,
                SeoData = s.SeoData,
                Title = s.Title
            }).ToListAsync();

        if (productDTO.SecondrySubCategory != null)
        {
            var SecondarySubCategories = await shopContext.Categories
                .Where(c => c.Id == productDTO.SecondrySubCategory.Id)
                .Select(s => new ProductCategoryDTO()
                {
                    Id = s.Id,
                    ParentId = s.ParentId,
                    SeoData = s.SeoData,
                    Slug = s.Slug,
                    Title = s.Title,
                })
                .FirstOrDefaultAsync();

            if (SecondarySubCategories != null)
                productDTO.SecondrySubCategory = SecondarySubCategories;
        }
        productDTO.Category = categories.First(r => r.Id == productDTO.Category.Id);
        productDTO.SubCategory = categories.First(r => r.Id == productDTO.SubCategory.Id);
    }
}