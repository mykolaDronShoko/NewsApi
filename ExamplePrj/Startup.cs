using System.Reflection;
using AuthSPA.Controllers;
using DbContextSPA.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModelsSPA.Models;

namespace ExamplePrj
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<ExampleDbContext>(options =>
         
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors(option =>
            {
                option.AddPolicy("AllowSpecificOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            services.AddIdentity<ApplicationUser, IdentityRole>()
                     .AddEntityFrameworkStores<ExampleDbContext>()
                     .AddDefaultTokenProviders();
            services.AddMvcCore()
                  .AddApplicationPart(typeof(AccountController).GetTypeInfo().Assembly)
                 .AddControllersAsServices()
                 .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                 .AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("AllowSpecificOrigins");
          //  app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
