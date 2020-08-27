using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2_Project.Models;

namespace ATP2_Project.Models.DataViewModel
{
    public class PurchaseViewModel
    {
        public Category category;
        public Purchase purchase;
        public List<Category> categories;
        public List<Information> suppliers;
    }
}