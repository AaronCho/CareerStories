using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CareerStories.Controllers
{
    public class RobotsController : Controller
    {
        //
        // GET: /Robots/
        //used http://www.edandersen.com/2013/02/17/adding-a-dynamic-robots-txt-file-to-an-asp-net-mvc-site/ for dynamic, we only need static.
        public FileContentResult RobotsText()
        {
            var contentBuilder = new StringBuilder();
            contentBuilder.AppendLine("User-agent: *");
            contentBuilder.AppendLine("Disallow: ");
            contentBuilder.AppendLine("Sitemap: http://careeries.com/sitemap");
           
            return File(Encoding.UTF8.GetBytes(contentBuilder.ToString()), "text/plain");
        }
	}
}