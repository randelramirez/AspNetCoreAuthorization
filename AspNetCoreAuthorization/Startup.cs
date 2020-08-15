using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Areas.Identity.Data;
using AspNetCoreAuthorization.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspNetCoreAuthorization.Authorization.Policies;
using AspNetCoreAuthorization.Authorization.Claims;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreAuthorization.Authorization.Requirements;
using Microsoft.AspNetCore.Identity.UI.Services;
using AspNetCoreAuthorization.Models;
using AspNetCoreAuthorization.Authorization.Middleware;

namespace AspNetCoreAuthorization
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

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
            });

            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddDbContextPool<UserManagementContext>(options =>
            {
                var connectionString = $"Data Source = localhost; Database={typeof(UserManagementContext).Assembly.GetName().FullName}; Integrated Security = True";
                options.UseSqlServer(connectionString);
            });

            //AddAuthentication
            //AddCookie
            services.AddIdentity<MyAppUser, IdentityRole>()
                .AddEntityFrameworkStores<UserManagementContext>()
                .AddDefaultTokenProviders();

            // override default, because Identity is using different LoginPath etc. from default
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.ConfigureApplicationAuthorization();

            services.AddScoped<IUserClaimsPrincipalFactory<MyAppUser>, UserClaimsPrincipalFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.CreateIdentityDatabase();
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
                endpoints.MapRazorPages();
                //.RequireAuthorization(PoliciesConstants.AUTHENTICATED_USER, PoliciesConstants.IT_PROFESSIONAL);

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                .RequireAuthorization(PoliciesConstants.AUTHENTICATED_USER, PoliciesConstants.IT_PROFESSIONAL);
            });
        }
    }
}
