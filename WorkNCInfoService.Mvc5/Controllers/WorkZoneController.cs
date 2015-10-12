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
    public class WorkZoneController : Controller
    {
        private const int pageSize = 5;
        WorkNCDbContext db = new WorkNCDbContext();
        // GET: WorkZone
        public ActionResult Index(int? page)
        {
            int pageNumber = (page??1);
            return View(db.WorkNC_WorkZone.OrderBy(n=>n.Name).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult List(string name = "", int factoryId=0, int machineId=0, DateTime? min = null, DateTime? max=null)
        {
            if(name!="")
            {
                var model = db.WorkNC_WorkZone.Where(n => n.Name.Contains(name));
                return View(model); 
            }
            if(factoryId!=0)
            {
                var model = db.WorkNC_WorkZone.Where(f => f.FactoryId == factoryId);
                return View(model);
            }
            if(machineId!=0)
            {
                var model = db.WorkNC_WorkZone.Where(m => m.MachineId == machineId);
                return View(model);
            }
            if(min!=null)
            {
                var date = from d in db.WorkNC_WorkZone
                           where min >= d.ProgramDate
                           select d;
            }
            if (max != null)
            {
                var date = from d in db.WorkNC_WorkZone
                           where max <= d.ProgramDate
                           select d;
            }
            if (min!=null && max!=null)
            {
                var date = from d in db.WorkNC_WorkZone
                           where min >= d.ProgramDate && max <= d.ProgramDate
                           select d;
            }
            return View(db.WorkNC_WorkZone);
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
        //public JsonResult GetFactory()
        //{
        //    List<WorkNC_Factory> allFactory = new List<WorkNC_Factory>();
        //    using (WorkNCDbContext context = new WorkNCDbContext())
        //    {
        //        allFactory = context.WorkNC_Factory.OrderBy(n => n.Name).ToList();
        //    }
        //    return new JsonResult { Data = allFactory, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    //return PartialView("_Search");
        //}
        //// Fetch Machine by Factory ID
        //public JsonResult GetMachine(int factoryId)
        //{
        //    List<WorkNC_Machine> allMachine = new List<WorkNC_Machine>();
        //    using (WorkNCDbContext context = new WorkNCDbContext())
        //    {
        //        allMachine = context.WorkNC_Machine.Where(n => n.FactoryId==factoryId).OrderBy(n => n.Name).ToList();
        //    }
        //    return new JsonResult { Data = allMachine, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}
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
