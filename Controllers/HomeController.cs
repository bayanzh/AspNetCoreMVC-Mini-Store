using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mini_Store.Models;

namespace Mini_Store.Controllers;

public class HomeController : Controller
{
    private static dynamic[] _categories =
    {
        new { Id = 0, Name = "إلكترونيات", Icon = "fa-solid fa-bolt-lightning" },
        new { Id = 1, Name = "ملابس", Icon = "fa-solid fa-shirt" },
        new { Id = 2, Name = "كتب", Icon = "fas fa-book-open" }
    };

    private static dynamic[] _products =
    {
        new
        {
            Id = 1,
            CategoryId = 0,
            Name = "هاتف ذكي",
            Price = 2500,
            Description = "هاتف ذكي بكاميرا عالية الدقة",
            Image = "phone.jpg"
        },

        new
        {
            Id = 2,
            CategoryId = 0,
            Name = "حاسوب محمول",
            Price = 4500,
            Description = "حاسوب مخصص للمطورين",
            Image = "laptop.jpg"
        },

        new
        {
            Id = 3,
            CategoryId = 1,
            Name = "قميص قطني",
            Price = 150,
            Description = "قميص صيفي مريح",
            Image = "shirt.jpg"
        },

        new
        {
            Id = 4,
            CategoryId = 2,
            Name = "كتاب برمجة",
            Price = 90,
            Description = "دليل شامل لتعلم البرمجة",
            Image = "book.jpg"
        }
    };

public IActionResult Index()
{
    CookieOptions options = new CookieOptions
    {
        Expires = DateTime.Now.AddDays(7)
    };

    Response.Cookies.Append("User", "Saad", options);

    HttpContext.Session.SetString("UserName", "أحمد");
HttpContext.Session.SetInt32("UserId", 105);

    ViewBag.Categories = _categories;
    ViewBag.Products = _products;

    return View();
}

    public IActionResult Products(int id)
    {
        var products = _products
            .Where(p => p.CategoryId == id)
            .ToList();

        var category = _categories
            .FirstOrDefault(c => c.Id == id);

        ViewBag.Products = products;
        ViewBag.Category = category;

        return View();
    }

    public IActionResult Details(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Product = product;

        return View();
    }

    public IActionResult Search(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return RedirectToAction("Index");
        }

        var products = _products
            .Where(p =>
                p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();

        ViewBag.Keyword = keyword;
        ViewBag.Products = products;

        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}