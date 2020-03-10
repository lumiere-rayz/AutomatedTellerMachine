using AutomatedTellerMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AutomatedTellerMachine.Controllers
{
    [Authorize]
    public class CheckingAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); 
        // GET: CheckingAccount
        public ActionResult Index()
        {
            return View();
        }

        // GET: CheckingAccount/Details
        public ActionResult Details()
        {
            //to get the checking account from the db set, first grab thge userId which requires the microsoft.Aspnet.Identity namespace
            var userId = User.Identity.GetUserId();
            // filter on the actual checkingAccount db set with the where() and filter on ApplicationUserId and take the first result
            var checkingAccount = db.CheckingAccounts.Where(c => c.ApplicationUserId == userId).First();
            
            return View(checkingAccount);
        }

        // GET: CheckingAccount/DetailsForAdmin
        [Authorize(Roles ="Admin")] // only to be access by user with the admin role
        public ActionResult DetailsForAdmin(int id)
        {

            
            var checkingAccount = db.CheckingAccounts.Find(id);

            return View("Details",checkingAccount);
        }

        //adding a method for the admin user so they can view the balance for any account by parsing the checkingAccount id
        // to easy access the account we need a view that list all the existing Accounts
        [Authorize(Roles = "Admin")] //only to be access by user with the admin role
        public ActionResult List()
        {
            return View(db.CheckingAccounts.ToList());
        }

        public ActionResult Statement(int id)
        {
            var checkingAccount = db.CheckingAccounts.Find(id);
            return View(checkingAccount.Transactions.ToList());
        }
        // GET: CheckingAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CheckingAccount/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckingAccount/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CheckingAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckingAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CheckingAccount/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
