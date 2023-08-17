using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TheGeneralStore.Backend.Database;
using TheGeneralStore.Backend.Database.Repositories;
using TheGeneralStore.Backend.WebAPI.Persistence.Services;

namespace TheGeneralStore.Backend.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adding CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowDevelopment", builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            // Adding HttpContextAccessor
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Adding Connection
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContextPool<DatabaseContext>(options => options.EnableSensitiveDataLogging(false).UseMySql(connection, ServerVersion.AutoDetect(connection)), 64);

            // Adding UnitOfWork
            builder.Services.AddScoped<UnitOfWork>();

            // Adding AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            // Adding Repositories
            builder.Services.AddScoped<CustomerRepository>();
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<ImageRepository>();
            builder.Services.AddScoped<CategoryRepository>();

            //Adding Services
            builder.Services.AddScoped<ImageService>();

            // Adding MVC
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            });

            // Adding Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Enables Cors
            app.UseCors("AllowDevelopment");

            // Enable Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheGeneralStore.Backend.WebAPI V1");
                });
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(app.Environment.ContentRootPath, "Uploads")),
                RequestPath = "/Resources"
            });

            app.MapControllers();

            app.Run();
        }
    }
}