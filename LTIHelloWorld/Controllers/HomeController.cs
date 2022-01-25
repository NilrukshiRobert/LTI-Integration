using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LtiLibrary.Common;
using LtiLibrary.Lti1;
using LtiLibrary.AspNetCore.Extensions;

namespace LTIHelloWorld.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserProfile objUser)
        {
            if (ModelState.IsValid)
            {
                using (DB_Entities db = new DB_Entities())
                {
                    var obj = db.UserProfiles.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("Launch");
                       // return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Launch()
        {
            Uri launchUri;
            var request = new LtiRequest(LtiConstants.BasicLaunchLtiMessageType)
            {
                
                ConsumerKey = "3PKVrpMLbe7Mvollj8gfdjgdse8pQ08w8g",
                ContextId = "1",
                ResourceLinkId = "SSO",
    
                 Url = Uri.TryCreate(Request.Url, Url.Action("Tool", "Provider"), out launchUri) ? launchUri : null


            };
            return View(request.GetLtiRequestViewModel("gYHSF8Nhfd53jfs9IAdi-QOHKfFe7yg"));
        }
    }
}