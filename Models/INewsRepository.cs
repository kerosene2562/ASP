namespace NewsPortal.Models
{
    public interface INewsRepository
    {
        IQueryable<News> News { get; }
        IQueryable<Category> Categories { get; }
    }
}
