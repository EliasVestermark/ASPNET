using ASPNetMVC.Helpers;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase")));
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<IsCourseSaved>();
builder.Services.AddHttpClient();

builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie.HttpOnly = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    x.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    x.SlidingExpiration = true;

    x.LoginPath = "/signin";
    x.LogoutPath = "/signout"; 
});

builder.Services.AddAuthentication().AddFacebook(x => 
{
    x.AppId = "905519511272067";
    x.AppSecret = "5d6ad2d72d5f3ce49ed07438839edec7";
    x.Fields.Add("first_name");
    x.Fields.Add("last_name");
});

builder.Services.AddAuthentication().AddGoogle(x =>
{
    x.ClientId = "885765406612-3edd6d8h7lgpnv5s0fk5fug2u6jegmng.apps.googleusercontent.com";
    x.ClientSecret = "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";
});

var app = builder.Build();
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseUserSessionValidation();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
