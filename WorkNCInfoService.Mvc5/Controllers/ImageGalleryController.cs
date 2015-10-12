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
            
            return View(listImage);
        }
        public ActionResult Upload()
        {
            return View();
        }
        
        
    }
}