using CoreMVCIntro.Models.Context;
using CoreMVCIntro.Models.Entities;
using CoreMVCIntro.VMClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreMVCIntro.Controllers
{
    //CodeFirst için EFCore indir. Migrations yapabilmek içinse EFCore.Tools gerekir.
    public class HomeController : Controller
    {
        MyContext _db;
        public HomeController(MyContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        //.NetCore Authorization işlemleri(artık bir class açmayacağız çnet teki gibi)

        //Async metotlar her zaman generic bir task döndürmek zorundalar. ister kullan ister kullanma önemli değil ama döndürecek. Task class'ı asenkron  metotların calısma prensipleri hakkında ayrıntılı bilgiyi tutan(metot çalıışırken hata var mı, metotdun bu grevi yapma sırasında  kendisine eşzamanlı gelen istekler metodun calısma durumu(success, failed).. ) o yüzden normal şartlarda döndüreceğiniz dğeri Task'e generic olarak vermek zorundasınız.
        [HttpPost]
        public async Task<IActionResult> Login(Employee employee)
        {
            Employee loginEmployee = _db.Employees.FirstOrDefault(x => x.FirstName == employee.FirstName);
            if (loginEmployee != null)
            {
                //CLAIM, rol bazlı(admin girsin member girsin gibi) veya identity(admin olan ve ismi mert olan gibi) bazlı güvenlik işlemlerinden sorumlu olan bir class'tır. Dilersek birden fazla claim nesnesi yaratıp hepsini kullabiliriz.rol bazlı seçelim şimdi


                List<Claim> claims = new List<Claim>
                {
                    

                    new Claim(ClaimTypes.Role,loginEmployee.UserRole.ToString())

                };

                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login"); //burada login ismine sahip olan güvenlik durumu için hangi güvenlik önlemlerinin çalışacağını belirleyeceğiz.

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);//.NetCore'a burada anlatıyoruz.  //.NetCor'un içerisinde sonlanmış olan security işlemlerinin artık tetiklenmesi lazım. 

                //asenkron methodlar çalıştıkalrı zaman başka bir işlemi nengellenmemesini sağlayan metotlardır. 

                await HttpContext.SignInAsync(principal);
                return RedirectToAction("ProductList", "Product");



            }
            return View(new EmployeeVM { Employee = employee });

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
