using CMLSmartHomeController.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Microsoft.Extensions.Hosting;
using Quartz.Spi;
using CMLSmartHomeController.JobScheduler;
using Quartz;
using Quartz.Impl;
using CMLSmartHomeController.JobScheduler.Jobs;

namespace CMLSmartHomeController
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

            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc()
                .AddNewtonsoftJson();

            services.AddControllersWithViews();
            services.AddControllers();
            services.AddRazorPages();

            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // OpenWeatherJob
            services.AddSingleton<OpenWeatherJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(OpenWeatherJob),
                cronExpression: "0 0 0/1 * * ?")); // run every 1 hour
             // cronExpression: "0/30 * * * * ?")); // run every 5 seconds
             // cronExpression: "0 0/5 * * * ?")); // run every 5 minutess

            // WeatherRecordArchiveJob
            services.AddSingleton<WeatherRecordArchiveJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(WeatherRecordArchiveJob),
                cronExpression: "0 0 23 ? * *")); // run every day at 23:00

            //QuartzService
            services.AddHostedService<QuartzHostedService>();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = 2;
                options.KnownProxies.Add(IPAddress.Parse("127.0.10.1"));
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMLSmartHome API", Version = "v1" });                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                // c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddLog4Net();
                                    
        }
    }
}
