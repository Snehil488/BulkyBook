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
                TempData["success"] = "Added successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var category = db.Categories.Find(id);

            if(category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //Post
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            //Add custom model validation
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Name and Display order can not be same");
            }

            if (ModelState.IsValid)
            {
                db.Categories.Update(category);
                db.SaveChanges();
                TempData["success"] = "Updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = db.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //Post
        //Since same nameand signature need to explicitly give name
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var obj = db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            db.Categories.Remove(obj);
            db.SaveChanges();
            TempData["success"] = "Deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
