using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2_Project.Models.DataViewModel;
using ATP2_Project.Models;
namespace ATP2_Project.Controllers
{
    public class ReportController : Controller
    {
        Inventory_ManagementEntities inv = new Inventory_ManagementEntities();
        ReportViewController reportView = new ReportViewController();
        // GET: Report
        public ActionResult Index()
        {
            reportView.purchases = new List<Purchase>();
            reportView.sales = new List<Sale>();
            reportView.suppliers = inv.Informations.Where(c => c.usertype == "supplier").ToList();
            reportView.categories = inv.Categories.ToList();
            return View(reportView);
        }

        [HttpPost]
        public ActionResult Index(DateTime? dateFrom = null, DateTime? dateTo = null, string reportFor = null, int? product_id = 0, int? catagory_id = 0)
        {
            ReportViewController reportView = new ReportViewController();
            if (reportFor == "sale")
            {
                if (product_id == 0 && catagory_id == 0)
                {
                    reportView.purchases = new List<Purchase>();
                    reportView.sales = inv.Sales.Where(c => c.date >= dateFrom && c.date <= dateTo).ToList();

                }
                else
                {
                    reportView.purchases = new List<Purchase>();
                    reportView.sales = inv.Sales.Where(c => c.product_id == product_id && c.category_id == catagory_id && (c.date >= dateFrom && c.date <= dateTo)).ToList();
                }
            }
            else if (reportFor == "purchase")
            {
                if (product_id == 0 && catagory_id == 0)
                {
                    reportView.sales = new List<Sale>();
                    reportView.purchases = inv.Purchases.Where(c => c.date >= dateFrom && c.date <= dateTo).ToList();

                }
                else
                {
                    reportView.sales = new List<Sale>();
                    reportView.purchases = inv.Purchases.Where(c => c.product_Id == product_id && c.catagory_id == catagory_id && (c.date >= dateFrom && c.date <= dateTo)).ToList();

                }
            }
            if (reportView.purchases.Count == 0 && reportView.sales.Count == 0)
            {
                ViewBag.Message = "No data Found !";
            }
            reportView.suppliers = inv.Informations.Where(c => c.usertype == "supplier").ToList();
            reportView.categories = inv.Categories.ToList();
            return View(reportView);
        }

        public ActionResult pDetails(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(inv.Purchases.Where(c => c.id == id).FirstOrDefault());
            }

        }
        public ActionResult sDetails(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(inv.Sales.Where(c => c.id == id).FirstOrDefault());
            }
        }
    }
}