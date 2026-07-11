using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mini_Store;
using Mini_Store.Data;

var builder = WebApplication.CreateBuilder(args);

// =====================================
// Database
// =====================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// =====================================
// Localization
// =====================================
builder.Services.AddLocalization(options =>
    options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    });

// =====================================
// Supported Languages
// =====================================
var supportedCultures = new[] { "ar", "en-US" };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("ar");

    options.SupportedCultures = supportedCultures
        .Select(c => new System.Globalization.CultureInfo(c))
        .ToList();

    options.SupportedUICultures = supportedCultures
        .Select(c => new System.Globalization.CultureInfo(c))
        .ToList();

    options.RequestCultureProviders = new IRequestCultureProvider[]
    {
        new CookieRequestCultureProvider()
    };
});

var app = builder.Build();

// =====================================
// Configure the HTTP request pipeline.
// =====================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// =====================================
// Localization Middleware
// =====================================
var localizationOptions =
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(localizationOptions.Value);

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();