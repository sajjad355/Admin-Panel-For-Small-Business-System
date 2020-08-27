using ATP2_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATP2_Project.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard


        Inventory_ManagementEntities inv = new Inventory_ManagementEntities();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            string us = Session["user"].ToString();
            User a = inv.Users.Where(id => id.username.Equals(us)).FirstOrDefault();
            Information info = inv.Informations.Where(id => id.id == a.info_id).FirstOrDefault();

            return View(info);
        }
        [HttpGet]
        public ActionResult Edit(int? id = null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Information info = inv.Informations.Where(pid => pid.id == id).FirstOrDefault();

            return View(info);
        }
        [HttpPost]
        public ActionResult Save(Information u)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Information information = inv.Informations.Where(x => x.id == u.id).FirstOrDefault();

            information.Full_Name = u.Full_Name;
            information.Address = u.Address;
            information.Acc_Number = u.Acc_Number;
            information.Balance = u.Balance;
            information.Phone = u.Phone;
            if (ModelState.IsValid)
            {
                inv.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(u);
        }
        [HttpGet]
        public ActionResult changepass(int? id = null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            User u = inv.Users.Where(pid => pid.info_id == id).FirstOrDefault();
            return View(u);

        }
        [HttpPost]
        public ActionResult Confirm(User u)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            User information = inv.Users.Where(x => x.id == u.id).FirstOrDefault();

            information.password = u.password;

            if (ModelState.IsValid)
            {
                inv.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(u);
        }
    }
}