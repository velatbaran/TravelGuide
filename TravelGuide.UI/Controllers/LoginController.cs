using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TravelGuide.BL;
using TravelGuide.Entities.Entity;
using TravelGuide.UI.Helpers;
using TravelGuide.UI.Models;

namespace TravelGuide.UI.Controllers
{
    public class LoginController : Controller
    {
        private MemberManager memberManager = new MemberManager();
        string Message = "";
        bool hasError = false;

        [HttpPost]
        public JsonResult SignUp(string register_name, string register_surname, string register_username, string register_email, string register_password)
        {
            // sağındaki ve solundaki boşluklar varsa silinir
            register_name = register_name?.Trim();
            register_surname = register_surname?.Trim();
            register_email = register_email?.Trim();
            register_username = register_username?.Trim();
            register_password = register_password?.Trim();

            if (string.IsNullOrEmpty(register_name) == true || string.IsNullOrEmpty(register_surname) == true || string.IsNullOrEmpty(register_email) == true || string.IsNullOrEmpty(register_username) == true || string.IsNullOrEmpty(register_password) == true)
            {
                hasError = true;
                Message = "Lütfen tüm alanları doldurun.";
            }
            else
            {
                Member member = memberManager.Find(x => x.Username == register_username || x.Email == register_email);
                if (member != null)
                {
                    if (member.Email == register_email)
                    {
                        hasError = true;
                        Message = "Bu email adresi kullanılmaktadır.";
                    }
                    if (member.Username == register_username)
                    {
                        hasError = true;
                        Message = "Bu kullanıcı adı kullanılmaktadır.";
                    }
                }
                else
                {
                    Member m = new Member()
                    {
                        Name = register_name,
                        Surname = register_surname,
                        Username = register_username,
                        Email = register_email,
                        ProfileImage = "user_boy.png",
                        RoleId = 2,
                        Password = Crypto.Hash(register_password.ToString(), "MD5"),
                        RePassword = Crypto.Hash(register_password.ToString(), "MD5")
                    };
                    if (memberManager.Insert(m) > 0)
                    {
                        hasError = false;
                        Message = "Hesabınız başarılı bir şekilde oluşturuldu.";
                    }
                    else
                    {
                        hasError = true;
                        Message = "Kayıt yapılırken hata oluştu";
                    }
                }
            }

            return Json(new
            {
                hasError,
                Message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SignIn(string login_username, string login_password)
        {
            login_username = login_username?.Trim();
            login_password = login_password?.Trim();
            var pass = Crypto.Hash(login_password.ToString(), "MD5");

            if (string.IsNullOrEmpty(login_username) || string.IsNullOrEmpty(login_password))
            {
                hasError = true;
                Message = "Kullanıcı adı ya da şifre boş geçilemez.";
            }
            Member m = memberManager.Find(x => x.Username == login_username && x.Password == pass);
            if (m == null)
            {
                hasError = true;
                Message = "Kullanıcı adı ya da şifre hatalı girdiniz.";
            }
            else
            {
                if (m.IsDeleted == true || m.IsLock)
                {
                    if (m.IsDeleted)
                    {
                        hasError = true;
                        Message = "Bu kullanıcı silinmiş.";
                    }
                    if (m.IsLock)
                    {
                        hasError = true;
                        Message = "Bu kullanıcı kilitlenmiş.";
                    }
                }
                else
                {
                    hasError = false;
                    Message = "Giriş başarılı";
                }
                CurrentSession.Set<Member>("login", m);
            }

            return Json(new { hasError, Message });
        }

        public ActionResult SignOut()
        {
            CurrentSession.Clear();
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public JsonResult LostPassword(string lost_email)
        {
            Member m = memberManager.Find(x => x.Email == lost_email);
            if (m == null)
            {
                hasError = true;
                Message = "Bu e-mail adresi kayıtlı değil.";
            }
            else
            {
                Random rnd = new Random();
                string new_pass = rnd.Next().ToString();
                m.Password = Crypto.Hash(new_pass, "MD5");
                m.RePassword = Crypto.Hash(new_pass, "MD5");

                //string _email = "okyanusmavisi1990@hotmail.com";
                string body = $"<b>Yeni Şifreniz : </b>{new_pass}<br>";
                bool send = MailHelper.SendMail(body, lost_email, "Seyahat Rehberi - Yeni Şifre Talebi!");
                if (send == false)
                {
                    hasError = true;
                    Message = "Yeni şifre gönderilirken hata oluştu";
                }
                else
                {
                    memberManager.Update(m);
                    hasError = false;
                    Message = "Şifreniz başarılı bir şekilde gönderilmiştir.";
                }
            }

            return Json(new { hasError, Message }, JsonRequestBehavior.AllowGet);
        }
    }
}
