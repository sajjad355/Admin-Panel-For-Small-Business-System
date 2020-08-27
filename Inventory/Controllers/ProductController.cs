using ATP2_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATP2_Project.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Inventory_ManagementEntities inv = new Inventory_ManagementEntities();
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(inv.Products.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Category[] categories = inv.Categories.ToArray();
            ViewData["categories"] = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                inv.Products.Add(p);
                inv.SaveChanges();
                return RedirectToAction("Index");
            }
            Category[] categories = inv.Categories.ToArray();
            ViewData["categories"] = categories;

            return View(p);
            
        }

        [HttpGet]
        public ActionResult Edit(int? id=null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id==null)
            {
                return RedirectToAction("Index");
            }
            

            Product p = inv.Products.Where(x => x.id == id).FirstOrDefault();
            
            Category[] categories = inv.Categories.ToArray();
            ViewData["categories"] = categories;
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(Product p, int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Product productToUpdate = inv.Products.Where(x => x.id == id).FirstOrDefault();
            productToUpdate.id = id;
            productToUpdate.name = p.name;
            productToUpdate.price = p.price;
            productToUpdate.quantity = p.quantity;
            productToUpdate.Category.Catagory_Name= p.Category.Catagory_Name;
            inv.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id=null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Product p = inv.Products.Where(x => x.id == id).FirstOrDefault();
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int? id=null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Product p = inv.Products.Where(x => x.id == id).FirstOrDefault();
            inv.Products.Remove(p);
            inv.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int? id=null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            Product p = inv.Products.Where(x => x.id == id).FirstOrDefault();
            return View(p);
        }
    }
}