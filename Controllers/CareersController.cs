using CareerStories.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
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
            long careerId = 1;
            var list = db.Careers.Where(u => u.IsActive == 1).OrderBy(u => u.CareerName).ToList();
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

                    //set careerId to use in the list of stories below.
                    careerId = list.ElementAt(i).Id;
                }
            }

            ViewBag.careersList = careersList;

            if (!careerNameUrlFound)
            {
                return Redirect("/careers/administrative-assistant"); //anything after careers/ that is not valid will be redirected to aa.
            }

            //get stories from selected career, sort, and put in viewbag.
            var careerStoriesList = db.Stories.Where(u => u.IsActive == 1 && u.CareerId == careerId).OrderByDescending(u => u.PostCount).ToList();
            ViewBag.careerStoriesList = careerStoriesList;
         
            return View(careersViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")] //if the signature changes, you can comment this line out
        public ActionResult IndexPost()
        {
            string selected = Request.Form["careersList"]; //grabs the value of the selected item that was posted.
            return Redirect(@"~\" + "careers/" + selected);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
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

            bool careerNameUrlFound = false;

            string careerNameUrl = "";
            if (RouteData.Values["careerName"] != null)
            {
                careerNameUrl = RouteData.Values["careerName"].ToString();
                careerNameUrl = careerNameUrl.Replace("-", " ");
            }

            var db = new CareersDataContext();
            var list = db.Careers.Where(u => u.IsActive == 1).ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).CareerName.ToLower().Equals(careerNameUrl))
                {
                    careerNameUrlFound = true;
                }
            }

            if (!careerNameUrlFound)
            {
                return Redirect("/careers/administrative-assistant"); //anything after careers/ that is not valid will be redirected to aa.
            }
            ////////////////////////end redirect check/////////////////////////////////////////////

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoriesCreateViewModel viewModelStories) //Look into ViewModels, more work but may be worth it.
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

            bool careerNameUrlFound = false;

            string careerNameUrl = "";
            if (RouteData.Values["careerName"] != null)
            {
                careerNameUrl = RouteData.Values["careerName"].ToString();
                careerNameUrl = careerNameUrl.Replace("-", " ");
            }

            var db = new CareersDataContext();
            var list = db.Careers.Where(u => u.IsActive == 1).ToList();

            for (int i = 0; i < list.Count(); i++)
            {
                if (list.ElementAt(i).CareerName.ToLower().Equals(careerNameUrl))
                {
                    careerNameUrlFound = true;
                }
            }

            if (!careerNameUrlFound)
            {
                return Redirect("/careers/administrative-assistant"); //anything after careers/ that is not valid will be redirected to aa.
            }
            ////////////////////////end redirect check/////////////////////////////////////////////

            //This is how you get the current user id.
            string userId = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userId = userIdClaim.Value;
                }
            }

            Stories stories = new Stories();

            stories.Title = viewModelStories.Title;
            stories.Education = viewModelStories.Education;
            stories.Company = viewModelStories.Company;
            stories.Salary = viewModelStories.Salary;
            stories.Location = viewModelStories.Location;
            stories.Story = viewModelStories.Story;

            stories.CareerId = getCareerId(RouteData.Values["careerName"].ToString().Replace("-", " "));
            stories.CareerName = RouteData.Values["careerName"].ToString().Replace("-", " ");
            stories.UserId = userId; //MUST CHANGE to current user id!
            stories.Username = User.Identity.Name;

            stories.PostDate = DateTime.Now.ToString("MM/dd/yyyy h:mm tt"); //account for time difference
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
                db = new CareersDataContext();
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
            var db = new CareersDataContext();
            var storiesViewModel = new StoriesViewModel();
            long inputId = Int64.Parse(RouteData.Values["Id"].ToString());
            var story = db.Stories.Where(x => x.Id == inputId).ToArray();

            if (RouteData.Values["slug"] == null)
            {
                return Redirect(@"~\" + "careers/" + story[0].CareerName + "/" + RouteData.Values["Id"].ToString() + "/" + getSlug(Int64.Parse(RouteData.Values["Id"].ToString())));
            }

            //sanitize url for careername (again), id, AND slug.
            string inputUrlParam;
            string cleanUrlParam;

            inputUrlParam = RouteData.Values["careerName"].ToString();
            cleanUrlParam = URLFriendly(inputUrlParam);

            if (!inputUrlParam.Equals(cleanUrlParam))
            {//use StringBuilder here for optimization
                return Redirect(@"~\" + "careers/" + cleanUrlParam + "/" + RouteData.Values["Id"].ToString() + "/"
                    + RouteData.Values["slug"].ToString());
            }
           
            if (!RouteData.Values["slug"].ToString().Equals(getSlug(Int64.Parse(RouteData.Values["Id"].ToString())))) {
                return Redirect(@"~\" + "careers/" + RouteData.Values["careerName"].ToString() + "/" + RouteData.Values["Id"].ToString() + "/" + getSlug(Int64.Parse(RouteData.Values["Id"].ToString())));
            }

            if (story.Length < 1)
            {//wrong id
                return Redirect(@"~\" + "careers/" + RouteData.Values["careerName"].ToString()); 
            }
            
            storiesViewModel.CareerName = story[0].CareerName;
            storiesViewModel.StarCount = story[0].StarCount;
            storiesViewModel.PostCount = story[0].PostCount;
            storiesViewModel.FunnyCount = story[0].FunnyCount;
            storiesViewModel.InformativeCount = story[0].InformativeCount;
            storiesViewModel.Username = story[0].Username;
            storiesViewModel.Story = story[0].Story;
            storiesViewModel.Title = story[0].Title;
            storiesViewModel.Education = story[0].Education;
            storiesViewModel.Company = story[0].Company;
            storiesViewModel.Salary = story[0].Salary;
            storiesViewModel.Location = story[0].Location;
            storiesViewModel.PostDate = story[0].PostDate;
            
            ///////////////////////////////
            /*Posts and Replies: use the inputId variable to find corresponding posts and replies.  Add them to ViewBag variables and display them 
             * in the View.
             * */
            var PostsList = db.Posts.Where(u => u.IsActive == 1 && u.StoryId == inputId).OrderBy(u => u.PostDate).ToList();
            ViewBag.PostsList = PostsList; 
            ///////////////////////////////
            return View(storiesViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ActionName("Story")]
        public ActionResult StoryPost(PostsViewModel viewModelPosts)
        {
            //Don't do any redirects, the slug is null when form is posted.

        
            //This is how you get the current user id.
            string userId = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userId = userIdClaim.Value;
                }
            }

            Posts posts = new Posts();
            posts.StoryId = Int64.Parse(RouteData.Values["Id"].ToString()); //what if user changes id parameter in url then presses post??
            posts.UserId = userId;
            posts.Username = User.Identity.Name;
            posts.ReplyCount = 0;
            posts.LikeCount = 0;
            posts.PostDate = DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
            posts.Post = viewModelPosts.Post;
            posts.IsActive = 1;

            if (ModelState.IsValid)
            {
                //Save to Database
                var db = new CareersDataContext();
                db.Posts.Add(posts);
                db.SaveChanges();

                //increment post count for current story
                var story = db.Stories.Find(Int64.Parse(RouteData.Values["Id"].ToString()));
                story.PostCount += 1;
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();


                //use StringBuilder here for optimization
                return Redirect(@"~\" + "careers/" + RouteData.Values["careerName"].ToString() + "/" + RouteData.Values["Id"].ToString() + "/" + getSlug(posts.StoryId));
            }

            return Story(); //return the getStory action so that the new reply or post will be shown.
        }

        public static string getSlug(long StoryId)
        {
            string slug = "";

            var db = new CareersDataContext();
            var stories = db.Stories.Where(x => x.Id == StoryId).ToArray();

            slug = stories[0].Title;
            slug = URLFriendly(slug);

            return slug;
        }

        public static long getCareerId(string careerName)
        {
            long careerId = 1;

            var db = new CareersDataContext();
            var careers = db.Careers.Where(x => string.Equals(x.CareerName, careerName)).ToArray();

            if (careerName == "" || careerName == null)
            {
                careerId = 1;
            }
            else
            {
                careerId = careers[0].Id;
            }

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