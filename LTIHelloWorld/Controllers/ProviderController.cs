using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LtiLibrary.Lti1;
using LtiLibrary.Outcomes;
using LtiLibrary.Common;
using LtiLibrary.Extensions;
using LTIHelloWorld.Models;

namespace LTIHelloWorld.Controllers
{
    public class ProviderController : Controller
    {
        // GET: Provider
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tool()
        {
            try
            {
                // Parse and validate the request
                Request.CheckForRequiredLtiParameters();

                var ltiRequest = new LtiRequest(null);
                ltiRequest.ParseRequest(Request);

                if (!ltiRequest.ConsumerKey.Equals("3PKVrpMLjt6bgwa4be7MvolpQ08w8ghfd7ckkjg"))
                {
                    ViewBag.Message = "Invalid Consumer Key";
                    return View();
                }

                var oauthSignature = Request.GenerateOAuthSignature("gYHSF8Nhfd53jfs9IAdi-QOHKfFe7yg");
                if (!oauthSignature.Equals(ltiRequest.Signature))
                {
                    ViewBag.Message = "Invalid Signature";
                    return View();
                }

                // The request is legit, so display the tool
                ViewBag.Message = string.Empty;
                var model = new ToolModel
                {
                    ConsumerSecret = "gYHSF8Nhfd53jfs9IAdi-QOHKfFe7yg",
                    LtiRequest = ltiRequest
                };
                return View(model);
            }
            catch (LtiException e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }
    }
}