﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkNCInfoService.Mvc5.Models.WorkModels;

namespace WorkNCInfoService.Mvc5.Controllers
{
    [Authorize]
    public class WorkZoneDetailController : Controller
    {
        WorkNCDbContext db = new WorkNCDbContext();
        // GET: WorkZoneDetail
        public PartialViewResult List(int? workzoneId)
        {
            if (workzoneId != 0)
            {
                var model = db.WorkNC_WorkZoneDetail.Where(n => n.WorkZoneId == workzoneId);
                return PartialView(model);
            }
            return PartialView("List");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            WorkNC_WorkZoneDetail zoneDetail = db.WorkNC_WorkZoneDetail.SingleOrDefault(n=>n.WorkZoneDetailId==id);
            if (zoneDetail == null)
                return HttpNotFound();
            return View(zoneDetail);
        }
        [HttpPost]
        public ActionResult Edit(int? id,WorkNC_WorkZoneDetail zoneDetail)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Entry(zoneDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Edit", "WorkZone", new { id=zoneDetail.WorkZoneId});
                }
                return View(zoneDetail);
            }
            catch
            {
                return View();
            }
        }
    }
}