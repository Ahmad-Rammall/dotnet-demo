using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext db;
        public CategoryController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = db.Categories;
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj?.Name == obj?.DisplayOrder?.ToString())
            {
                ModelState.AddModelError("CustomError", "Name cannot be exactly the Display Order");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Category Added Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = db.Categories.Find(id);
            //var categoryFromDb = db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDb = db.Categories.SingleOrDefault(u => u.Id == id);

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj?.Name == obj?.DisplayOrder?.ToString())
            {
                ModelState.AddModelError("CustomError", "Name cannot be exactly the Display Order");
            }
            if (ModelState.IsValid)
            {
                db.Categories.Update(obj);
                db.SaveChanges();
                TempData["success"]="Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = db.Categories.Find(id);

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)
        {
            if (id == null)
                return NotFound();
            var obj = db.Categories.Find(id);

            if (obj == null)
                return NotFound();

            db.Categories.Remove(obj);
            db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");


        }
    }
}
