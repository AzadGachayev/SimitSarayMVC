using Simit_Saray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simit_Saray.ViewModels
{
    public class DefaultViewModel  
    {  
        public HomeHeader EsasSehife { get; set; } 
        public HomeNavbar NavBackground { get; set; }
        public List<Menyu> Menyum { get; set; }
        public List<MenuCategory> KateqoriyaMenyu { get; set; }
        public List<BlogItem> BloqSehife { get; set; }
        public BlogItem blogDetail { get; set; }
        public Contact Kontakt { get; set; }
        public About Haqqinda { get; set; }
        public List<Gallery> Qalereya {get; set;}
        public List<Gallery2> Qalereya2 { get; set;}
        public HomeHeader homeHeader { get; set; }
        public Reservation reserv { get; set; }
    }
}