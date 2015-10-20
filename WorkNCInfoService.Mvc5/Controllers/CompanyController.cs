using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WorkNCInfoService.Mvc5.Models.WorkModels;

namespace WorkNCInfoService.Mvc5.Controllers
{
    public class CompanyController : Controller
    {
        WorkNCDbContext db = new WorkNCDbContext();
        private const int pageSize = 10;

        // GET: Company
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string companyName, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            if (companyName != null)
            {
                page = 1;
            }
            else
            {
                companyName = currentFilter;
            }
            ViewBag.CurrentFilter = companyName;

            var company = from s in db.WorkNC_Company select s;

            if(!String.IsNullOrEmpty(companyName))
            {
                company = company.Where(n => n.CompanyName.Contains(companyName));
            }

            switch(sortOrder)
            {
                case "name_asc":
                    company = company.OrderBy(n => n.CompanyName);
                    break;
                default:
                    company = company.OrderBy(n => n.CompanyId);
                    break;
            }

            int pageNumber = (page ?? 1);
            
            return View(company.ToPagedList(pageNumber, pageSize));
        }
        
        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Company/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search()
        {
            return PartialView("_SearchCompany");
        }
        public ActionResult CompanyDropdown()
        {
            List<WorkNC_Company> listCompany = new List<WorkNC_Company>();
            using (WorkNCDbContext db = new WorkNCDbContext())
            {
                listCompany = db.WorkNC_Company.ToList();
            }
            ViewBag.Company = new SelectList(listCompany, "CompanyId", "CompanyName");
            ViewBag.Company = new SelectList(db.WorkNC_Company.OrderBy(n => n.CompanyName), "CompanyId", "CompanyName");
            return PartialView("_CompanyPartial");

        }
    }
}
