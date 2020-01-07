using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Simit_Saray
{
    public static class UrlExtenions
    {
        public static string cleanURL(this System.Web.Mvc.HtmlHelper htmlHelper, string title)
        {
            string cleanTitle = title.ToLower().Replace(" ", "-");
            //Removes invalid character like .,-_ etc
            cleanTitle = Regex.Replace(cleanTitle, @"[^a-zA-Z0-9\/_|+ -]", "");
            return cleanTitle;
        }
        
    }
}