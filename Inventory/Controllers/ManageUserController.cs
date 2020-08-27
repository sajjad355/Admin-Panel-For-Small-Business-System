using ATP2_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATP2_Project.Controllers
{
    public class ManageUserController : Controller
    {
        Inventory_ManagementEntities inv = new Inventory_ManagementEntities();
        // GET: MangeUser
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Information[] info = inv.Informations.Where(pid => pid.usertype.Equals("user")).ToArray();
            return View(info.ToList());
        }
        [HttpGet]
        public ActionResult AddUser(int? id = null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                User u = new User();
                Information information = inv.Informations.Where(x => x.id == id).FirstOrDefault();
                u.username = information.Full_Name+information.id.ToString();
                u.info_id = information.id;
                u.type = information.Work_Position;
                u.password = information.Acc_Number;
                u.permission = "valid";

                return View(u);
            }
        }
        [HttpPost]
        public ActionResult Save(User u)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Information information = inv.Informations.Where(x => x.id == u.info_id).FirstOrDefault();
            information.usertype = u.type;
            inv.Users.Add(u);
            inv.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult InvokeUser()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(inv.Users.Where(pid => pid.permission.Equals("valid") && pid.type != ("Admin")).ToList());
        }
        [HttpGet]
        public ActionResult SelectUser(int? id = null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                User u = inv.Users.Where(pid => pid.id == id).FirstOrDefault();
                if (u.permission.Equals("valid"))
                {
                    u.permission = "invalid";
                    inv.SaveChanges();
                    return RedirectToAction("InvokeUser");
                }
                else
                {
                    u.permission = "valid";
                    inv.SaveChanges();
                    return RedirectToAction("RevokeUser");
                }


            }
        }
        [HttpGet]
        public ActionResult RevokeUser()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(inv.Users.Where(pid => pid.permission.Equals("invalid") && pid.type != ("Admin")).ToList());
        }

    }
}