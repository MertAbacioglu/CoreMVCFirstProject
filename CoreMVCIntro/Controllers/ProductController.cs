using CoreMVCIntro.Models.Context;
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
    }
}
