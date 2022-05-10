using Common.Domain.ValueObjects;
using Common.Query;
using Shop.Domain.CategoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Categories.DTOs;

public class CategoryDto : BaseDTO
{
    public string Title { get;  set; }
    public SeoData SeoData { get;  set; }
    public string Slug { get;  set; }
    public List<CategoryChildDto> Childs { get; set; }
}

