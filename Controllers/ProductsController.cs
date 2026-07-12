using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mini_Store.Data;
using Mini_Store.Models;

namespace Mini_Store.Controllers
{
    [Authorize (Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(
            AppDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // =======================
        // عرض جميع المنتجات
        // =======================
public async Task<IActionResult> Index(string searchString)
{
    string user = Request.Cookies["User"] ?? "Not Found";

    string name = HttpContext.Session.GetString("UserName") ?? "Not Found";

    int? id = HttpContext.Session.GetInt32("UserId");

    Console.WriteLine($"UserName: {name}, UserId: {id}");
    Console.WriteLine(user);

    ViewBag.User = user;
    ViewBag.UserName = name;
    ViewBag.UserId = id;

    var products = _context.Products
        .Include(p => p.Category)
        .AsQueryable();

    if (!string.IsNullOrWhiteSpace(searchString))
    {
        products = products.Where(p =>
            p.Name.Contains(searchString));
    }

    ViewBag.SearchString = searchString;

    return View(await products.ToListAsync());
}

        // =======================
        // عرض صفحة الإضافة
        // =======================
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(
                _context.Categories,
                "Id",
                "Name");

            return View();
        }
// =======================
// عرض صفحة التعديل
// =======================
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var product = await _context.Products.FindAsync(id);

    if (product == null)
    {
        return NotFound();
    }

    ViewBag.Categories = new SelectList(
        _context.Categories,
        "Id",
        "Name",
        product.CategoryId);

    return View(product);
}
        // =======================
        // حفظ التعديل
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(
                        _webHostEnvironment.WebRootPath,
                        "Images");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string extension = Path.GetExtension(product.ImageFile.FileName);

                    string fileName = Guid.NewGuid().ToString() + extension;

                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(stream);
                    }

                    product.Image = fileName;
                }

                _context.Update(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(
                _context.Categories,
                "Id",
                "Name",
                product.CategoryId);

            return View(product);
        }
// =======================
// حفظ المنتج
// =======================
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Product product)
{
    if (ModelState.IsValid)
    {
        if (product.ImageFile != null)
        {
            string uploadsFolder = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "Images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string extension = Path.GetExtension(product.ImageFile.FileName);

            string fileName = Guid.NewGuid().ToString() + extension;

            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await product.ImageFile.CopyToAsync(stream);
            }

            product.Image = fileName;
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    ViewBag.Categories = new SelectList(
        _context.Categories,
        "Id",
        "Name",
        product.CategoryId);

    return View(product);
}
        // =======================
        // صفحة الحذف
        // =======================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // =======================
        // تنفيذ الحذف
        // =======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
       
    }
}