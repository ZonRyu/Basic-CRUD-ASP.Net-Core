using Learn_Web_App.Data;
using Learn_Web_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learn_Web_App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;

        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.categories;
            return View(objCategoryList);
        }
        
        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

		// GET
		public IActionResult Edit(int? id)
		{
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.categories.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

			return View(categoryFromDb);
		}

		// POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
			}
			if (ModelState.IsValid)
			{
				_db.categories.Update(obj);
				_db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
			}
			return View(obj);
		}

		public IActionResult Delete(int? id)
		{
			var obj = _db.categories.Find(id);
            if (obj == null)
            {
                NotFound();
            }

			_db.categories.Remove(obj);
			_db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
		}
	}
}
