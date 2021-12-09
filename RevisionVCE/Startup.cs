using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RevisionVCE.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RevisionVCE.Services;
using RevisionVCE.IServices;
using RevisionVCE.UnitOfWork;
using RevisionVCE.IRepositories;
using RevisionVCE.Repositories;

namespace RevisionVCE
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
            services.AddControllersWithViews();
            services.AddDbContext<VceQuizzContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DevEnvironnement"), b => b.MigrationsAssembly("RevisionVCE.UI"))
                .UseLazyLoadingProxies();

            });
            AddInjectionDependencyForServices(services);
            AddInjectionDependencyForRepositories(services);
        }

        private void AddInjectionDependencyForServices(IServiceCollection services)
        {
            services.AddScoped<IPdfParserService, PdfParserService>();
            services.AddScoped<ISurveyService, QuestionService>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }

        private void AddInjectionDependencyForRepositories(IServiceCollection services)
        {
            services.AddScoped<ISurveyRepository, SurveyRepository>();
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
