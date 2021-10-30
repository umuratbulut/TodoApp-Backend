using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string ApiCorsPolicy = "_apiCorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(ApiCorsPolicy, builder =>
            {
                builder.WithOrigins("https://localhost:5000", "http://localhost:5000")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithMethods("PUT", "DELETE", "POST", "GET"); 
                //.AllowAnyMethod()
                //.AllowAnyHeader()
                //.AllowCredentials();
            }));

            services.AddControllers();
            services.AddRazorPages();
            services.AddSwaggerDocument(config=> {
                config.PostProcess=(doc =>
                {
                    doc.Info.Title = "TodoApp Web API";
                    doc.Info.Version = "1.0.0";
                    doc.Info.Contact = new NSwag.OpenApiContact()
                    {
                        Name = "Murat BULUT",
                        Url= "https://github.com/umuratbulut",

                    };
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseRouting();
            app.UseCors(ApiCorsPolicy);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
