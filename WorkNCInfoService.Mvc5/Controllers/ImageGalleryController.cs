using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkNCInfoService.Mvc5.Models;
using WorkNCInfoService.Mvc5.Models.WorkModels;

namespace WorkNCInfoService.Mvc5.Controllers
{
    public class ImageGalleryController : Controller
    {
        // GET: ImageUpload
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Galelery()
        {
            List<ImageGallery> listImage = new List<ImageGallery>();
            using (WorkNCDbContext context = new WorkNCDbContext())
            {
                listImage = context.ImageGallery.ToList();
            }
            return View(listImage);
        }
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(ImageGallery IG)
        {
            if(IG.File.ContentLength>(5*1024*1024))
            {
                ModelState.AddModelError("custom error ","file size must be less than 5Mb");
                return View();
            }
            IG.FileName = IG.File.FileName;
            IG.ImageSize = IG.File.ContentLength;

            byte[] data = new byte[IG.File.ContentLength];
            IG.File.InputStream.Read(data,0,IG.File.ContentLength);
            IG.ImageData = data;
            using (WorkNCDbContext context = new WorkNCDbContext())
            {
                context.ImageGallery.Add(IG);
                context.SaveChanges();
            }
            return RedirectToAction("Gallery");
        }
        
    }
}