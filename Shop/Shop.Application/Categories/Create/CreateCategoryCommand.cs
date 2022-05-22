using Common.Application;
using Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Categories.Create
{
    public record CreateCategoryCommand(string Title, SeoData SeoData, string Slug) : IBaseCommand<long>;
}