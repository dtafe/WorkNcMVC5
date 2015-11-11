using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WorkNCInfoService.Mvc5.Models.WorkModels;
using WorkNCInfoService.Mvc5.ViewModel;

namespace WorkNCInfoService.Mvc5.Controllers
{
    public class CompanyController : Controller
    {
        WorkNCDbContext db = new WorkNCDbContext();
        private const int pageSize = 10;

        // GET: Company
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllCompanies(SearchCompany searchCompany)
        {
            var company = (from s in db.WorkNC_Company
                          where ((String.IsNullOrEmpty(searchCompany.CompanyName) || s.CompanyName.Contains(searchCompany.CompanyName))
                                && (searchCompany.isDeleted == true || s.isDeleted == false))
                          select s).OrderBy(s=>s.CompanyName).ToList();
                return Json(company, JsonRequestBehavior.AllowGet);
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
        public PartialViewResult Search()
        {
            return PartialView("_SearchCompany");
        }

        public ActionResult FillDropDownCompany()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                List<WorkNC_Company> listCompany = new List<WorkNC_Company>();
                using (WorkNCDbContext db = new WorkNCDbContext())
                {
                    listCompany = db.WorkNC_Company.ToList();
                }
                var user = (from f in db.WorkNC_UserPermission
                            where f.Username == User.Identity.Name
                            select f).FirstOrDefault();

                //check role
                if (user != null)
                {
                    if (User.IsInRole("Admin"))
                    {
                        int companyId;
                        HttpCookie cookie = Request.Cookies["cookieCompany"];
                        if (cookie != null)
                            companyId = Convert.ToInt32(cookie.Value);
                        else
                            companyId = user.CompanyId;
                        ViewBag.Company = new SelectList(listCompany, "CompanyId", "CompanyName", companyId);
                    }
                    else
                    {
                        listCompany = db.WorkNC_Company.Where(n => n.CompanyId == user.CompanyId && n.isDeleted == false).ToList();
                        ViewBag.Company = new SelectList(listCompany, "CompanyId", "CompanyName");
                    }
                }
                
            }
            return PartialView("_CompanyPartial");
        }

        //change dropdownList Company 
        public ActionResult ChangeDropdownCompany(string companyId)
        {
            if (!String.IsNullOrEmpty(User.Identity.Name))
            {
                var user = (from f in db.WorkNC_UserPermission
                            where f.Username == User.Identity.Name
                            select f).FirstOrDefault();
                HttpCookie cookie = Request.Cookies["cookieCompany"];
                if(cookie==null)
                {
                    cookie = new HttpCookie("cookieCompany");
                }
                cookie.Value = companyId;
                Response.SetCookie(cookie);
            }
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);//Redirect(Request.RawUrl);
        }
    }
}
