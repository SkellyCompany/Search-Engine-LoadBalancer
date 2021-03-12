using LoadBalancer.LoadManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.LoadBalancer
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowWeb",
                builder =>
                {
                    builder.AllowAnyOrigin();
                    // builder.WithOrigins("http://localhost:5000");
                    // builder.AllowAnyHeader();
                    // builder.AllowAnyMethod();
                    // builder.AllowCredentials();
                });
            });

            services.Configure<LoadBalancerSettings>(Configuration.GetSection(nameof(LoadBalancerSettings)));

            services.AddSingleton<ILoadBalancerSettings, LoadBalancerSettings>(sp =>
                sp.GetRequiredService<IOptions<LoadBalancerSettings>>().Value);
            services.AddSingleton<ILoadManager, LoadManager>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SearchEngine - LoadBalancer", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SearchEngine - LoadBalancer v1"));
            }

            app.UseCors("AllowWeb");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
