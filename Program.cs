using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using NewsPortal.Models;
using static NewsPortal.Models.NewsPortalDBContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NewsPortalDBContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:NewsPortalConnection"]);
});
builder.Services.AddScoped<INewsRepository, EFNewsRepository>();

var app = builder.Build();
app.UseSession();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
SeedData.EnsurePopulated(app);
app.Run();