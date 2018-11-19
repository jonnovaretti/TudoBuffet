using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TudoBuffet.Website.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using TudoBuffet.Website.Services.Contracts;
using TudoBuffet.Website.Services;
using System.IO;
using TudoBuffet.Website.Configs;

namespace TudoBuffet.Website
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                             .AddJsonOptions((setup) =>
                             {
                                 setup.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                                 setup.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                             });

            var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                          .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();

            var dbConnection = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TudoBuffet;Integrated Security=True";
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(dbConnection));

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();

            services.Configure<ConnectionString>(config.GetSection("ConnectionString"));
            services.Configure<ApplicationSetting>(config.GetSection("ApplicationSetting"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
