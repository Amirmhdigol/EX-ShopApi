using Common.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.SiteEntities.Sliders.Create
{
    public class CreateSliderCommand : IBaseCommand
    {
        public CreateSliderCommand()
        {

        }
        public CreateSliderCommand(IFormFile imageFile, string title, string link)
        {
            ImageFile = imageFile;
            Title = title;
            Link = link;
        }
        public IFormFile ImageFile { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
