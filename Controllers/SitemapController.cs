using CareerStories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace CareerStories.Controllers
{
    public class SitemapController : Controller
    {
        //
        // GET: /Sitemap/
        public ActionResult Index()
        {
            var db = new CareersDataContext();
            var careerslist = db.Careers.Where(u => u.IsActive == 1).OrderBy(u => u.CareerName).ToList();
            var storieslist = db.Stories.Where(u => u.IsActive == 1).OrderBy(u => u.CareerName).ToList();

            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            const string careersurl = "http://careeries.com/careers/{0}";
            var careeritems = careerslist;
            var storyitems = storieslist;
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    new XElement(ns + "url", 
                        new XElement(ns + "loc", "http://careeries.com"),
                        new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                        new XElement(ns + "changefreq", "always"),
                        new XElement(ns + "priority", "0.5")),
                    new XElement(ns + "url", 
                        new XElement(ns + "loc", "http://careeries.com/careers"),
                        new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                        new XElement(ns + "changefreq", "always"),
                        new XElement(ns + "priority", "0.5")),
                    new XElement(ns + "url", 
                        new XElement(ns + "loc", "http://careeries.com/account/register"),
                        new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                        new XElement(ns + "changefreq", "always"),
                        new XElement(ns + "priority", "0.5")),
                    new XElement(ns + "url", 
                        new XElement(ns + "loc", "http://careeries.com/account/login"),
                        new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                        new XElement(ns + "changefreq", "always"),
                        new XElement(ns + "priority", "0.5")),
                    from i in careeritems
                    select
                    new XElement(ns + "url",
                        new XElement(ns + "loc", string.Format(careersurl, i.CareerName.Replace(" ", "-").ToLower())),
                        new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                        new XElement(ns + "changefreq", "always"),
                        new XElement(ns + "priority", "0.5")),
                    from story in storyitems
                    select
                    new XElement(ns + "url",
                        new XElement(ns + "loc", string.Format(careersurl, story.CareerName.Replace(" ", "-").ToLower() + "/" + story.Id + "/" + story.Title.Replace(" ", "-").ToLower())),
                        new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", DateTime.Now)),
                        new XElement(ns + "changefreq", "always"),
                        new XElement(ns + "priority", "0.5"))
                )//end urlset            
            );//end XDocument

            return Content("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" + sitemap.ToString(), "text/xml");
        }
    }

}