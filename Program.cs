using AstreeWebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AstreeWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AstreeDbContext>(options =>
    options.UseSqlServer(connectionString + ";TrustServerCertificate=True;"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity
builder.Services.AddDefaultIdentity<AstreeUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AstreeDbContext>();

// Configure Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Account/Login";
        options.LogoutPath = "/Identity/Account/Logout"; // Ensure this path matches
    });

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache(); // Use distributed cache if needed
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Make the session cookie HTTP only
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

// Ensure session is used before authentication and authorization
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
