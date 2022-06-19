using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.SiteEntities.Banners.Create
{
    public class CreateBannerCommand : IBaseCommand
    {
        public string Link { get; set; }
        public IFormFile ImageFile { get; set; }
        public BannerPosition Position { get; set; }
    }
}
