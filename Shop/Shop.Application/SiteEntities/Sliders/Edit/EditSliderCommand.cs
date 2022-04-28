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
        public EditSliderCommand(long id, IFormFile? imageFile, string title, string link)
        {
            Id = id;
            ImageFile = imageFile;
            Title = title;
            Link = link;
        }
        public long Id { get; private set; }
        public IFormFile? ImageFile { get; private set; }
        public string Title { get; private set; }
        public string Link { get; private set; }
    }

}