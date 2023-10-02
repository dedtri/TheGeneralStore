using CompressedStaticFiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
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

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:7113",
                    ValidAudience = "https://localhost:7113",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });

            //JWT Token service
            builder.Services.AddTransient<TokenService>();

            // Adding CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowDevelopment", builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            builder.Services.AddCompressedStaticFiles();

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
            builder.Services.AddScoped<OrderRepository>();

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

            app.UseCompressedStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(app.Environment.ContentRootPath, "Uploads")),
                RequestPath = "/resources"
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}