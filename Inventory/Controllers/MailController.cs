using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2_Project.Models;
using System.Net;
using System.Net.Mail;

namespace ATP2_Project.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(ATP2_Project.Models.DataViewModel.MailView model)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                MailMessage m = new MailMessage("sajjadurrahman3434@gmail.com", model.to);
                m.Subject = model.subject;
                m.Body = model.body;
                m.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential("sajjadurrahman3434@gmail.com", "Sajjad3434#");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = nc;
                smtp.Send(m);
                ViewBag.Message = "Sucessfully sent!";
                return View();
            }
            catch
            {
                ViewBag.message = "Invalid Mail Address!";
                return View();
            }

        }
    }
}

