using System;
using AspNetCoreAuthorization.Areas.Identity.Data;
using AspNetCoreAuthorization.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AspNetCoreAuthorization.Areas.Identity.IdentityHostingStartup))]
namespace AspNetCoreAuthorization.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            // We move this setup to Startup.cs
            //var connectionString = "Data Source=localhost; Database=Test; User Id=sa;Password=randel1_23;";;
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<UserManagementContext>(options =>
            //        options.UseSqlServer(
            //            connectionString));

            //    services.AddDefaultIdentity<MyAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<UserManagementContext>();
            //});
        }
    }
}