using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models;

namespace NewsPortal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly INewsRepository repository;

        public CategoryController(INewsRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index()
        {
            var categories = repository.Categories.ToList();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = repository.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            foreach (var modelStateKey in ModelState.Keys)
            {
                var value = ModelState[modelStateKey];
                foreach (var error in value.Errors)
                {
                    Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                }
            }

            if (repository.Categories.Any(c => c.Name == category.Name))
            {
                ModelState.AddModelError("Name", "Категорія з таким іменем вже існує");
            }
            if (ModelState.IsValid)
            {
                repository.CreateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = repository.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (repository.Categories.Any(c => c.Name == category.Name && c.Id != category.Id))
            {
                ModelState.AddModelError("Name", "Така категорія вже є");
            }
            if (ModelState.IsValid)
            {
                repository.UpdateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = repository.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = repository.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                repository.DeleteCategory(category);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
