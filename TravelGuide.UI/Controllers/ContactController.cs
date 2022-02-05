using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelGuide.BL;
using TravelGuide.Entities.Entity;
using TravelGuide.UI.Filters;
using TravelGuide.UI.Helpers;

namespace TravelGuide.UI.Controllers
{
    public class ContactController : Controller
    {
        private ContactManager contactManager = new ContactManager();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PartialContactInfo()
        {
            return PartialView(contactManager.List());
        }

        [HttpPost]
        public ActionResult SendMessage(string name, string email, string phone, string message)
        {
            string _email = "okyanusmavisi1990@hotmail.com";
            string body = $"<b>İsim-Soyisim : </b>{name}<br>" +
                          $"<b>E-Posta : </b>{email}<br>" +
                          $"<b>Telefon : </b>{phone}<br>" +
                          $"<b>Mesaj : </b>{message}";

            MailHelper.SendMail(body, _email, "Seyahat Rehberi - Mesajınız Var!");

            ViewBag.Result = "Mesajınız gönderilmiştir.";

            return View("Index");
        }
        
        [ErrorExc]
        [Auth]
        [AuthAdmin]
        [HttpGet]
        public ActionResult GetContact()
        {
            return View(contactManager.Find(x => x.Id == 1));
        }

        [ErrorExc]
        [Auth]
        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                Contact c = contactManager.Find(x => x.Id == contact.Id);
                c.Address = contact.Address;
                c.Email = contact.Email;
                c.Phone = contact.Phone;
                c.Twitter = contact.Twitter;
                c.Telegram = contact.Telegram;
                c.Github = contact.Github;
                c.Instagram = contact.Instagram;
                if (contactManager.Update(c) > 0)
                    return RedirectToAction("GetContact");
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(contact);
        }
    }
}