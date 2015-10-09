using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkNCInfoService.Mvc5.Models.WorkModels;

namespace WorkNCInfoService.Mvc5.Controllers
{
    public class MachineController : Controller
    {
        WorkNCDbContext db = new WorkNCDbContext();
        // GET: Machine
        public ActionResult Index()
        {
            return View(db.WorkNC_Machine.ToList());
        }

        // GET: Machine/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Machine/Create
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult SearchMachine()
        {
            List<WorkNC_Factory> listFactory = new List<WorkNC_Factory>();
            using (WorkNCDbContext context = new WorkNCDbContext())
            {
                listFactory = context.WorkNC_Factory.ToList();
            }
            ViewBag.Factory = new SelectList(listFactory, "FactoryId", "Name");
            //ViewBag.Factory = new SelectList(db.WorkNC_Factory.OrderBy(n => n.Name), "FactoryId", "Name");
            return PartialView("_SearchMachine");
        }

        // POST: Machine/Create
        [HttpPost]
        public ActionResult Create(WorkNC_Machine machine)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Entry(machine).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(machine);
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Machine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            WorkNC_Machine machine = db.WorkNC_Machine.Find(id);
            if (machine == null)
                return HttpNotFound();
            return View(machine);
        }

        // POST: Machine/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, WorkNC_Machine machine)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Entry(machine).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(machine);
            }
            catch
            {
                return View();
            }
        }

        // GET: Machine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            WorkNC_Machine machine = db.WorkNC_Machine.Find(id);
            if (machine == null)
                return HttpNotFound();
            return View(machine);
        }

        // POST: Machine/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, WorkNC_Machine machine)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Entry(machine).State = EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(machine);
            }
            catch
            {
                return View();
            }
        }
    }
}
