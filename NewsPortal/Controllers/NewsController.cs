using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using NewsPortal.Models;

namespace NewsPortal.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository repository;

        private readonly IHubContext<NewsHub> _hubContext;

        public NewsController(INewsRepository repo, IHubContext<NewsHub> hubContext)
        {
            repository = repo;
            _hubContext = hubContext;
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

        [HttpPost]
        public async Task<IActionResult> React(int id)
        {
            var news = repository.News.FirstOrDefault(n => n.NewsID == id);
            if (news == null) return NotFound();

            news.ReactionCount++;
            repository.UpdateNews(news);

            await _hubContext.Clients.All.SendAsync("ReceiveReaction", news.NewsID, news.ReactionCount);

            return Ok();
        }
    }
}
