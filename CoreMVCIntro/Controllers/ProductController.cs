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
    public class ProductController : Controller
    {
        MyContext _db;
        public ProductController(MyContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ProductVM pvm = new ProductVM
            {
                Products = _db.Products.ToList(),
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }

        public IActionResult AddProduct()
        {
            ProductVM pvm = new ProductVM
            {
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return View("Index");
        }
        public IActionResult UpdateProduct(int id)
        {
            ProductVM pvm = new ProductVM
            {
                Product = _db.Products.Find(id),
                Categories = _db.Categories.ToList()
            };
            return View(pvm);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            Product toBeUpdated = _db.Products.Find(product.ID);
            _db.Entry(toBeUpdated).CurrentValues.SetValues(product);
            return View("Index");
        }

        public IActionResult DeleteProduct(int id)
        {
            _db.Products.Remove(_db.Products.Find(id));
            _db.SaveChanges();
            return View("Index");
        }
    }
}
