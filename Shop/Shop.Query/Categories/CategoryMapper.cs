using Shop.Domain.CategoryAgg;
using Shop.Query.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Categories;

public static class CategoryMapper
{
    public static CategoryDto Map(this Category? category)
    {
        if (category == null)
            return null;

        return new CategoryDto()
        {
            Title = category.Title,
            CreationDate = category.CreationDate,
            SeoData = category.SeoData,
            Id = category.Id,
            Slug = category.Slug,
            Childs = category.Childs.MapChildren()
        };
    }
    public static List<CategoryDto> Map(this List<Category> categories)
    {
        var model = new List<CategoryDto>();
        
        categories.ForEach(category =>
        {
            model.Add(new CategoryDto()
            {
                Title = category.Title,
                CreationDate = category.CreationDate,
                SeoData = category.SeoData,
                Id = category.Id,
                Slug = category.Slug,
                Childs = category.Childs.MapChildren()
            });
        });
        return model;
    }
    public static List<CategoryChildDto> MapChildren(this List<Category> children)
    {
        var model = new List<CategoryChildDto>();
        children.ForEach(category =>
        {
            model.Add(new CategoryChildDto()
            {
                Title = category.Title,
                CreationDate = category.CreationDate,
                SeoData = category.SeoData,
                Id = category.Id,
                Slug = category.Slug,
                ParentId = (long)category.ParentId,
                Childs = category.Childs.MapSecondaryChildren()
            });
        });
        return model;
    }
    private static List<SecondaryCategoryChildDto> MapSecondaryChildren(this List<Category> children)
    {
        var model = new List<SecondaryCategoryChildDto>();
        children.ForEach(category =>
        {
            model.Add(new SecondaryCategoryChildDto()
            {
                Title = category.Title,
                CreationDate = category.CreationDate,
                SeoData = category.SeoData,
                Id = category.Id,
                Slug = category.Slug,
                ParentId = (long)category.ParentId,
            });
        });
        return model;
    }
}


