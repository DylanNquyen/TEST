using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using THLTW_BT3.Models;
using THLTW_BT3.Repositories;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

// Thêm session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

var app = builder.Build();

// Thêm dữ liệu mẫu
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Áp dụng migrations
    dbContext.Database.EnsureCreated();

    // Kiểm tra và thêm dữ liệu mẫu
    if (!dbContext.Products.Any())
    {
        try
        {
            dbContext.Products.AddRange(
                new Product
                {
                    Name = "Vợt Pickleball Li-Ning Hyperpower 20",
                    Price = 1472721,
                    Description = "Vợt pickleball cao cấp",
                    Images = new List<ProductImage> { new ProductImage { Url = "/images/pickleball.jpg" } },
                    CategoryId = 1
                },
                new Product
                {
                    Name = "Giày Thể Thao Li-Ning Unisex II Lite",
                    Price = 1474666,
                    Description = "Giày thể thao unisex",
                    Images = new List<ProductImage> { new ProductImage { Url = "/images/shoes.jpg" } },
                    CategoryId = 2
                },
                new Product
                {
                    Name = "Quần Lót Thể Thao Nam Li-Ning Thơ",
                    Price = 2181818,
                    Description = "Quần lót thể thao nam",
                    Images = new List<ProductImage> { new ProductImage { Url = "/images/underwear.jpg" } },
                    CategoryId = 3
                },
                new Product
                {
                    Name = "Vợt Cầu Lông Kawasaki Spider 4U",
                    Price = 4053000,
                    Description = "Vợt cầu lông cao cấp",
                    Images = new List<ProductImage> { new ProductImage { Url = "/images/badminton.jpg" } },
                    CategoryId = 4
                }
            );
            dbContext.SaveChanges();
            Console.WriteLine("Dữ liệu mẫu đã được thêm vào cơ sở dữ liệu THLTW_BT3.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi thêm dữ liệu mẫu: {ex.Message}");
        }
    }
    else
    {
        // Kiểm tra và thêm hình ảnh cho các sản phẩm hiện có
        var products = dbContext.Products.Include(p => p.Images).ToList();
        foreach (var product in products)
        {
            if (product.Images == null || !product.Images.Any())
            {
                product.Images = new List<ProductImage>
                {
                    new ProductImage { Url = $"/images/{product.Name.ToLower().Replace(" ", "-")}.jpg" }
                };
            }
        }
        dbContext.SaveChanges();
        Console.WriteLine("Đã kiểm tra và thêm hình ảnh cho các sản phẩm hiện có.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();