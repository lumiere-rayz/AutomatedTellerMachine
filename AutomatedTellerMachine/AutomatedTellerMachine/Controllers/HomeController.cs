using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity.Owin;

namespace AutomatedTellerMachine.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        //GET /home/index
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var checkingAccountId = db.CheckingAccounts.Where(c => c.ApplicationUserId == userId).First().Id;
            ViewBag.CheckingAccountId = checkingAccountId;
            //adding the pin custom proprety
            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(userId);
            ViewBag.Pin = user.Pin;

            return View();
        }

        //GET /home/about
        [ActionName("about-this-atm")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("About");
        }
        public ActionResult Contact()
        {
            ViewBag.TheMessage = "Having toruble? send us a mesage.";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            //TODO: send a message to the HQ
            ViewBag.TheMessage = "Thanks, we got your message. ";
            return View();
        }
        public ActionResult Foo()
        {
            return View();
        }

        public ActionResult Serial(string lettercase)
        {
            var serial = "ASPNETMVCATM";
            if(lettercase == "lower")
            {
                return Content(serial.ToLower());
            }
            //return new HttpStatusCodeResult(403);
            // return Json(new { name = "serial", value = serial }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
    }

}