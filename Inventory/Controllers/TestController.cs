using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2_Project.Models.DataViewModel;
using ATP2_Project.Models;

namespace ATP2_Project.Controllers
{
    public class TestController : Controller
    {
        Inventory_ManagementEntities inm = new Inventory_ManagementEntities();
        // GET: Test
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            CategoryViewModel viewModel= new CategoryViewModel();
            viewModel.categories = inm.Categories.ToList();
            return View(viewModel);
        }

        public ActionResult List()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            CategoryViewModel viewModel = new CategoryViewModel();
            viewModel.categories = inm.Categories.ToList();
            return View(viewModel);
        }
    }
}