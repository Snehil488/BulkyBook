using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;

        //Can get Applicationdbcontext object through dependency injection because registered service in Program.cs
        public CategoryController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = db.Categories;
            return View(categoryList);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        public IActionResult Create(Category category)
        {
            //Add custom model validation
            //Checking if name and display order are same
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and Display order can not be same");
            }

            if(ModelState.IsValid) 
            { 
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
