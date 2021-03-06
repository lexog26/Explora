using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Explora.BusinessLogic.Configurations.Mapper.Explora;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Explora.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Explora.DataLayer.UnitOfWork;
using Explora.DataLayer.Repository;
using Explora.BusinessLogic.Services.Interfaces;
using Explora.BusinessLogic.Services;
using Explora.DataLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Explora.Web.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Explora.Web.Helpers;
using Explora.Web.Configurations;

namespace Explora.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Smtp settings
            services.Configure<SmtpConfiguration>(options => Configuration.GetSection("SMTP").Bind(options));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string identityUrl = Configuration.GetValue<string>("IdentityUrl");

            //Identity(Bearer access token)
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = identityUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "filesApi";
                });

            //Mapper
            services.AddAutoMapper(typeof(ExploraMapperProfile));

            //ExploraContext
            string connectionString = Configuration.GetConnectionString("ExploraConnectionString");
            services.AddDbContext<ExploraContext>(options =>
              options.UseMySql(connectionString,
                    mySqlOptions =>
                    {
                        mySqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(6),
                                errorNumbersToAdd: new List<int>())
                            .CommandTimeout(30);
                    }));

            //IdentityContext
            string identityConnString = Configuration.GetConnectionString("IdentityConnectionString");
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseMySql(identityConnString,
                    mySqlOptions =>
                    {
                        mySqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(6),
                                errorNumbersToAdd: new List<int>())
                            .CommandTimeout(30);
                    }));

            /*services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedEmail = true).AddEntityFrameworkStores<ApplicationDbContext>();*/

            //Options for registration email confirmed
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    //options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 1;
                }              
            ).AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            //Unit of work
            services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork<ExploraContext>>();

            //Repositories
            services.AddScoped<IRepository, ContextRepository<ExploraContext>>();

            //Services

            services.AddScoped<IEmailSender, EmailSender>();

            //Storage Service
            services.AddScoped<IBlobStorageService>(x => new BlobStorageService(Environment.WebRootPath));

            //ExploraFiles service
            services.AddScoped<IExploraFileService, ExploraFileService>();

            services.AddScoped<ICollectionService, CollectionService>();

            services.AddScoped<ITotemService, TotemService>();

            //Put login page as startup page
            services.AddMvc().AddRazorPagesOptions(options => {
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    //template: "{area=Identity}/page={Login}");
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
