using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelGuide.BL;
using TravelGuide.Entities.Entity;
using TravelGuide.UI.Filters;

namespace TravelGuide.UI.Controllers
{
    public class AboutController : Controller
    {
        private AboutManager aboutManager = new AboutManager();
        public ActionResult Index()
        {
            return View(aboutManager.List());
        }

        [ErrorExc]
        [Auth]
        [AuthAdmin]        
        [HttpGet]
        public ActionResult GetAbout()
        {
            return View(aboutManager.Find(x=>x.Id == 1));
        }

        [ErrorExc]
        [Auth]
        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAbout(About about, HttpPostedFileBase AboutImage)
        {
            if (ModelState.IsValid)
            {
                About a = aboutManager.Find(x => x.Id == about.Id);
                if (AboutImage != null)
                {
                    string filename = $"Image_{about.Title}.{AboutImage.ContentType.Split('/')[1]}";
                    AboutImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    a.AboutImage = filename;
                }
                a.Title = about.Title;
                a.Description = about.Description;
                if (aboutManager.Update(a) > 0)
                    return RedirectToAction("GetAbout");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(about);
        }
    }
}