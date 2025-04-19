namespace NewsPortal.Models
{
    public interface INewsRepository
    {
        IQueryable<News> News { get; }
        IQueryable<Category> Categories { get; }
        void CreateNews(News news);
        void UpdateNews(News news);
        void DeleteNews(News news);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
