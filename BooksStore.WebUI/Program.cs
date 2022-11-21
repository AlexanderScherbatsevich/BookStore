using BookStore.Domain.Abstract;
using BookStore.Domain.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EFDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
        name: "",
        pattern: "",
        defaults: new { controller = "Products", action = "List", category = (string)null, page = "1" });

    app.MapControllerRoute(
        name: "",
        pattern: "Page{page}",
        defaults: new { controller = "Products", action = "List", category = (string)null },
        constraints: new { page = @"\d+" });

    app.MapControllerRoute(
        name: "",
        pattern: "{category}",
        defaults: new { controller = "Products", action = "List", page = "1" });

    app.MapControllerRoute(
        name: "",
        pattern: "{category}/Page{page}",
        defaults: new { controller = "Products", action = "List" },
        constraints: new { page = @"\d+" });

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
