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
    public class MachineController : Controller
    {
        WorkNCDbContext db = new WorkNCDbContext();
        // GET: Machine
        private const int page = 10;
        public ActionResult Index(string sortOrder, string currentFilter, int? factoryId, string name, string isDeleted, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            ViewBag.NoSort = sortOrder == "No" ? "no_asc" : "no";
            if (factoryId!=0 && name!=null)
            {
                page = 1;
            }
            else
            {
                name = currentFilter;
            }

            ViewBag.CurrentFilter = name;
            var machine = from s in db.WorkNC_Machine select s;

            if (factoryId != null)
            {
                if(isDeleted=="true")
                {
                    machine = machine.Where(n => n.FactoryId == factoryId);
                }
                if(isDeleted=="false")
                {
                    machine = machine.Where(n => n.FactoryId == factoryId && n.isDeleted.Equals(false));
                }
            }

            if (!String.IsNullOrEmpty(name))
            {
                if(isDeleted=="true")
                {
                    machine = machine.Where(n => n.Name.Contains(name));
                }
                if (isDeleted == "false")
                {
                    machine = machine.Where(n => n.Name.Contains(name)&&n.isDeleted.Equals(false));
                }

            }
            
            switch(sortOrder)
            {
                case "no_asc":
                    machine = machine.OrderBy(n => n.No);
                    break;
                case "name_asc":
                    machine = machine.OrderBy(n => n.Name);
                    break;
                default:
                    machine = machine.OrderBy(n => n.MachineId);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(machine.ToPagedList(pageNumber, pageSize));       }

        // GET: Machine/Details/5
        public ActionResult Details(int id)
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
        // GET: Machine/Create
        public ActionResult Create()
        {
            return View();
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
