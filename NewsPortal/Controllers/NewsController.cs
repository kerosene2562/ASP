using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsPortal.Models;

namespace NewsPortal.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository repository;

        public NewsController(INewsRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index()
        {
            var news = repository.News.ToList();
            return View(news);
        }

        public IActionResult Details(int id)
        {
            var newsItem = repository.News.FirstOrDefault(n => n.NewsID == id);
            if (newsItem == null) return NotFound();
            return View(newsItem);
        }

        public IActionResult Create()
        {
            var categories = repository.Categories.ToList();
            if (categories == null || !categories.Any())
            {
                ModelState.AddModelError("", "Немає доступних категорій. Додайте категорії перед створенням новини.");
            }

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(News newsItem)
        {
            if (ModelState.IsValid)
            {
                repository.CreateNews(newsItem);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = repository.Categories.ToList();
            return View(newsItem);
        }

        public IActionResult Edit(int id)
        {
            var news = repository.News.FirstOrDefault(n => n.NewsID == id);
            if (news == null) return NotFound();

            ViewBag.Categories = repository.Categories.ToList();

            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(News newsItem)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateNews(newsItem);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = repository.Categories.ToList();
            return View(newsItem);
        }

        public IActionResult Delete(int id)
        {
            var newsItem = repository.News.FirstOrDefault(n => n.NewsID == id);
            if (newsItem == null) return NotFound();
            return View(newsItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var newsItem = repository.News.FirstOrDefault(n => n.NewsID == id);
            if (newsItem != null)
            {
                repository.DeleteNews(newsItem);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
