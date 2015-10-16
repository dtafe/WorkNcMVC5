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

namespace WorkNCInfoService.Mvc5.Controllers
{
    public class FactoryController : Controller
    {
        private const int pageSize = 10;
        WorkNCDbContext db = new WorkNCDbContext();
        // GET: Factory
        
        public ActionResult Index(string search, string check, int? page)
        {
            int pageNumber = (page ?? 1);
            if(!string.IsNullOrEmpty(search))
            {
                if (check == "true")
                {
                    var record = db.WorkNC_Factory.Where(n=>n.Name.Contains(search)).OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize);
                    return View(record);
                }
                if (check == "false")
                {
                    var record = db.WorkNC_Factory.Where(n => n.isDeleted.Equals(false) && n.Name.Contains(search)).OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize);
                    return View(record);
                }
            }
            else
            {
                if (check == "true")
                {
                    var record = db.WorkNC_Factory.OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize);
                    return View(record);
                }
                if (check == "false")
                {
                    var record = db.WorkNC_Factory.Where(n => n.isDeleted.Equals(false)).OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize);
                    return View(record);
                }
            }
            
            
            return View(db.WorkNC_Factory.Where(n => n.isDeleted.Equals(false)).OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize));

        }

        public ActionResult List(string name = "", string isDeleted = "" )
        {
            if (name != "")
            {
                if (isDeleted == "true")
                {
                    var record = db.WorkNC_Factory.Where(n => n.Name.Contains(name)).OrderBy(n => n.Name);
                    return View(record);
                }
                if (isDeleted == "false")
                {
                    var record = db.WorkNC_Factory.Where(n => n.isDeleted.Equals(false) && n.Name.Contains(name)).OrderBy(n => n.Name);
                    return View(record);
                }
            }
            else

            {
                if (isDeleted == "true")
                {
                    var record = db.WorkNC_Factory.OrderBy(n => n.Name);
                    return View(record);
                }
                if (isDeleted == "false")
                {
                    var record = db.WorkNC_Factory.Where(n => n.isDeleted.Equals(false)).OrderBy(n => n.Name);
                    return View(record);
                }
            }


            return View(db.WorkNC_Factory.Where(n => n.isDeleted.Equals(false)).OrderBy(n => n.Name));

        }
        // GET: Factory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Factory/Create
        public ActionResult Create()
        {
            return View();

        }

        // POST: Factory/Create
        [HttpPost]
        public ActionResult Create(WorkNC_Factory factory)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    factory.CreateDate = DateTime.Now;
                    //factory.CreateAccount = User.Identity.Name;
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
                if(ModelState.IsValid)
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
                if(ModelState.IsValid)
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
