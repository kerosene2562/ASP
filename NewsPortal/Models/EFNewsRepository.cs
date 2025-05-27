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

        public void CreateNews(News news)
        {
            context.News.Add(news);
            context.SaveChanges();
        }

        public void UpdateNews(News news)
        {
            context.News.Update(news);
            context.SaveChanges();
        }

        public void DeleteNews(News news)
        {
            context.News.Remove(news);
            context.SaveChanges();
        }

        public void CreateCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }
}
