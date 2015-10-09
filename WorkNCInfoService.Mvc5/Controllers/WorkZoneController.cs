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
    public class WorkZoneController : Controller
    {
        WorkNCDbContext db = new WorkNCDbContext();
        // GET: WorkZone
        public ActionResult Index()
        {
            return View(db.WorkNC_WorkZone.ToList());
        }

        // GET: WorkZone/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            WorkNC_WorkZone workZone = db.WorkNC_WorkZone.Find(id);
            if (workZone == null)
                return HttpNotFound();

            return View(workZone);
        }

        // GET: WorkZone/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkZone/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkZone/Edit/5

        public ActionResult Edit(int? id)
        {

            List<WorkNC_Factory> listFactory = new List<WorkNC_Factory>();
            List<WorkNC_Machine> listMachine = new List<WorkNC_Machine>();

            using (WorkNCDbContext context = new WorkNCDbContext())
            {
                listFactory = db.WorkNC_Factory.ToList();
                listMachine = db.WorkNC_Machine.ToList();
            }
            ViewBag.Factory = new SelectList(listFactory, "FactoryId", "Name");
            ViewBag.Machine = new SelectList(listMachine, "MachineId", "Name");


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            WorkNC_WorkZone workZone = db.WorkNC_WorkZone.Find(id); 

            //fill to DropdownList
            ViewBag.Factory = new SelectList(db.WorkNC_Factory.OrderBy(n => n.Name), "FactoryId", "Name");
            ViewBag.Machine = new SelectList(db.WorkNC_Machine.OrderBy(n => n.Name), "MachineId", "Name");

            if (workZone == null)
                return HttpNotFound();
            return View(workZone);
        }

        // POST: WorkZone/Edit/5
        [HttpPost]
        public ActionResult Edit(WorkNC_WorkZone workZone)
        {
            List<WorkNC_Factory> listFactory = new List<WorkNC_Factory>();
            List<WorkNC_Machine> listMachine = new List<WorkNC_Machine>();
            using (WorkNCDbContext context = new WorkNCDbContext())
            {
                listFactory = db.WorkNC_Factory.ToList();
                listMachine = db.WorkNC_Machine.ToList();
            }
            ViewBag.Factory = new SelectList(listFactory, "FactoryId", "Name");
            ViewBag.Machine = new SelectList(listMachine, "MachineId", "Name");

            try
            {
                if (ModelState.IsValid)
                {
                    workZone.ModifiedDate = DateTime.Now;
                    workZone.ModifiedAccount = User.Identity.Name;
                    db.Entry(workZone).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(workZone);
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkZone/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            WorkNC_WorkZone workZone = db.WorkNC_WorkZone.Find(id);
            if (workZone == null)
                return HttpNotFound();
            return View(workZone);
        }

        // POST: WorkZone/Delete/5
        [HttpPost]
        public ActionResult Delete(WorkNC_WorkZone workZone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(workZone).State = EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(workZone);
            }
            catch
            {
                return View();
            }
        }


        #region form search
        public ActionResult Search()
        {
            return PartialView("_Search");
        }
        public JsonResult GetFactory()
        {
            List<WorkNC_Factory> allFactory = new List<WorkNC_Factory>();
            using (WorkNCDbContext context = new WorkNCDbContext())
            {
                allFactory = context.WorkNC_Factory.OrderBy(n => n.Name).ToList();
            }
            return new JsonResult { Data = allFactory, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return PartialView("_Search");
        }
        // Fetch Machine by Factory ID
        public JsonResult GetMachine(int factoryId)
        {
            List<WorkNC_Machine> allMachine = new List<WorkNC_Machine>();
            using (WorkNCDbContext context = new WorkNCDbContext())
            {
                allMachine = context.WorkNC_Machine.Where(n => n.FactoryId==factoryId).OrderBy(n => n.Name).ToList();
            }
            return new JsonResult { Data = allMachine, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion
        public JsonResult GetAllFactory()
        {
            return Json(db.WorkNC_Factory.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMachineByIdFactory(string factoryId)
        {
            int id = Convert.ToInt32(factoryId);
            var machine = from a in db.WorkNC_Machine where a.FactoryId == id select a;
            return Json(machine); 
        }
    }
}
