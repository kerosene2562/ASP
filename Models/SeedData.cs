using Microsoft.EntityFrameworkCore;

namespace NewsPortal.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            NewsPortalDBContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<NewsPortalDBContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.News.Any())
            {
                context.News.AddRange(
                new News
                {
                    Title = "News1",
                    Text = "text of a news1",
                    PublicationDate = DateTime.Now,
                    ImgDirUrl = "Imgs/img1.png",
                    Category = "politic"
                },
                new News
                {
                    Title = "News2",
                    Text = "text of a news2",
                    PublicationDate = DateTime.Now,
                    ImgDirUrl = "Imgs/img2.png",
                    Category = "gaming"
                },
                new News
                {
                    Title = "News3",
                    Text = "text of a news3",
                    PublicationDate = DateTime.Now,
                    ImgDirUrl = "Imgs/img3.png",
                    Category = "sport"
                });
                context.SaveChanges();
            }
        }

    }
}
