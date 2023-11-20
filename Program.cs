using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Areas/Admin/Views/Home/TrangAdmin.cshtml");
    options.AreaViewLocationFormats.Add("/Areas/Admin/Views/Shared/_Layout.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/_Layout.cshtml");
});

//Thiết lập dịch vụ kết nối tới Database 
var connectionString = builder.Configuration.GetConnectionString("ShopDB");
builder.Services.AddDbContext<QLBanGiayDBContext>(opptions => opptions.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(60); //Thời gian tồn tại của session
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=TrangAdmin}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "quantri",
    pattern: "{controller=SanPham}/{action=Index}/{id?}");
app.Run();
