using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mysqltest.Core.Providers;
using mysqltest.Helpers;
using mysqltest.Mapping;
using mysqltest.Models;
using mysqltest.Models.Helpers;
using mysqltest.Services;
using System;
using System.Text.Json.Serialization;
using WebApi.Authorization;

namespace mysqltest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionString:ClubsDb"];

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));

            services.AddDbContext<ClubsContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, serverVersion)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

            services.AddSingleton(sp => MapperInitializer.GetMapper());
            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new EnumEntityBinderProvider());
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddSwaggerGen();
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IStudentService, UserService>();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ClubsContext clubsContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //clubsContext.Database.EnsureDeleted();
                //clubsContext.Database.EnsureCreated();
                DatabaseInitializer.InitializeIfNeeded(clubsContext);
            }

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
        }
    }
}