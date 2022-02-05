using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelGuide.BL;
using TravelGuide.Entities.Entity;
using TravelGuide.UI.Filters;
using TravelGuide.UI.Models;

namespace TravelGuide.UI.Controllers
{
    [ErrorExc]
    public class CityController : Controller
    {
        private CityManager cityManager = new CityManager();
        private CountryManager countryManager = new CountryManager();
        private CommentManager commentManager = new CommentManager();

        [Auth]
        [AuthAdmin]
        public ActionResult Index()
        {
            var list_city = cityManager.ListIQueryable().Include("Country").Where(x => x.IsDeleted == false).ToList();
            return View(list_city);
        }

        [Auth]
        [AuthAdmin]
        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> list_country = (from x in countryManager.List()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.Id.ToString()
                                                 }).ToList();
            ViewBag.ListCountry = list_country;
            return View();
        }

        [Auth]
        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(City city, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string filename = $"Image_{city.Name}.{Image.ContentType.Split('/')[1]}";
                Image.SaveAs(Server.MapPath($"~/images/medias/{filename}"));
                city.Image = filename;

                if (cityManager.Insert(city) > 0)
                    return RedirectToAction("Index");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(city);
        }

        [Auth]
        [AuthAdmin]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = cityManager.Find(x => x.Id == id.Value);
            if (city == null)
            {
                return HttpNotFound();
            }

            ViewBag.ListCountry = new SelectList(countryManager.List(), "Id", "Name", city.CountryId);
            return View(city);
        }

        [Auth]
        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(City city, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                City c = cityManager.Find(x => x.Id == city.Id);

                if (Image != null)
                {
                    string filename = $"Image_{city.Name}.{Image.ContentType.Split('/')[1]}";
                    Image.SaveAs(Server.MapPath($"~/images/medias/{filename}"));
                    c.Image = filename;
                }

                c.Name = city.Name;
                c.CountryId = city.CountryId;
                c.Slogan = city.Slogan;
                c.DateInformation = city.DateInformation;
                c.Foods = city.Foods;
                c.OtherInformations = city.OtherInformations;
                c.WanderPlaces = city.WanderPlaces;
                if (cityManager.Update(c) > 0)
                    return RedirectToAction("Index");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ListCountry = new SelectList(countryManager.List(), "Id", "Name", city.CountryId);
            return View(city);
        }

        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = cityManager.Find(x => x.Id == id.Value);
            if (city == null)
            {
                return HttpNotFound();
            }

            return View(city);
        }

        [Auth]
        [AuthAdmin]
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = cityManager.Find(x => x.Id == id.Value);
            if (city == null)
            {
                return HttpNotFound();
            }
            if (cityManager.Delete(city) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(city);
        }

        [Auth]
        [HttpPost]
        public ActionResult AddComment(int? id, string commenttext)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment()
                {
                    CityId = id.Value,
                    CommentText = commenttext,
                    Owner = CurrentSession.User,
                    CommentDate = DateTime.Now
                };

                if (commentManager.Insert(comment) > 0)
                    return Redirect($"/City/Detail/{ id}#comments");
            }

            return View($"/City/Detail/{ id}");
        }
    }
}