using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Models;
using NewsPortal.Models.ViewModel;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        INewsRepository repository;
        public HomeController(INewsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int itemsPerPage = 1;
            int totalPages = await this.repository.News.CountAsync();
            var items = await repository.News
                .OrderBy(n => n.NewsID)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
            var model = new PagedList<News>(items, totalPages, page, itemsPerPage);
            return View(model);
        }
    }
}