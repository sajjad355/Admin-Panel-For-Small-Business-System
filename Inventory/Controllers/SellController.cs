using ATP2_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATP2_Project.Controllers
{
    public class SellController : Controller
    {
        Inventory_ManagementEntities inv = new Inventory_ManagementEntities();
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Information[] info = inv.Informations.Where(pid => pid.usertype.Equals("Saler")).ToArray();
            ViewData["information"] = info ;
            return View();
        }
        public ActionResult gotolist(FormCollection form)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int a = Convert.ToInt32(form["info_id"]);
            Information info = inv.Informations.Where(pid => pid.id == a).FirstOrDefault();
            Session["saler"] = info.Full_Name;
            Session["Info_Id"] = form["info_id"];
            return RedirectToAction("List");
        }
         
      

        // GET: Sell
        [HttpGet]
        public ActionResult List()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (Session["saler"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
               


              //  Information info = inv.Informations.Where(pid => pid.id == id).FirstOrDefault();
              //  TempData["saler"] = info.Full_Name;

                return View(inv.Products.Where(q=>q.quantity>0).ToList());
            }

        }
        [HttpGet]
        // GET: Sell
        public ActionResult Sell(int? id = null)
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
               
                Product p = inv.Products.Where(x => x.id == id).FirstOrDefault();
                
                Sale s = new Sale();
                /*
                Sale s2;
               DateTime d= inv.Sales.Where(x => x.product_id == id ).Max(x => x.date);
                s2 = inv.Sales.Where(x => x.date.Equals(d)).FirstOrDefault();
                s.pre_unit_price = s2.new_unit_price;
                */
                var pup = inv.Sales.Where(c => c.product_id == id).FirstOrDefault();
                s.quantity = p.quantity;
                s.new_unit_price = p.price;
                if (pup!=null)
                    s.pre_unit_price = pup.pre_unit_price;
                else
                    s.pre_unit_price = 0;
                s.product_id= p.id;
                Session["pid"] = p.id;
                s.category_id = p.category_id;
 
               
                Category[] categories = inv.Categories.ToArray();
                ViewData["categories"] = categories;

                Product[] product = inv.Products.ToArray();
                ViewData["product"] = product;

                Information[] information = inv.Informations.ToArray();
                ViewData["informations"] = information;

                return View(s);
            }

        }

        [HttpPost]
        public ActionResult Sell(Sale s,int id,FormCollection form)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            inv.Sales.Add(s);
           

            Product productToUpdate = inv.Products.Where(x => x.id == id).FirstOrDefault();

            productToUpdate.quantity -= s.quantity;


             if(ModelState.IsValid)
                {
                    inv.SaveChanges();
                     TempData["sale"] = s;
                    return RedirectToAction("Details");
                }
      
            else
            {

                Sale s1 = new Sale();
                string a = Session["pid"].ToString();
                int b = Convert.ToInt32(a);
                s1.product_id = b;
                s1.quantity = s.quantity;
                s1.new_unit_price = s.new_unit_price;
                s1.category_id = s.category_id;


                Category[] categories = inv.Categories.ToArray();
                ViewData["categories"] = categories;

                Product[] pro = inv.Products.ToArray();
                ViewBag.product = pro;

                Information[] information = inv.Informations.ToArray();
                ViewData["informations"] = information;

                return View(s1);
            }

        }
        [HttpGet]


        public ActionResult SellRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SellRegistration(Information infomation)
        {
            if (ModelState.IsValid)
            {
                inv.Informations.Add(infomation);
                inv.SaveChanges();
                return RedirectToAction("Index", "Sell");
            }
            return View(infomation);
        }
        [HttpPost]
        public ActionResult CheckInvoiceNo(string invoice)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var purchase = inv.Sales.Where(c => c.invoice == invoice).FirstOrDefault();
            if (purchase == null)
            {
                return Json(new { invo = "" });
            }
            else
            {
                return Json(new { invo = purchase.invoice });
            }
        }
        [HttpGet]
        public ActionResult Details()
        {
            return View(TempData["sale"]);
        }
       

    }
}