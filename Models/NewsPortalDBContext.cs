using Microsoft.EntityFrameworkCore;

namespace NewsPortal.Models
{
    public class NewsPortalDBContext : DbContext
    {
        public NewsPortalDBContext(DbContextOptions<NewsPortalDBContext> options) : base(options) { }
        public DbSet<News> News => Set<News>();
        public DbSet<Category> Categories => Set<Category>();
    }
}