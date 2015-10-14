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
        public ActionResult Index(string name, int? page)
        {
            
            if(name!=null)
            {
                page = 1;
            }
            int pageNumber = (page ?? 1);
            if (!string.IsNullOrEmpty(name))
            {
                return View(db.WorkNC_Company.Where(n=>n.CompanyName.Contains(name)).OrderBy(n=>n.CompanyName).ToPagedList(pageNumber, pageSize));
            }
            return View(db.WorkNC_Company.OrderBy(n=>n.CompanyId).ToPagedList(pageNumber, pageSize));
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
    }
}
