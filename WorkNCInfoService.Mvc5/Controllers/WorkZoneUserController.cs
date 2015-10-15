using MVCtest.Models.WorkModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkNCInfoService.Mvc5.Models.WorkModels;

namespace WorkNCInfoService.Mvc5.Controllers
{
    public class WorkZoneUserController : Controller
    {

        WorkNCDbContext db = new WorkNCDbContext();
        // GET: User
        public ActionResult Index()
        {
            return View(db.WorkNC_UserPermission.ToList());
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(WorkNC_UserPermission user)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception)
            {
                return View();
            }
        }
        public ActionResult Search()
        {
            return PartialView("_SearchUser");
        }
        public ActionResult ViewPermission()
        {
            return PartialView("_WebPermission");
        }
    }
}