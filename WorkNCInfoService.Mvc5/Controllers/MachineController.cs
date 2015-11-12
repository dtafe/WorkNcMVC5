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
    public class MachineController : Controller
    {
        WorkNCDbContext db = new WorkNCDbContext();
        // GET: Machine

        public ActionResult GetAllMachines(SearchMachines searchMachines)
        {
            var machine = from s in db.WorkNC_Machine select s;

            var user = (from s in db.WorkNC_UserPermission
                        where s.Username == User.Identity.Name
                        select s).FirstOrDefault();

            #region search with role is Admin
            if (User.IsInRole("Admin"))
            {
                int companyId;
                HttpCookie cookie = Request.Cookies["cookieCompany"];
                if (cookie != null)
                    companyId = Convert.ToInt32(cookie.Value);
                else
                    companyId = user.CompanyId;

                var result = machine.Where(n => n.CompanyId == companyId
                             && (searchMachines.FacrotyId == 0 || n.FactoryId == searchMachines.FacrotyId)
                             && (String.IsNullOrEmpty(searchMachines.Name)|| n.Name.Contains(searchMachines.Name))
                             && (searchMachines.isDeleted == true || n.isDeleted == false))
                            .Select(n => new { n.No, n.Name, n.isDeleted }).OrderBy(n => n.Name);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            #endregion

            #region search with role is Chief, Member
            else
            {
                return Json(machine.Where(n => n.CompanyId == user.CompanyId
                            && (searchMachines.FacrotyId == 0 || n.FactoryId == searchMachines.FacrotyId)
                            && (String.IsNullOrEmpty(searchMachines.Name) || n.Name.Contains(searchMachines.Name))
                            && (searchMachines.isDeleted == true || n.isDeleted.Equals(false)))
                            .Select(n => new { n.No, n.Name, n.isDeleted }).OrderBy(n => n.Name),
                        JsonRequestBehavior.AllowGet);
            }
            #endregion
        }
        public ActionResult Index()
        {
            return View();
        }
        // GET: Machiner/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public PartialViewResult SearchMachine()
        {
            var user = (from f in db.WorkNC_UserPermission
                        where f.Username == User.Identity.Name
                        select f).FirstOrDefault();           
            List<WorkNC_Factory> listFactory = new List<WorkNC_Factory>();
            using (WorkNCDbContext context = new WorkNCDbContext())
            {
                int companyId;
                HttpCookie cookie = Request.Cookies["cookieCompany"];
                if (cookie != null)
                    companyId = Convert.ToInt32(cookie.Value);
                else
                    companyId = user.CompanyId;
                listFactory = context.WorkNC_Factory.Where(n=>n.CompanyId == companyId).ToList();
            }
            ViewBag.FacrotyId = new SelectList(listFactory, "FactoryId", "Name");
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
                if (ModelState.IsValid)
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
                if (ModelState.IsValid)
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
                if (ModelState.IsValid)
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
