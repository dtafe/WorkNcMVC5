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
using System.IO;

namespace WorkNCInfoService.Mvc5.Controllers
{
    public class WorkZoneController : Controller
    {
        private const int pageSize = 10;
        WorkNCDbContext db = new WorkNCDbContext();
        
        // GET: WorkZone
        public ActionResult Index(string sortOrder, string currentFilter, string name = "", int factoryId = 0, int machineId = 0, DateTime? startDate=null, DateTime? endDate=null, int? page=1)
        {
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            ViewBag.CompanySort = sortOrder == "Company" ? "company_asc" : "";

            if (name != null)
                page = 1;
            else
                name = currentFilter;

            ViewBag.CurrentFilter = name;

            var workZone = from s in db.WorkNC_WorkZone select s;
            var user = (from f in db.WorkNC_UserPermission
                        where f.Username.Equals(User.Identity.Name)
                        select f).FirstOrDefault();    
            switch(sortOrder)
            {
                case "name_asc":
                    workZone = workZone.OrderBy(n => n.Name);            
                    break;
                case "company_asc":
                    workZone = workZone.OrderBy(n => n.CompanyName);
                    break;
                default:
                    workZone = workZone.OrderBy(n => n.Name);
                    break;             
            }
            if (User.IsInRole("Admin"))
            {
                int companyId = Convert.ToInt32(Request.Cookies["cookieCompany"].Value);
                if(companyId.ToString()!=null)
                {
                    if(!String.IsNullOrEmpty(name))
                    {
                        View(workZone.Where(n => n.Name.Equals(name) && n.CompanyId==companyId).ToPagedList(pageNumber, pageSize));    
                    }
                    if(factoryId!=0)
                    {
                        View(workZone.Where(n => n.FactoryId == factoryId && n.CompanyId == companyId).ToPagedList(pageNumber, pageSize));
                    }
                    if(machineId!=0)
                    {
                        View(workZone.Where(n => n.MachineId == machineId && n.CompanyId == companyId).ToPagedList(pageNumber, pageSize));
                    }
                    if(startDate == null && endDate != null)
                    {
                        View(workZone.Where(n => n.ProgramDate <= endDate && n.CompanyId == companyId).ToPagedList(pageNumber, pageSize));
                    }
                    if(startDate !=null && endDate==null)
                    {
                        View(workZone.Where(n => n.ProgramDate >= startDate && n.CompanyId == companyId).ToPagedList(pageNumber, pageSize));
                    }
                    if(startDate !=null && endDate !=null)
                    {
                        View(workZone.Where(n => n.ProgramDate >= startDate && n.ProgramDate <= endDate
                                                && n.CompanyId == companyId).ToPagedList(pageNumber, pageSize));
                    }
                    return View(workZone.Where(n => n.CompanyId == 1).ToPagedList(pageNumber, pageSize));                    
                }
                else
                {
                    if (!String.IsNullOrEmpty(name))
                    {
                        View(workZone.Where(n => n.Name.Equals(name)).ToPagedList(pageNumber, pageSize));
                    }
                    if (factoryId != 0)
                    {
                        View(workZone.Where(n => n.FactoryId == factoryId).ToPagedList(pageNumber, pageSize));
                    }
                    if (machineId != 0)
                    {
                        View(workZone.Where(n => n.MachineId == machineId).ToPagedList(pageNumber, pageSize));
                    }
                    if (startDate == null && endDate != null)
                    {
                        View(workZone.Where(n => n.ProgramDate <= endDate).ToPagedList(pageNumber, pageSize));
                    }
                    if (startDate != null && endDate == null)
                    {
                        View(workZone.Where(n => n.ProgramDate >= startDate).ToPagedList(pageNumber, pageSize));
                    }
                    if (startDate != null && endDate != null)
                    {
                        View(workZone.Where(n => n.ProgramDate >= startDate && n.ProgramDate <= endDate
                                                ).ToPagedList(pageNumber, pageSize));
                    }
                    return View(workZone.Where(n => n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(name))
                {
                    View(workZone.Where(n => n.Name.Equals(name) && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                }
                if (factoryId != 0)
                {
                    View(workZone.Where(n => n.FactoryId == factoryId && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                }
                if (machineId != 0)
                {
                    View(workZone.Where(n => n.MachineId == machineId && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                }
                if (startDate == null && endDate != null)
                {
                    View(workZone.Where(n => n.ProgramDate <= endDate && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                }
                if (startDate != null && endDate == null)
                {
                    View(workZone.Where(n => n.ProgramDate >= startDate && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                }
                if (startDate != null && endDate != null)
                {
                    View(workZone.Where(n => n.ProgramDate >= startDate && n.ProgramDate <= endDate
                                            && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                }
                return View(workZone.Where(n => n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
            }
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
            WorkNC_WorkZone workZone = db.WorkNC_WorkZone.Find(id); 

            //fill to DropdownList
            ViewBag.Factory = new SelectList(listFactory.OrderBy(n => n.Name), "FactoryId", "Name");
            ViewBag.Machine = new SelectList(listMachine.OrderBy(n => n.Name), "MachineId", "Name");
            return View(workZone);
        }

        // POST: WorkZone/Edit/5
        [HttpPost]
        public ActionResult Edit(WorkNC_WorkZone workZone, HttpPostedFileBase upload)
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
                    if(upload!=null && upload.ContentLength>0)
                    {
                        
                    }
                    //workZone.FactoryId = ViewBag.Factory("Name");
                    workZone.ModifiedDate = DateTime.Now;
                    workZone.ModifiedAccount = User.Identity.Name;
                    db.Entry(workZone).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(workZone);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("error", e);
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> id)
        {
            List<WorkNC_WorkZone> listDelete = db.WorkNC_WorkZone.Where(x => id.Contains(x.WorkZoneId)).ToList();
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
