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
    public class RoadDescribeUnitController : Controller
    {
        private RoadDescribeUnitManager roadDescribeUnitManager = new RoadDescribeUnitManager();
        private PlaceManager placeManager = new PlaceManager();
        public ActionResult Index()
        {
            return View(roadDescribeUnitManager.List(x => x.IsDeleted == false).OrderByDescending(x=>x.CreatedOn));
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> list_places = (from k in placeManager.List()
                                                select new SelectListItem
                                                {
                                                    Text = k.Name,
                                                    Value = k.Id.ToString()
                                                }).ToList();
            ViewBag.ListPlaces = list_places;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoadDescribeUnit roadDescribeUnit)
        {
            if (ModelState.IsValid)
            {
                if (roadDescribeUnitManager.Insert(roadDescribeUnit) > 0)
                    return RedirectToAction("Index");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(roadDescribeUnit);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoadDescribeUnit roadDescribeUnit = roadDescribeUnitManager.Find(x => x.Id == id.Value);
            if (roadDescribeUnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListPlacess = new SelectList(placeManager.List(), "Id", "Name", roadDescribeUnit.PlaceId);
            return View(roadDescribeUnit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoadDescribeUnit roadDescribeUnit)
        {
            if (ModelState.IsValid)
            {
                RoadDescribeUnit r = roadDescribeUnitManager.Find(x => x.Id == roadDescribeUnit.Id);
                r.PlaceId = roadDescribeUnit.PlaceId;
                r.Detail = roadDescribeUnit.Detail;
                if (roadDescribeUnitManager.Update(r) > 0)
                    return RedirectToAction("Index");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ListPlaces = new SelectList(placeManager.List(), "Id", "Name", roadDescribeUnit.PlaceId);
            return View(roadDescribeUnit);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoadDescribeUnit roadDescribeUnit = roadDescribeUnitManager.Find(x => x.Id == id.Value);
            if (roadDescribeUnit == null)
            {
                return HttpNotFound();
            }
            if (roadDescribeUnitManager.Delete(roadDescribeUnit) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(roadDescribeUnit);
        }

        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoadDescribeUnit roadDescribeUnit = roadDescribeUnitManager.Find(x => x.Id == id.Value);
            if (roadDescribeUnit == null)
            {
                return HttpNotFound();
            }
            return View(roadDescribeUnit);
        }
    }
}