using CoreMVCIntro.Models.Context;
using CoreMVCIntro.Models.Entities;
using CoreMVCIntro.VMClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro.Controllers
{
    public class CategoryController : Controller
    {
        MyContext _db;

       //startup'ta yaptığımız ayarlamalar sayesinde projenin herhangi bir yerinde herhangi bir metotta MyContex tipinde bir parametre görüldüğü anda SingletonPattern uygulanarak ram'deki yapı getirilecek.

        //_ViewImports dosyamız View'larımızın kullanılacağı ortak alanları belirlediğiniz bir dosyadır.

        //.NetCore, MVC Helperları korumanın yanı sıra daha kolay ve performanslı bir yapı da sunar. Bunlar TagHelper'dır. Normal HTML tagler içierisine yazılan attribute'lardır. Kullanabilmek için namespaceleri gereklidir.(zaten_ViewImport içerisinde vardır)

        //Projeyi wtch run olarak izlemek için proje klasörüne gt cmd yaz enter bas. dotnet watch run yaz. Front end'de yazdığın restart etmeden yansır.
         
        public CategoryController(MyContext db)
        {
            _db = db;
        }
        public IActionResult CategoryList()
        {
            CategoryVM cvm = new CategoryVM
            {
                Categories = _db.Categories.ToList()
            };
            return View(cvm);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return View();
        }


        public IActionResult UpdateCategory(int id)
        {
            CategoryVM cvm = new CategoryVM
            {
                Category = _db.Categories.Find(id)
            };
            return View(cvm);

        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            Category toBeUpdated = _db.Categories.Find(category.ID);
            _db.Entry(toBeUpdated).CurrentValues.SetValues(category);
            _db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {
            _db.Remove(_db.Categories.Find(id));
            _db.SaveChanges();
            return RedirectToAction("Index");

        }




    }
}
