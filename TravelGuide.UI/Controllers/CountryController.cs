
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
    public class CountryController : Controller
    {
        private CountryManager countryManager = new CountryManager();

        public ActionResult Index()
        {
            return View(countryManager.ListIQueryable().Where(x=>x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).ToList());
        }

        [HttpPost]
        public JsonResult Create(Country country)
        {
            try
            {
                if (string.IsNullOrEmpty(country.Name) == true)
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (countryManager.Insert(country) > 0)
                        return Json(new { hasError = false, Message = "Kayıt işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { hasError = true, Message = "Kayıt yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Kayıt yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                var result = countryManager.List(x => x.Id == id.Value).Select(x => new { x.Id, x.Name }).FirstOrDefault();
                if (result == null)
                    return Json(new { hasError = true, Message = "Kayıt bulunamadı." }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { result, hasError = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Kayıt bulunamadı." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Edit(Country country)
        {
            try
            {
                if (string.IsNullOrEmpty(country.Name) == true)
                {
                    return Json(new { hasError = true, Message = "Lütfen gerekli alanları doldurun." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Country c = countryManager.Find(x => x.Id == country.Id);
                    c.Name = country.Name;
                    if (countryManager.Update(c) > 0)
                        return Json(new { hasError = false, Message = "Güncelleme işlemi başarıyla gerçekleşti." }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { hasError = true, Message = "Güncelleme yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Güncelleme yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return Json(new { hasError = true, Message = "ID bilgisi null değer dönderdi." }, JsonRequestBehavior.AllowGet);
                }
                Country c = countryManager.Find(x => x.Id == id.Value);
                if (c == null)
                {
                    return Json(new { hasError = true, Message = "Kayıt bulunamadı." }, JsonRequestBehavior.AllowGet);
                }
                if (countryManager.Delete(c) > 0)
                    return Json(new { hasError = false, Message = $"{c.Name} kaydı başarılı bir şekilde silindi." }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { hasError = true, Message = "Silme işlemi yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { hasError = true, Message = "Silme işlemi yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = countryManager.Find(x => x.Id == id.Value);
            if (country == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialCountryDetail", country);
        }
    }
}