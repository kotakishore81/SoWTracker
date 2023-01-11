using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoW.Tracker.WebAPI.Models;
using SoW.Tracker.WebAPI.ServiceInterface;
using SoW.Tracker.WebAPI.ServiceImplementation;
using SoW.Tracker.WebAPI.Utility.OtherUtilities.OtherUtilitiesInterface;
using SoW.Tracker.WebAPI.DBContext;
using SoW.Tracker.WebAPI.Utility.OtherUtilities.OtherUtilitiesImplementation;
using SoW.Tracker.WebAPI.Models.ViewModels;


namespace SoW.Tracker.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            //this part is to load the appsettings file basdd on the environment.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.Development.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            configuration = builder.Build();
            //Load appsettings end.

            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
      
            ///Load the connection string from appsetting.json file
            services.AddSingleton(Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IManageUsers, ManageUsers>();
            services.AddScoped<ISeachSoW, SearchSoW>();
            services.AddScoped<ISoWTracker, SoWTracker>();
            services.AddScoped<ISoWReview, SoWReview>();
            services.AddScoped<IEmailCommunication, EmailCommunication>();
            //Inject IUtilityFunctions to UtilityFunctions class.
            services.AddScoped<IUtilityFunctions, UtilityFunctions>();
            services.AddDbContext<SoWDbContext>();
            services.Configure<LoggingConfigSection>(Configuration.GetSection("LoggingConfigSection"));
           // services.Configure<LdapConfigSection>(Configuration.GetSection("LdapConfiguration"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //var config = new AutoMapper.MapperConfiguration(c =>
            //{
            //    c.AddProfile(new AutoMapperProfile());
            //});

            //var mapper = config.CreateMapper();
            //services.AddSingleton(mapper);
            services.AddMemoryCache();
            //Allowing the Cors Policy , in order to call the web API from different domain 
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            //Allow Cors Policy end 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Using the Cors Policy 
            app.UseCors("CorsPolicy");
            //Cors Policy use end 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
