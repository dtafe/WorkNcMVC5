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
            int pageNumber = (page ?? 1);
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

            switch (sortOrder)
            {
                case "name_asc":
                    company = company.OrderBy(n => n.CompanyName);
                    break;
                default:
                    company = company.OrderBy(n => n.CompanyId);
                    break;
            }
            if (!String.IsNullOrEmpty(companyName))
            {
                return View(company.Where(n => n.CompanyName.Contains(companyName)).ToPagedList(pageNumber,pageSize));
            }
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
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                List<WorkNC_Company> listCompany = new List<WorkNC_Company>();
                using (WorkNCDbContext db = new WorkNCDbContext())
                {
                    listCompany = db.WorkNC_Company.ToList();
                }
                List<WorkNC_Company> list = new List<WorkNC_Company>();

                var user = (from f in db.WorkNC_UserPermission
                            where f.Username == User.Identity.Name
                            select f).FirstOrDefault();

                //check role
                if (user != null)
                {
                    if (User.IsInRole("Admin"))
                    {
                        list = db.WorkNC_Company.ToList();
                    }
                    else
                    {
                        list = db.WorkNC_Company.Where(n => n.CompanyId == user.CompanyId && n.isDeleted == false).ToList();
                    }
                }
                HttpCookie cookie = Request.Cookies["cookieCompany"];
                if (cookie != null)
                {
                    //companyId = Convert.ToInt32(cookie.Value);
                    
                }
                //ViewBag.CompanySelected = companyId;
                ViewBag.Company = new SelectList(list, "CompanyId", "CompanyName");
                
            }

            return PartialView("_CompanyPartial");
        }

        //change dropdownList Company 
        public ActionResult ChangeDropdownCompany(string companyId)
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                var user = (from f in db.WorkNC_UserPermission
                            where f.Username == User.Identity.Name
                            select f).FirstOrDefault();
                HttpCookie cookie = Request.Cookies["cookieCompany"];
                if(cookie==null)
                {
                    cookie = new HttpCookie("cookieCompany");
                }
                cookie.Value = Convert.ToString(companyId);
                Response.SetCookie(cookie);
            }
            
            return Redirect(Request.RawUrl);
        }
    }
}
