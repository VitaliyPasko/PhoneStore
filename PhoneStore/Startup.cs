using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneStore.Models;
using PhoneStore.Services;
using PhoneStore.Services.Abstractions;

namespace PhoneStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private IServiceCollection _services;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MobileContext>(options => options.UseSqlite(connection));
            services.AddControllersWithViews();
            services.AddScoped<IBasketService, BasketService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.Run(async context =>
            // {
            //     var sb = new StringBuilder();
            //     sb.Append("<h1>Все сервисы</h1>");
            //     sb.Append("<table>");
            //     sb.Append("<tr><th>Тип</th><th>Lifetime</th><th>Реализация</th></tr>");
            //     foreach (var svc in _services)
            //     {
            //         sb.Append("<tr>");
            //         sb.Append($"<td>{svc.ServiceType.FullName}</td>");
            //         sb.Append($"<td>{svc.Lifetime}</td>");
            //         sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
            //         sb.Append("</tr>");
            //     }
            //     sb.Append("</table>");
            //     context.Response.ContentType = "text/html;charset=utf-8";
            //     await context.Response.WriteAsync(sb.ToString());
            // });
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
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}