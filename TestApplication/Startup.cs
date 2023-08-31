using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TestApplication.Services;

using TestApplication.Repository.Application;
using TestApplication.Repository.Application.Interfaces;
using Serilog;

namespace TestApplication
{
    public class Startup
    {
        public IWebHostEnvironment AppEnvironment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            AppEnvironment = env;
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            #region CORS

            var origins = new[]
            {
                "http://localhost:3000",
                "http://localhost:3000/callback"
            };

            services.AddCors();

            #endregion

           

            #region DI

            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            services.AddDbContext<Model.Database.UserContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

          
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

          
            #endregion


            #region MVC

            services.AddHealthChecks();
            services.AddControllers();

            #endregion

            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSerilogRequestLogging();


            }
          
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "v1");
            });

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHealthChecks("/healthprobe");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

      
    }
}   
