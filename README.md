# 🛍️ Mini Store

متجر إلكتروني مصغر تم تطويره باستخدام **ASP.NET Core MVC** و **Entity Framework Core** و **SQL Server**، ويهدف إلى تطبيق مفاهيم MVC وبناء لوحة تحكم متكاملة لإدارة المنتجات مع واجهة حديثة باستخدام Bootstrap 5.

![Mini Store](Screenshots/Mini-Store.jpg)

---

# 📖 نبذة عن المشروع

يحتوي المشروع على متجر إلكتروني لعرض المنتجات بالإضافة إلى لوحة تحكم (Dashboard) لإدارة المنتجات وربطها بالفئات باستخدام قاعدة بيانات SQL Server.

يمكن للمستخدم:

- تصفح المنتجات.
- البحث عن المنتجات.
- عرض تفاصيل المنتج.
- تصفح الصفحات الثابتة.
- إدارة المنتجات من لوحة التحكم.

---

#  المميزات

-  عرض جميع المنتجات.
-  عرض تفاصيل المنتج.
-  إضافة منتج جديد.
-  تعديل بيانات المنتج.
-  حذف منتج مع رسالة تأكيد.
-  تصنيف المنتجات حسب الفئة.
-  One-to-Many Relationship بين Products و Categories.
-  البحث عن المنتجات بالاسم.
-  Server-side Validation باستخدام Data Annotations.
-  دعم رفع صور المنتجات.
-  واجهة حديثة ومتجاوبة باستخدام Bootstrap 5.

---


## 📂 هيكل المشروع

```text
Mini-Store
│
├── Controllers
│   ├── HomeController.cs
│   └── ProductsController.cs
│
├── Data
│   └── AppDbContext.cs
│
├── Migrations
│
├── Models
│   ├── Category.cs
│   └── Product.cs
│
├── Properties
│
├── Screenshots
│   
├── Views
│   ├── Home
│   ├── Products
│   └── Shared
│
├── wwwroot
│   ├── css
│   ├── Images
│   ├── js
│   └── lib
│
├── appsettings.json
├── Program.cs
├── Mini-Store.csproj
└── README.md
```
--- 

# 📸 Screenshots

## 🏠 الصفحة الرئيسية

![Home](Screenshots/Home.png)

---

## 📂 صفحة المنتجات

![Products](Screenshots/Products1.png)

![Products](Screenshots/Product2.png)

---

## 📄 صفحة تفاصيل المنتج

![Details](Screenshots/Details.png)

---
---

## 🏢 صفحة حول الشركة

![About](Screenshots/About.png)

---

## 📞 صفحة اتصل بنا

![Contact](Screenshots/Contact.png)

## 📊 لوحة التحكم

![Dashboard](Screenshots/Dashboard.png)

---

## ➕ إضافة منتج

![Create](Screenshots/Create.png)

---

## ✏️ تعديل منتج

![Edit](Screenshots/Edit.png)

---

## 🗑️ حذف منتج

![Delete](Screenshots/Delete.png)

---

## 🔍 البحث عن المنتجات

![Search](Screenshots/Search.png)

---

## ✅ التحقق من صحة البيانات

![Validation](Screenshots/Validation.png)



