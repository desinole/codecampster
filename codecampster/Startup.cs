using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Codecamp2018.Models;
using Codecamp2018.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Codecamp2018
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
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddMvc();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // For the API. Allow other domain names to call us.
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });


            services.AddCors(options =>
            {
                options.AddPolicy("AllowFromAll",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // For the API : to allow other domains than ours to access the API
            app.UseCors(policy => {
                policy.WithOrigins("*");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    var configurationSection = Configuration.GetSection("AppSettings");
                    var adminUser = configurationSection.GetValue<string>("AdminUser");
                    var adminPass = configurationSection.GetValue<string>("AdminPass");
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                         .Database.Migrate();
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                    .EnsureSeed(adminUser, adminPass);
                }
            }
            catch { }

            app.UseStaticFiles();
            app.UseAuthentication();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
