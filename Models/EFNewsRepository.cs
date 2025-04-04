using static NewsPortal.Models.NewsPortalDBContext;

namespace NewsPortal.Models
{
    public class EFNewsRepository : INewsRepository
    {
        private NewsPortalDBContext context;
        public EFNewsRepository(NewsPortalDBContext context)
        {
            this.context = context;
        }
        public IQueryable<News> News => context.News;
        public IQueryable<Category> Categories => context.Categories;
    }
}
