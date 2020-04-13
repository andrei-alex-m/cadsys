using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Caly.Dropbox;
using CS.CadGen;
using CS.EF;
using CS.Services;
using CS.Services.Interfaces;
using CS.ImportExportWeb.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CS.ImportExportWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            
            var connectionString = configuration.GetConnectionString("con");

            services.AddTransient<IExcelConfigurationRepo>(s => new ExcelConfigurationRepo(Environment.ContentRootPath));

            services.AddSingleton(s => new ServiceBuilder(Environment.ContentRootPath));

            services.AddSingleton(s => new DropBoxBase());

            services.AddDbContext<CadSysContext>(options => options.UseMySql(connectionString), ServiceLifetime.Transient);

            services.AddTransient<IRepo, Repo>();

            services.AddTransient<IExporter, Exporter>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddJsonOptions(opt =>
                        {
                            opt.JsonSerializerOptions.IgnoreNullValues = true;
                        });
            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoint.MapHub<MessagesHub>("/messageshub");
            });

            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<MessagesHub>("/messageshub");
            //});


        }
    }
}
