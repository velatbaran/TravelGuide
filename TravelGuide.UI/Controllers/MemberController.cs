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

namespace TravelGuide.UI.Controllers
{
    [ErrorExc]
    [Auth]
    [AuthAdmin]
    public class MemberController : Controller
    {
        private MemberManager memberManager = new MemberManager();
        private RoleManager roleManager = new RoleManager();
        public ActionResult Index()
        {
            return View(memberManager.ListIQueryable().Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                BLResults<Member> result = memberManager.InsertMember(member);
                if (result.Errors.Count > 0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(member);
                }
                return RedirectToAction("Index");
            }
            return View(member);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLResults<Member> result = memberManager.GetMemberById(id.Value);
            if (result.Errors.Count > 0)
            {
                result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
            }
            return View(result.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
        {
            ModelState.Remove("Password");
            ModelState.Remove("RePassword");
            if (ModelState.IsValid)
            {
                BLResults<Member> result = memberManager.UpdateMember(member);
                if (result.Errors.Count > 0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(member);
                }
                return RedirectToAction("Index");
            }

            return View(member);
        }

        [HttpGet]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLResults<Member> result = memberManager.GetMemberById(id.Value);
            if (result.Errors.Count > 0)
            {
                result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
            }
            return View(result.Result);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLResults<Member> result = memberManager.DeleteMember(id.Value);
            if (result.Errors.Count > 0)
            {
                result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View(result.Result);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangeRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BLResults<Member> result = memberManager.GetMemberById(id.Value);
            if (result.Errors.Count > 0)
            {
                result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View(result.Result);
            }
            var list_role = new SelectList(roleManager.List(), "Id", "Name", result.Result.RoleId);
            ViewBag.ListRole = list_role;
            ViewBag.MemberId = result.Result.Id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeRole(ChangeRoleValueObject model)
        {
            if (ModelState.IsValid)
            {
                BLResults<Member> result = memberManager.ChangeRole(model);
                if (result.Errors.Count > 0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult GetLocked(int[] ids)
        {
            List<int> lockeds = memberManager.List(x => x.IsLock == true && ids.Contains(x.Id)).Select(x => x.Id).ToList();
            return Json(new { result = lockeds }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetLocked(int id, bool locked)
        {

            Member member = memberManager.Find(x => x.Id == id);
            if (member == null)
            {
                return Json(new { hasError = true, Message = "Üye bulunamadı" }, JsonRequestBehavior.AllowGet);
            }

            member.IsLock = locked;
            if (memberManager.Update(member) > 0)
            {
                return Json(new { hasError = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { hasError = true, Message = "Üye kilitleme işlemi yapılırken hata oluştu." }, JsonRequestBehavior.AllowGet);
        }
    }
}