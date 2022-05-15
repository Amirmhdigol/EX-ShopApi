using Common.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.SiteEntities.DTOs
{
    public class SliderDTO : BaseDTO
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Link { get; set; }
    }
}
