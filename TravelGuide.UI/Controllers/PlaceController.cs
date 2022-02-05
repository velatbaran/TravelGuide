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
    [ErrorExc]
    [Auth]
    [AuthAdmin]
    public class PlaceController : Controller
    {
        private PlaceManager placeManager = new PlaceManager();
        private CityManager cityManager = new CityManager();
        public ActionResult Index(int id)
        {
            var placesOfCity = placeManager.List(x => x.IsDeleted == false && x.CityId == id).OrderByDescending(x => x.CreatedOn);
            ViewBag.CityId = id;
            ViewBag.CityName = cityManager.Find(x => x.Id == id).Name;
            return View(placesOfCity);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.CityId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Place place, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    place.Image = "default.jpeg";
                }
                else
                {
                    string filename = $"Image_{place.Name}.{Image.ContentType.Split('/')[1]}";
                    Image.SaveAs(Server.MapPath($"~/images/medias/{filename}"));
                    place.Image = filename;
                }

                if (placeManager.Insert(place) > 0)
                    return RedirectToAction("Index", new { id = place.CityId });
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CityId = place.CityId;
            return View(place);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = placeManager.Find(x => x.Id == id);
            ViewBag.CityId = place.CityId;
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Place place, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                Place p = placeManager.Find(x => x.Id == place.Id);
                if (Image != null)
                {
                    string filename = $"Image_{place.Name}.{Image.ContentType.Split('/')[1]}";
                    Image.SaveAs(Server.MapPath($"~/images/medias/{filename}"));
                    p.Image = filename;
                }

                p.CityId = place.CityId;
                p.Name = place.Name;
                p.Address = place.Address;
                p.Phone = place.Phone;
                p.Description = place.Description;
                p.RoadDescribe = place.RoadDescribe;
                p.Latitude = place.Latitude;
                p.Longitude = place.Longitude;
                if (placeManager.Update(p) > 0)
                    return RedirectToAction("Index", new { id = place.CityId });
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CityId = place.CityId;
            return View(place);
        }

        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = placeManager.Find(x => x.Id == id);
            ViewBag.CityId = place.CityId;
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = placeManager.Find(x => x.Id == id.Value);
            if (place == null)
            {
                return HttpNotFound();
            }
            if (placeManager.Delete(place) > 0)
            {
                return RedirectToAction("Index", new { id = place.CityId });
            }
            return View(place);
        }
    }
}