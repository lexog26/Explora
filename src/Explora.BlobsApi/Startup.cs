using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Explora.BusinessLogic.Configurations.Mapper.Explora;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Explora.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Explora.DataLayer.UnitOfWork;
using Explora.DataLayer.Repository.Interfaces;
using Explora.DataLayer.Repository;
using Explora.BusinessLogic.Services.Interfaces;
using Explora.BusinessLogic.Services;

namespace Explora.BlobsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Mapper
            services.AddAutoMapper(typeof(ExploraMapperProfile));

            //Context
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

            //Unit of work
            services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork<ExploraContext>>();

            //Repositories
            services.AddScoped<IRepository, ContextRepository<ExploraContext>>();

            //Services
            //Storage Service
            services.AddScoped<IBlobStorageService>(x => new BlobStorageService(Environment.WebRootPath));

            //ExploraFiles service
            services.AddScoped<IExploraFileService, ExploraFileService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
