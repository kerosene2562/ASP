using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Models;
using NewsPortal.Models.ViewModel;
using NewsPortal.Infrastructure;

namespace NewsPortal.Controllers
{
    public class HomeController : Controller
    {
        INewsRepository repository;
        public HomeController(INewsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index(int page = 1, int categoryId = 0)
        {
            int itemsPerPage = 1;
            int totalPages = await this.repository.News.CountAsync();
            var categories = await repository.Categories.ToListAsync();

            IQueryable<News> newsQuery = repository.News;
            newsQuery = newsQuery.Where(n => n.CategoryID == categoryId);
            int totalItems = await newsQuery.CountAsync();

            var items = await newsQuery
                .OrderBy(n => n.NewsID)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            var model = new PagedList<News>(items, totalItems, page, itemsPerPage)
            {
                Categories = categories,
                SelectedCategoryId = categoryId
            };

            return View(model);
        }
        public IActionResult SaveViewedNews(int newsId, string newsTitle)
        {
            var viewedNews = HttpContext.Session.GetJson<List<string>>("ViewedNews") ?? new List<string>();

            if (!viewedNews.Contains(newsTitle))
            {
                viewedNews.Add(newsTitle);
                HttpContext.Session.SetJson("ViewedNews", viewedNews);
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult GetViewedNews()
        {
            var viewedNews = HttpContext.Session.GetJson<List<string>>("ViewedNews") ?? new List<string>();
            return Content($"Переглянуті новини: {string.Join(", ", viewedNews)}");
        }
    }
}