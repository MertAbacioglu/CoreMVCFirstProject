using CoreMVCIntro.Models.Context;
using CoreMVCIntro.VMClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVCIntro.CommonTools; //Session methodumu getirmek için ekledik.
using CoreMVCIntro.Tools;
using CoreMVCIntro.Models.Entities;

namespace CoreMVCIntro.Controllers
{
    public class CustomerController : Controller
    {
        MyContext _db;
        public CustomerController(MyContext db)
        {
            _db = db;
        }
        public IActionResult ShoppingList()
        {
            ProductVM pvm = new ProductVM
            {
                Products = _db.Products.ToList(),
            };
            return View(pvm);
        }

        public IActionResult AddToCart(int id)
        {
            Cart cart = HttpContext.Session.GetObject<Cart>("scart") == null ? new Cart() : HttpContext.Session.GetObject<Cart>("scart");
            Product toBeAdded = _db.Products.Find(id);
            CartItem cartItem = new CartItem
            {
                ID=toBeAdded.ID,
                Name=toBeAdded.ProductName,
                Price=toBeAdded.UnitPrice
            };

            cart.SepeteEkle(cartItem);
            //session'ımızı yaratmamaız ya da güncellememiz lazım :
            HttpContext.Session.SetObject("scart",cart);

            return RedirectToAction("ShoppingList");
        }

        public IActionResult CartPage()
        {
            if (HttpContext.Session.GetObject<Cart>("scart")==null)
            {
                TempData["message"] = "There are no products in the cart ";
                return View();
            }
            else
            {
                Cart cart = HttpContext.Session.GetObject<Cart>("scart");
                return View(cart);
            }
        }
    }
}
