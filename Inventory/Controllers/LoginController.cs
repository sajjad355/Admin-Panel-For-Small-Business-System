
using ATP2_Project.Models;
using ATP2_Project.Models.DataViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATP2_Project.Controllers
{
    public class LoginController : Controller
    {
        Inventory_ManagementEntities entities = new Inventory_ManagementEntities();
        // GET: Login
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(UserViewModel user)
        {


            User a = entities.Users.Where(name => name.username.Equals(user.username)).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (a == null)
                {
                    ViewData["Message"] = "Invalid_User!";
                    return View(user);
                }

                else
                {
                    if (a.permission.Equals("invalid"))
                    {
                        ViewData["Message"] = "Your are invoked.Contact with admin!!";
                        return View(user);
                    }
                    else if (a.password.Equals(user.password))
                    {
                        Session["user"] = user.username;
                        User us = entities.Users.Where(id => id.username.Equals(user.username)).FirstOrDefault();
                        Session["type"] =us.type;


                        return RedirectToAction("Index", "DashBoard");

                    }

                    else
                    {
                        ViewData["Message"] = "Invalid_Credentials!!";
                        return View(user);
                    }
                }

            }
            else
            {

                return View(user);
            }

        }
    }
}