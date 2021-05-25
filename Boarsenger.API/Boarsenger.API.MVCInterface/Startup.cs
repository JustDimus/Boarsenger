using Boarsenger.API.BLL.Service;
using Boarsenger.API.BLL.Service.Implementations;
using Boarsenger.API.Core.Models;
using Boarsenger.API.DAL.Repository;
using Boarsenger.API.DAL.Repository.Implementations;
using Boarsenger.API.EF;
using Boarsenger.API.MVCInterface.FluentValidation;
using Boarsenger.API.MVCInterface.Services.Implementation;
using FluentValidation.AspNetCore;
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

namespace Boarsenger.API.MVCInterface
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
            services.AddHttpContextAccessor();

            services
                .AddDbContext<APIDbContext>(c => 
                c.UseSqlServer(Configuration.GetConnectionString("API.DbConnection")));

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<DbContext>(c => c.GetRequiredService<APIDbContext>());

            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountTokenService, AccountTokenService>();
            services.AddTransient<IServerService, ServerService>();
            services.AddTransient<IServerTokenService, ServerTokenService>();

            services.AddControllersWithViews(setup =>
            {
            }).AddFluentValidation(setup =>
            {
                setup.RegisterValidatorsFromAssemblyContaining<AccountCreditionalsValidationRules>();
            }).AddNewtonsoftJson();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/LogIn");
                    options.Cookie.Name = "BoarCookie";
                });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "api",
                    areaName: "api",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
