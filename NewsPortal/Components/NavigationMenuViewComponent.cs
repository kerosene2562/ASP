using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Models;
using NewsPortal.Models.ViewModel;

namespace NewsPortal.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly NewsPortalDBContext _context;
        public NavigationMenuViewComponent(NewsPortalDBContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
    }
}
