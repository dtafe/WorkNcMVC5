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
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.Mvc5.Controllers
{
    public class WorkZoneController : Controller
    {
        private const int pageSize = 10;
        WorkNCDbContext db = new WorkNCDbContext();
        // GET: WorkZone
        public ActionResult Index(int? page)
        {
            int pageNumber = (page??1);
            return View(db.WorkNC_WorkZone.OrderBy(n=>n.Name).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult List(string name = "", int factoryId = 0, int machineId=0, DateTime? startDate=null, DateTime? endDate=null)
        {
             
            if (name != "")
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
            if(startDate!=null && endDate==null)
            {
                var date = from d in db.WorkNC_WorkZone
                           where  d.ProgramDate>= startDate
                           select d;
                return View(date);
            }
            if (startDate==null && endDate != null)
            {
                var date = from d in db.WorkNC_WorkZone
                           where  d.ProgramDate<= endDate
                           select d;
                return View(date);
            }
            if (startDate != null && endDate!=null)
            {
                var date = from d in db.WorkNC_WorkZone
                           where d.ProgramDate>=startDate && d.ProgramDate<= endDate
                           select d;
                return View(date);
            }
            return View(db.WorkNC_WorkZone);
        }
        // GET: WorkZone/Details/5
        public ActionResult Details(int? workzoneId)
        {
            if (workzoneId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            WorkNC_WorkZone workZone = db.WorkNC_WorkZone.Find(workzoneId);
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

        public ActionResult Edit(int? workzoneId)
        {

            List<WorkNC_Factory> listFactory = new List<WorkNC_Factory>();
            List<WorkNC_Machine> listMachine = new List<WorkNC_Machine>();

            using (WorkNCDbContext context = new WorkNCDbContext())
            {
                listFactory = db.WorkNC_Factory.ToList();
                listMachine = db.WorkNC_Machine.ToList();
            }
            if (workzoneId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            WorkNC_WorkZone workZone = db.WorkNC_WorkZone.Find(workzoneId); 

            //fill to DropdownList
            ViewBag.Factory = new SelectList(listFactory.OrderBy(n => n.Name), "FactoryId", "Name");
            ViewBag.Machine = new SelectList(listMachine.OrderBy(n => n.Name), "MachineId", "Name");

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
            ViewBag.Factory = new SelectList(listFactory.OrderBy(n => n.Name), "FactoryId", "Name");
            ViewBag.Machine = new SelectList(listMachine.OrderBy(n => n.Name), "MachineId", "Name");

            try
            {
                if (ModelState.IsValid)
                {
                    //workZone.FactoryId = ViewBag.Factory("Name");
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

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> workzoneId)
        {
            List<WorkNC_WorkZone> listDelete = db.WorkNC_WorkZone.Where(x => workzoneId.Contains(x.WorkZoneId)).ToList();
            foreach(WorkNC_WorkZone item in listDelete)
            {
                db.WorkNC_WorkZone.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Search()
        {
            return PartialView("_Search");
        }
        
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
