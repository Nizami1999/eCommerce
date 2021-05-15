using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCommerce.Models;

namespace eCommerce.ViewModels
{
    public class HomeIndexVM
    {
        public HomeCollection HomeCollection { get; set; }
        public List<HomeComingProduct> HomeComingProducts { get; set; }
        public IEnumerable<HomeSlider> HomeSlider { get; set; }
        public List<Smartphone> AllSmartphones { get; set; }
        public List<Smartphone> AllNewSmartphones { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Blog> Blog { get; set; }
        public About About { get; set; }
        public List<Team> Team { get; set; }

    }
}