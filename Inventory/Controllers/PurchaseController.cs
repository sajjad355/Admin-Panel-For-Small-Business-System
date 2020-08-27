using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2_Project.Models.DataViewModel;
using ATP2_Project.Models;

namespace ATP2_Project.Controllers
{
    public class PurchaseController : Controller
    {
        Inventory_ManagementEntities inv = new Inventory_ManagementEntities();

        // GET: Purchase
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            PurchaseViewModel purchaseView = new PurchaseViewModel();
            purchaseView.suppliers = inv.Informations.Where(c => c.usertype == "supplier").ToList();
            purchaseView.categories = inv.Categories.ToList();
            return View(purchaseView);
        }

        [HttpPost]
        public ActionResult Index(Purchase purchase, int info_id, int catagory_id, int product_id, string pre_unit_Price)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                purchase.info_id = Convert.ToInt32(info_id);
                purchase.catagory_id = Convert.ToInt32(catagory_id);
                purchase.product_Id = Convert.ToInt32(product_id);
                purchase.pre_unit_price = Convert.ToInt32(pre_unit_Price);
                purchase.remarks = "new";

                if (pre_unit_Price != null)
                    purchase.pre_unit_price = Convert.ToInt32(pre_unit_Price);

                inv.Purchases.Add(purchase);
                inv.SaveChanges();
                Product product = inv.Products.Where(x => x.id == purchase.product_Id).FirstOrDefault();
                product.quantity += purchase.quantity;
                inv.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult List()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            PurchaseViewModel purchaseView = new PurchaseViewModel();


            return View(purchaseView);
        }
        [HttpPost]
        public ActionResult List(Purchase purchase)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Purchase pur = purchase;
            return View();
        }
        [HttpPost]
        public ActionResult CheckCategory(int value)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var productList = inv.Products.Where(c => c.category_id == value).ToList();
            var products = from p in productList select (new { p.id, p.name });
            return Json(products, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CheckQuantity(int value)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var product = inv.Products.Where(c => c.id == value).ToList();
            var pup = inv.Purchases.Where(c => c.product_Id == value).FirstOrDefault();
            if (pup == null)
            {
                var pre_unit_price = 0;
                var productss = from p in product select (new { p.id, p.quantity, pre_unit_price });
                return Json(productss, JsonRequestBehavior.AllowGet);
            }
            var products = from p in product select (new { p.id, p.quantity ,pup.pre_unit_price});
            return Json(products, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CheckInvoiceNo(string invoice)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var purchase = inv.Purchases.Where(c => c.invoice == invoice).FirstOrDefault();
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
        public ActionResult addsupplier()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
        [HttpPost]
        public ActionResult addsupplier(Information infomation)
        {
            if (ModelState.IsValid)
            {
                inv.Informations.Add(infomation);
                inv.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(infomation);
        }
    }
}