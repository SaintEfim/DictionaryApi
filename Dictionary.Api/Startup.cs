using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Dictionary.Persistence;
using Dictionary.Domain.Interface;
using Dictionary.Persistence.Repository;
using Dictionary.Domain.Entity;
using Dictionary.Seeding;

namespace Dictionary.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            // Добавьте строку подключения к вашей базе данных
            services.AddDbContext<GermanRussianDictionaryDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PSQL")).LogTo(Console.WriteLine));

            // Добавьте сервисы репозитория
            services.AddScoped<IGermanRussianDictionaryRepository, GermanRussianDictionaryRepository>();
            services.AddScoped<Seeder>();
            services.AddAutoMapper(typeof(Startup));
            // Добавьте конфигурацию Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dctionary API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
                    seeder.SeedData().Wait();
                }
            }

            app.UseRouting();

            // Подключите Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store V1");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
