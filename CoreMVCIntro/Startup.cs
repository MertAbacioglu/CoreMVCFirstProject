using CoreMVCIntro.Models.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //hangi serbvisin kullanılacağı (dikkat.henüz kullanmadık). Servisleri ekliyoruz sadece

            //Burada standart bir Sql bağlantısı belirlemek istiyosanız (sınıf içinde optionBuilder'dan belirlemektense bu tercih  edillir) burada belşirlemelisiniz.

            //Pool kullanmak bir singleton pattern görevi görür.

            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("myConnection")).UseLazyLoadingProxies());
            //Yukarıdaki UseLazyLoadingProxies metodu  .NetCore'daki LazyLoading'in  sürekli tetiklenebilmesi adına gereklidir.
            services.AddControllersWithViews(); //böylece bağlantı ayarımızı burada belirlemiş olduk.

            //Authentication işlemini yapabilme için servisi burada yaratmamız gerekli. Biz services.AddAuthentication dediğimiz zaman bir session oluşur ama bu bildiğimiz session değildir cookie bazlı bir session'dır, yine bizim performans kazanmamız için kullandığımız cihazda yapıyor bunu.

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Home/Login"; //burada nerden ne bilgi alacğaını anlatıyoruz.
            });

            //Session kullanacaksak ayarlamaları yapmalıyız :
            services.AddSession(x =>
            {
                x.IdleTimeout = TimeSpan.FromMinutes(20); //Boş durduğum süre ne kadar olabilsin
                x.Cookie.HttpOnly = true; // güvenlik veriyouz protokou true yaparak
                x.Cookie.IsEssential = true; //cookie bazlı mı olsun. (cookie hariç başka yerden de gelsin mi mesela sadece api'ye özgü durumlar token vs. durumları)
            })

;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication'ı Authorization'dan önce vermeye özen gösterin.
            app.UseAuthentication(); //Kullanıcı kim bunu algıla.(doğrulama)

            app.UseAuthorization(); //Bizim yetkimiz var mı yok mu gibi durumlarda(yani authorization durumlarında) çalışacak metottur. (yetkilenndirme)

            //Bizim kim olduğumuzu Authentication ile anlıyor. Bizim yetkimiz olup olmadığını Authorization ile anlıyor.


            app.UseSession(); //session'ı ekledikten sonra kullanıyoruz
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=ProductList}/{id?}");
            });
        }
    }
}
