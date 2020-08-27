using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATP2_Project.Models;
using ATP2_Project.Models.DataViewModel;

namespace ATP2_Project.Controllers
{
    public class CategoryController : Controller
    {
        Inventory_ManagementEntities inm = new Inventory_ManagementEntities();
        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            if(Session["user"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            CategoryViewModel viewModel = new CategoryViewModel();
            viewModel.categories = inm.Categories.ToList();
            //return View();
            return View(viewModel);
        }
        
        [HttpGet]
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
        [HttpPost]
        public ActionResult Index(string name)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Category category = new Category();
            category.Catagory_Name = name;
                inm.Categories.Add(category);
                inm.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int? id = null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id==null)
            {
                return RedirectToAction("List");
            }

            Category CategoryToUpdate = inm.Categories.Where(x => x.id == id).FirstOrDefault();
            
            return View(CategoryToUpdate);
        }
       
        [HttpPost]
        public ActionResult Edit(Category c,int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                Category CategoryToUpdate = inm.Categories.Where(x => x.id == id).FirstOrDefault();
                CategoryToUpdate.id = id;
                CategoryToUpdate.Catagory_Name = c.Catagory_Name;
              

                inm.SaveChanges();
                return RedirectToAction("List");
            }
            return View(c);
            
        }

        [HttpGet]
        public ActionResult Delete(int? id = null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction("List");
            }
            Category CategoryToUpdate = inm.Categories.Where(x => x.id == id).FirstOrDefault();
            return View(CategoryToUpdate);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(int? id = null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return RedirectToAction("List");
            }
            Category CategoryToUpdate = inm.Categories.Where(x => x.id == id).FirstOrDefault();
            inm.Categories.Remove(CategoryToUpdate);
            inm.SaveChanges();

            return RedirectToAction("List");
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
                return RedirectToAction("List");
            }
            Category CategoryToUpdate = inm.Categories.Where(x => x.id == id).FirstOrDefault();
            return View(CategoryToUpdate);
        }


    }
}