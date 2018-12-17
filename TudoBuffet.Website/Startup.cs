using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using TudoBuffet.Website.Configs;
using TudoBuffet.Website.Infrastructures;
using TudoBuffet.Website.Infrastructures.Contracts;
using TudoBuffet.Website.Repositories;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Expiration = TimeSpan.FromHours(24);
                options.AccessDeniedPath = "/usuarios/acesso-negado";
                options.LoginPath = "/usuarios/entrar";
                options.LogoutPath = "/usuarios/sair";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                             .AddJsonOptions((setup) =>
                             {
                                 setup.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                                 setup.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                             });

            AddServicesToContainer(services);
            AddConfigurationInstance(services);
        }

        private static void AddConfigurationInstance(IServiceCollection services)
        {
            var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                          .AddJsonFile("appsettings.json", optional: true);

            var config = configBuilder.Build();

            var connectionSettings = config.GetSection("ConnectionString");

            services.Configure<ConnectionString>(connectionSettings);
            services.Configure<ApplicationSetting>(config.GetSection("ApplicationSetting"));

            var dbConnection = connectionSettings.GetSection("DataBase").Value;
            services.AddDbContext<MainDbContext>(options => options.UseSqlServer(dbConnection));

            var recaptchaSetting = config.GetSection("Recaptcha");
            services.Configure<RecaptchaSetting>(recaptchaSetting);
        }

        private static void AddServicesToContainer(IServiceCollection services)
        {
            services.AddTransient<IUserAccount, UserAccountService>();
            services.AddTransient<IEmailSender, EmailSenderService>();
            services.AddTransient<IUsersEmailsValidationUoW, UsersEmailsValidationUoW>();
            services.AddTransient<IEmailValidator, EmailValidatorService>();
            services.AddTransient<IBuffets, Buffets>();
            services.AddTransient<IPlans, Plans>();
            services.AddTransient<IPhotos, Photos>();
            services.AddTransient<IPhotoHandler, PhotoHandlerService>();
            services.AddTransient<IRecaptchaValidator, RecaptchaValidator>();

            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
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

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Strict
            });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
