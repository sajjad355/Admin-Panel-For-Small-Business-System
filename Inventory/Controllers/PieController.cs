using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2_Project.Models;
using ATP2_Project.Models.DataViewModel;

namespace ATP2_Project.Controllers
{
    public class PieController : Controller
    {
        Inventory_ManagementEntities inv = new Inventory_ManagementEntities();
        PieViewModel viewModel = new PieViewModel();
        // GET: Pie
        public ActionResult Index()
        {
            viewModel.purchase = "50";
            viewModel.sales = "50";
            return View(viewModel);
        }

        [HttpPost]
        // GET: Pie
        public ActionResult Index(DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            List<Purchase> purchases = inv.Purchases.Where(c => c.date >= dateFrom && c.date <= dateTo).ToList();
            List<Sale> sales = inv.Sales.Where(c => c.date >= dateFrom && c.date <= dateTo).ToList();
            double purchaseSum = 0;
            double salesSum = 0;
            foreach (var item in purchases)
            {
                purchaseSum += item.total_price;
            }
            foreach (var item in sales)
            {
                salesSum += item.total_price;
            }
            viewModel.purchase = purchaseSum.ToString();
            viewModel.sales = salesSum.ToString();
            return View(viewModel);
        }
    }
}