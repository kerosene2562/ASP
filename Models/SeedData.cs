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
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category { Name = "sport" },
                        new Category { Name = "politics" },
                        new Category { Name = "games" }
                    );

                    context.SaveChanges(); 
                }

                var category1 = context.Categories.FirstOrDefault(c => c.Name == "sport");
                var category2 = context.Categories.FirstOrDefault(c => c.Name == "politics");
                var category3 = context.Categories.FirstOrDefault(c => c.Name == "games");

                context.News.AddRange(
                    new News
                    {
                        Title = "News1",
                        Text = "text of a news1",
                        PublicationDate = DateTime.Now,
                        ImgDirUrl = "Imgs/img1.png",
                        CategoryID = category1.Id, 
                        //Category = category1
                    },
                    new News
                    {
                        Title = "News2",
                        Text = "text of a news2",
                        PublicationDate = DateTime.Now,
                        ImgDirUrl = "Imgs/img2.png",
                        CategoryID = category2.Id,
                        //Category = category2
                    },
                    new News
                    {
                        Title = "News3",
                        Text = "text of a news3",
                        PublicationDate = DateTime.Now,
                        ImgDirUrl = "Imgs/img3.png",
                        CategoryID = category3.Id,
                        //Category = category3
                    }
                );

                context.SaveChanges(); 
            }
        }
    }
}
