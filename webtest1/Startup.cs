using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using webtest1.Controllers;
using webtest1.Options;
using webtest1.Services;

namespace webtest1
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
            services.Configure<DALOptions>(Configuration.GetSection(DALOptions.DAL));

            services.AddSingleton(typeof(IDataService<>), typeof(DataService<>));
            services.AddSingleton(typeof(IVersionService), typeof(VersionService));
            
            services.AddHealthChecks()
                .AddCheck<LiveCheck>("live_check", null, new[] { "live" })
                .AddCheck<ReadyCheck>("ready_check", null, new[] { "ready" })
                .AddCheck<StartupCheck>("startup_check", null, new[] { "startup" });

            services.AddHttpClient();
                
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMetricServer();
            app.UseRequestMiddleware();
            
            if (env.IsDevelopment() || env.EnvironmentName == "Local")
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions() { Predicate = p => p.Tags.Contains("live")});
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions() { Predicate = p => p.Tags.Contains("ready")});
                endpoints.MapHealthChecks("/health/startup", new HealthCheckOptions() { Predicate = p => p.Tags.Contains("startup")});
            });            
        }
    }
}
