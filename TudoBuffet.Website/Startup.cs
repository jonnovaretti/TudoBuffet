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
using TudoBuffet.Website.Repositories;
using TudoBuffet.Website.Repositories.Contracts;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System;
using Microsoft.IdentityModel.Tokens;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                             .AddJsonOptions((setup) =>
                             {
                                 setup.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                                 setup.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                             });

            AddServicesToContainer(services);
            AddConfigurationInstance(services);
            AddAuthenticationToPipeline(services);
        }

        private void AddAuthenticationToPipeline(IServiceCollection services)
        {
            var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                          .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();


            var appSettingsSection = Configuration.GetSection("ApplicationSetting");
            var key = Encoding.ASCII.GetBytes(appSettingsSection.GetSection("SecretKey").Value);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;

                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserAuthenticatior>();
                        var userId = Guid.Parse(context.Principal.FindFirst("id").Value);
                        var user = userService.GetUserById(userId);
                        if (user == null)
                        {
                            context.Fail("Não autorizado");
                        }
                        return Task.CompletedTask;
                    }
                };

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
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
        }

        private static void AddServicesToContainer(IServiceCollection services)
        {
            services.AddTransient<IUserSignup, UserSignupService>();
            services.AddTransient<IEmailSender, EmailSenderService>();
            services.AddTransient<IUsersEmailsValidationUoW, UsersEmailsValidationUoW>();
            services.AddTransient<IEmailValidator, EmailValidatorService>();
            services.AddTransient<IUserAuthenticatior, UserAuthenticatorService>();
            services.AddTransient<IBuffets, Buffets>();
            services.AddTransient<IPlans, Plans>();
            services.AddTransient<IPhotos, Photos>();
            services.AddTransient<IBlobFileHandler, BlobFileHandlerService>();
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
    
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
