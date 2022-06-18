using Common.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.SiteEntities.Sliders.Edit
{
    public class EditSliderCommand : IBaseCommand
    {
        public long Id { get;  set; }
        public IFormFile? ImageFile { get;  set; }
        public string Title { get;  set; }
        public string Link { get;  set; }
    }

}