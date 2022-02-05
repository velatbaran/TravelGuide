using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelGuide.BL;
using TravelGuide.BL.Results;
using TravelGuide.Entities.Entity;
using TravelGuide.Entities.ValueObjects;
using TravelGuide.UI.Filters;
using TravelGuide.UI.Models;

namespace TravelGuide.UI.Controllers
{
    [ErrorExc]
    public class HomeController : Controller
    {
        private CityManager cityManager = new CityManager();
        private CountryManager countryManager = new CountryManager();
        private MemberManager memberManager = new MemberManager();
        public ActionResult Index()
        {
            return View(cityManager.List());
        }

        public ActionResult GetCitiesByCountryId(int? id)
        {
            return View("Index", cityManager.List(x => x.Country.Id == id.Value));
        }

        public ActionResult GetCitiesByUsername(int? id)
        {
            var username = cityManager.Find(x => x.Id == id.Value).CreatedUsername;
            return View("Index", cityManager.List(x => x.CreatedUsername == username));
        }

        public PartialViewResult GetAllCountry()
        {
            ViewBag.AllCityCount = cityManager.List().Count();
            return PartialView(countryManager.List());
        }

        [HttpPost]
        public ActionResult GetCityBySearchText(string search_text)
        {
            string _search_text = search_text.ToLower();
            List<City> list_search = cityManager.List(x => x.Name.ToLower().Contains(_search_text) ||
                                                x.OtherInformations.ToLower().Contains(_search_text) ||
                                                x.Foods.ToLower().Contains(_search_text) ||
                                                x.Slogan.ToLower().Contains(_search_text) ||
                                                x.DateInformation.ToLower().Contains(_search_text) ||
                                                x.WanderPlaces.ToLower().Contains(_search_text)).ToList();
            return View("Index", list_search);
        }

        [Auth]
        [HttpGet]
        public ActionResult ShowProfile()
        {
            return View(memberManager.Find(x => x.Id == CurrentSession.User.Id));
        }

        [Auth]
        [HttpGet]
        public ActionResult EditProfile()
        {
            return View(memberManager.Find(x => x.Id == CurrentSession.User.Id));
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(Member member, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("Password");
            ModelState.Remove("RePassword");
            if (ModelState.IsValid)
            {
                Member m = memberManager.Find(x => x.Id == CurrentSession.User.Id);
                if (ProfileImage != null)
                {
                    string filename = $"Image_{m.Username}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/images/members/{filename}"));
                    member.ProfileImage = filename;
                }

                BLResults<Member> res = memberManager.UpdateProfile(member);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(member);
                }

                CurrentSession.Set<Member>("login", res.Result);
                return RedirectToAction("ShowProfile");

            }
            return View(member);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
        public ActionResult Info()
        {
            return View();
        }
        public ActionResult HasError()
        {
            return View();
        }

        [Auth]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordValueObject model)
        {
            if (ModelState.IsValid)
            {
                BLResults<Member> result = memberManager.ChangePassword(model,CurrentSession.User.Id);
                if (result.Errors.Count > 0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                ViewBag.PassInfo = "Şifreniz başarılı bir şekilde değiştirilmiştir.";
                return View("ChangePassword");
            }
            return View(model);
        }

        [Auth]
        public ActionResult DeleteProfile()
        {
            BLResults<Member> res = memberManager.RemoveProfile(CurrentSession.User.Id);
            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View();
            }
            CurrentSession.Clear();
            return RedirectToAction("Index");
        }
    }
}