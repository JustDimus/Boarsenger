using Boarsenger.API.BLL.Service;
using Boarsenger.API.BLL.Service.Implementations;
using Boarsenger.API.Core.Models;
using Boarsenger.API.DAL.Repository;
using Boarsenger.API.DAL.Repository.Implementations;
using Boarsenger.API.EF;
using Boarsenger.API.MVCInterface.FluentValidation;
using FluentValidation.AspNetCore;
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
            services
                .AddDbContext<APIDbContext>(c => 
                c.UseSqlServer(Configuration.GetConnectionString("API.DbConnection")));

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<DbContext>(c => c.GetRequiredService<APIDbContext>());

            services.AddTransient<IEncryptionService, EncryptionService>();
            services.AddTransient<IAccountService, AccountService>();

            services.AddControllersWithViews(setup =>
            {
            }).AddFluentValidation(setup =>
            {
                setup.RegisterValidatorsFromAssemblyContaining<AccountCreditionalsValidationRules>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
