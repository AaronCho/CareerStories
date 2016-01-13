using CareerStories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CareerStories.Controllers
{
    public class CareersController : Controller
    {
        //
        // GET: /Careers/
        [HttpGet]
        public ActionResult Index()
        {
            //sanitize url
            string inputUrlParam; 
            string cleanUrlParam; 

            if (RouteData.Values["careerName"] == null)
            {
                return Redirect("/careers/administrative-assistant");
            }
            else
            {
                inputUrlParam = RouteData.Values["careerName"].ToString();
                cleanUrlParam = URLFriendly(inputUrlParam);

                if (!inputUrlParam.Equals(cleanUrlParam))
                {
                    return Redirect(cleanUrlParam);
                }
            }

            var db = new CareersDataContext();
            //var careersArray = db.Careers.ToArray(); //The ToArray executes the SQL call.
            /*var careersArray = db.Careers.Where(u => u.IsActive == 1).Select(u => u.CareerName);
            var careersIdArray = db.Careers.Where(u => u.IsActive == 1).Select(u => u.Id);
            SelectList careers = new SelectList(careersArray); //change the array to a select list*/

            var careersViewModel = new CareersViewModel();
            bool careerNameUrlFound = false;

            string careerNameUrl = "";
            if (RouteData.Values["careerName"] != null)  //This is from RouteConfig, so make sure it is up to date!
            {
                careerNameUrl = RouteData.Values["careerName"].ToString();
                careerNameUrl = careerNameUrl.Replace("-", " ");
            }
           
            //var list = db.Careers.ToList();
            var list = db.Careers.Where(u => u.IsActive == 1).ToList();
            List<SelectListItem> careersList = new List<SelectListItem>();

            for (int i = 0; i < list.Count(); i++)
            {
                careersList.Add(new SelectListItem
                {
                    Text = list.ElementAt(i).CareerName,
                    Value = list.ElementAt(i).CareerName // change to this if you want the value to be the Id: list.ElementAt(i).Id.ToString()
                });

                if (list.ElementAt(i).CareerName.ToLower().Equals(careerNameUrl))
                {
                    careerNameUrlFound = true;
                    careersList.ElementAt(i).Selected = true;

                    //set ViewModel parameters
                    careersViewModel.CareerName = list.ElementAt(i).CareerName;
                    careersViewModel.Description = list.ElementAt(i).Description;
                    careersViewModel.ImageUrl = list.ElementAt(i).ImageUrl;
                }
            }

            ViewBag.careersList = careersList;

            if (!careerNameUrlFound)
            {
                return Redirect("/careers/administrative-assistant"); //anything after careers/ that is not valid will be redirected to aa.
            }

            return View(careersViewModel);
        }

        [HttpPost]
        [ActionName("Index")] //if the signature changes, you can comment this line out
        public ActionResult IndexPost()
        {
            string selected = Request.Form["careersList"]; //grabs the value of the selected item that was posted.
            return Redirect(@"~\" + "careers/" + selected);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StoriesViewModel viewModelStories) //Look into ViewModels, more work but may be worth it.
        {
            Stories stories = new Stories();

            stories.Title = viewModelStories.Title;
            stories.Education = viewModelStories.Education;
            stories.Company = viewModelStories.Company;
            stories.Story = viewModelStories.Story;

            stories.CareerId = getCareerId(RouteData.Values["careerName"].ToString().Replace("-", " ")); 
            stories.UserId = 5; //MUST CHANGE to current user id!

            stories.PostDate = DateTime.Now; //account for time difference
            stories.StarCount = 0;
            stories.PostCount = 0;
            stories.FunnyCount = 0;
            stories.InformativeCount = 0;
            stories.IsActive = 1;

            if (stories.Title == null) stories.Title = "";
            if (stories.Education == null) stories.Education = "";
            if (stories.Company == null) stories.Company = "";

            if (ModelState.IsValid)
            {
                //Save to Database
                var db = new CareersDataContext();
                db.Stories.Add(stories);
                db.SaveChanges();

                var id = stories.Id; //grabs the recently added story's id.

                //use StringBuilder here for optimization
                return Redirect(@"~\" + "careers/" + RouteData.Values["careerName"].ToString() + "/" + id + "/" + stories.Title);
            }

            return Create(); //returns to the Get Create action
        }

        [HttpGet]
        public ActionResult Story()
        {
            //sanitize url for careername (again), id, AND slug.

            return View();
        }

        [HttpPost]
        [ActionName("Story")]
        public ActionResult StoryPost()
        {
            return View();
        }

        public static long getCareerId(string careerName)
        {
            long careerId = 1;

            var db = new CareersDataContext();
            var careers = db.Careers.Where(x => string.Equals(x.CareerName, careerName)).ToArray();

            careerId = careers[0].Id;

            return careerId;
        }

        //Different Functions.  change location of this to global or something so that the home controller can use this function also.  or include this controller. 
        public static string URLFriendly(string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }

        public static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("àåáâäãåą".Contains(s))
            {
                return "a";
            }
            else if ("èéêëę".Contains(s))
            {
                return "e";
            }
            else if ("ìíîïı".Contains(s))
            {
                return "i";
            }
            else if ("òóôõöøőð".Contains(s))
            {
                return "o";
            }
            else if ("ùúûüŭů".Contains(s))
            {
                return "u";
            }
            else if ("çćčĉ".Contains(s))
            {
                return "c";
            }
            else if ("żźž".Contains(s))
            {
                return "z";
            }
            else if ("śşšŝ".Contains(s))
            {
                return "s";
            }
            else if ("ñń".Contains(s))
            {
                return "n";
            }
            else if ("ýÿ".Contains(s))
            {
                return "y";
            }
            else if ("ğĝ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }
        }
	}
}