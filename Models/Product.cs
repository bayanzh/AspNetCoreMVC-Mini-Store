using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Mini_Store.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم المنتج مطلوب")]
        [StringLength(100, ErrorMessage = "اسم المنتج يجب ألا يتجاوز 100 حرف")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "السعر مطلوب")]
        [Range(1, 100000, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        public decimal Price { get; set; }

        public string? Image { get; set; }

        // رفع الصورة
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

       
        [Required(ErrorMessage = "اختر الفئة")]
        public int CategoryId { get; set; }

        
        public Category? Category { get; set; }
    }
}