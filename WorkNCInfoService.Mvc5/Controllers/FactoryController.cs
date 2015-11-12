using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WorkNCInfoService.Mvc5.Models.WorkModels;
using WorkNCInfoService.Mvc5.ViewModel;

namespace WorkNCInfoService.Mvc5.Controllers
{
    [Authorize]
    public class FactoryController : Controller
    {
        private const int pageSize = 10;
        WorkNCDbContext db = new WorkNCDbContext();
        // GET: Factory
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllFactories(SearchFactory searchFactory)
        {
            var factory = from f in db.WorkNC_Factory select f;
            var user = (from f in db.WorkNC_UserPermission
                        where f.Username == User.Identity.Name
                        select f).FirstOrDefault();

            if(User.IsInRole("Admin"))
            {
                int companyId=0;
                HttpCookie cookie = Request.Cookies["cookieCompany"];
                if (cookie != null)
                    companyId = Convert.ToInt32(cookie.Value);
                else
                    companyId = user.CompanyId;

                var resultFactory = factory.Where(n => n.CompanyId == companyId
                                    &&(String.IsNullOrEmpty(searchFactory.Name) ||n.Name.Contains(searchFactory.Name))
                                    &&(searchFactory.isDeleted==true||n.isDeleted==false)
                                    ).Select(n=>new { n.No, n.Name, n.isDeleted}).OrderBy(n=>n.Name);
                return Json(resultFactory, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var resultFactory = factory.Where(n => n.CompanyId == user.CompanyId
                                    && (String.IsNullOrEmpty(searchFactory.Name) || n.Name.Contains(searchFactory.Name))
                                    && (searchFactory.isDeleted == true || n.isDeleted == false)
                                    ).Select(n => new { n.No, n.Name, n.isDeleted }).OrderBy(n => n.Name);
                return Json(resultFactory, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: Factory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Factory/Create
        public ActionResult Create()
        {

            List<WorkNC_Company> listCompany = new List<WorkNC_Company>();
            using (WorkNCDbContext db = new WorkNCDbContext())
            {
                listCompany = db.WorkNC_Company.ToList();
            }
            ViewBag.Company = new SelectList(listCompany, "CompanyId", "CompanyName");
            return View();
        }

        // POST: Factory/Create
        [HttpPost]
        public ActionResult Create(WorkNC_Factory factory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    factory.ModifiedAccount = User.Identity.Name;
                    factory.ModifiedDate = DateTime.Now;
                    factory.CreateDate = DateTime.Now;
                    factory.CreateAccount = User.Identity.Name;
                    db.Entry(factory).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(factory);
            }
            catch
            {
                return View();
            }
        }

        // GET: Factory/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            WorkNC_Factory factory = db.WorkNC_Factory.Find(id);
            if (factory == null)
                return HttpNotFound();
            return View(factory);
        }

        // POST: Factory/Edit/5
        [HttpPost]
        public ActionResult Edit(WorkNC_Factory factory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    factory.ModifiedAccount = User.Identity.Name;
                    factory.ModifiedDate = DateTime.Now;

                    db.Entry(factory).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(factory);
            }
            catch
            {
                return View();
            }
        }

        // GET: Factory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            WorkNC_Factory factory = db.WorkNC_Factory.Find(id);
            if (factory == null)
                return HttpNotFound();
            return View(factory);
        }

        // POST: Factory/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, WorkNC_Factory factory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(factory).State = EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(factory);
            }
            catch
            {
                return View();
            }
        }
        public PartialViewResult Search()
        {
            return PartialView("_SearchFactory");
        }

    }
}
