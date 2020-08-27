using ATP2_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATP2_Project.Controllers
{
    public class RegistrationController : Controller
    {
        
    Inventory_ManagementEntities entities = new Inventory_ManagementEntities();
        // GET: Registration
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Firstname = "User";
            return View();
        }
        [HttpPost]
        public ActionResult Index(Information infomation)
        {
            if (ModelState.IsValid)
            {
                entities.Informations.Add(infomation);
                entities.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
            return View(infomation);
        }


    }
}