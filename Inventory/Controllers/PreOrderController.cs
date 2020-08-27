using ATP2_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATP2_Project.Controllers
{
    public class PreOrderController : Controller
    {
        Inventory_ManagementEntities inv = new Inventory_ManagementEntities();
        // GET: PreOrder
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Session["mail"] = null;
            return View(inv.Informations.Where(pid => pid.usertype.Equals("saler") || pid.usertype.Equals("supplier")).ToList());
        }
        [HttpGet]
        public ActionResult mail(int? id = null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id==null)
            {
                return RedirectToAction("Index");
            }
            Information info = inv.Informations.Where(pid => pid.id == id).FirstOrDefault();
            Session["mail"] = info.Address;
            return RedirectToAction("Index", "Mail");
        }
    }
}