﻿using System;
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
    public class MachineController : Controller
    {
        WorkNCDbContext db = new WorkNCDbContext();
        // GET: Machine
        private const int pageSize = 10;
        public ActionResult Index(string sortOrder, string currentFilter, int? factoryId, string name, bool isDeleted = false, int? page = 1)
        {
            int pageNumber = (page ?? 1);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_asc" : ";";
            ViewBag.NoSort = sortOrder == "No" ? "no_asc" : "no";
            if (factoryId != 0 && name != null)
            {
                page = 1;
            }
            else
            {
                name = currentFilter;
            }

            ViewBag.CurrentFilter = name;
            var machine = from s in db.WorkNC_Machine select s;

            //sort machine
            switch (sortOrder)
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

            //use when cookieCompany is null
            var user = (from f in db.WorkNC_UserPermission
                        where f.Username == User.Identity.Name
                        select f).FirstOrDefault();

            //search when rolo is Admin
            if (User.IsInRole("Admin"))
            {
                int companyId;
                HttpCookie cookie = Request.Cookies["cookieCompany"];
                if (cookie != null)
                {
                    companyId = Convert.ToInt32(cookie.Value);
                    if (factoryId != null)
                    {
                        if (isDeleted == true)
                        {
                            return View(machine.Where(n => n.FactoryId == factoryId
                                        && n.CompanyId == companyId).ToPagedList(pageNumber, pageSize));
                        }
                        if (isDeleted == false)
                        {
                            return View(machine.Where(n => n.FactoryId == factoryId
                                        && n.isDeleted.Equals(false) && n.CompanyId == companyId).ToPagedList(pageNumber, pageSize));
                        }
                    }
                    //search by factoryName & isDeleted
                    if (!String.IsNullOrEmpty(name))
                    {
                        if (isDeleted == true)
                        {
                            return View(machine.Where(n => n.Name.Contains(name)
                                        && n.CompanyId == companyId).ToPagedList(pageNumber, pageSize));
                        }
                        if (isDeleted == false)
                        {
                            return View(machine.Where(n => n.Name.Contains(name)
                                        && n.isDeleted.Equals(false) && n.CompanyId == companyId).ToPagedList(pageNumber, pageSize));
                        }

                    }
                    return View(machine.Where(n => n.isDeleted.Equals(isDeleted)
                                                && n.CompanyId == companyId).ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    if (factoryId != null)
                    {
                        if (isDeleted == true)
                        {
                            return View(machine.Where(n => n.FactoryId == factoryId
                                        && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                        }
                        if (isDeleted == false)
                        {
                            return View(machine.Where(n => n.FactoryId == factoryId
                                        && n.isDeleted.Equals(false) && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                        }
                    }
                    //search by factoryName & isDeleted
                    if (!String.IsNullOrEmpty(name))
                    {
                        if (isDeleted == true)
                        {
                            return View(machine.Where(n => n.Name.Contains(name)
                                        && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                        }
                        if (isDeleted == false)
                        {
                            return View(machine.Where(n => n.Name.Contains(name)
                                        && n.isDeleted.Equals(false) && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                        }

                    }
                    return View(machine.Where(n => n.isDeleted.Equals(isDeleted)
                                                && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                }
            }

            //search when role is Chief, member
            else
            {
                if (factoryId != null)
                {
                    if (isDeleted == true)
                    {
                        return View(machine.Where(n => n.FactoryId == factoryId
                                    && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                    }
                    if (isDeleted == false)
                    {
                        return View(machine.Where(n => n.FactoryId == factoryId
                                    && n.isDeleted.Equals(false) && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                    }
                }
                //search by factoryName & isDeleted
                if (!String.IsNullOrEmpty(name))
                {
                    if (isDeleted == true)
                    {
                        return View(machine.Where(n => n.Name.Contains(name)
                                    && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                    }
                    if (isDeleted == false)
                    {
                        return View(machine.Where(n => n.Name.Contains(name)
                                    && n.isDeleted.Equals(false) && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
                    }

                }
                return View(machine.Where(n => n.isDeleted.Equals(isDeleted)
                                            && n.CompanyId == user.CompanyId).ToPagedList(pageNumber, pageSize));
            }
        }

        public JsonResult GetAllMachines(SearchMachines searchMachines)
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

                var machines = machine.Where(n => n.CompanyId == companyId
                             && (searchMachines.FacrotyId == 0 || n.FactoryId == searchMachines.FacrotyId)
                             && (String.IsNullOrEmpty(searchMachines.Name) || n.Name.Equals(searchMachines.Name))
                             && (searchMachines.isDeleted == true || n.isDeleted == false))
                            .Select(n => new { n.No, n.Name, n.isDeleted }).OrderBy(n => n.Name);
                return Json(machines, JsonRequestBehavior.AllowGet);
            }
            #endregion

            #region search with role is Chief, Member
            else
            {
                return Json(machine.Where(n => n.CompanyId == user.CompanyId
                            && n.FactoryId == searchMachines.FacrotyId
                            && (searchMachines.Name == string.Empty || n.Name.Equals(searchMachines.Name))
                            && (searchMachines.isDeleted == true || n.isDeleted.Equals(false)))
                            .Select(n => new { n.No, n.Name, n.isDeleted }).OrderBy(n => n.Name),
                        JsonRequestBehavior.AllowGet);
            }
            #endregion
        }
        public ActionResult List()
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
