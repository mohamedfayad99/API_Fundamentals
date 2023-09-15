using AutoMapper;
using CityInfo.Services;
using CityInfo.DBContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;



using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CityInfo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("LogFile/cityinfo.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();               

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Logging.ClearProviders();
            //builder.Logging.AddConsole();

            builder.Host.UseSerilog();

            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            }
            ).AddNewtonsoftJson()
            .AddXmlDataContractSerializerFormatters();            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ICityInfoRepositry,CityInfoRepositry>();
            builder.Services.AddScoped<IPointInfoRepositry, PointInfoRepositry>();

            builder.Services.AddDbContext<ApplicationDb>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authenticate:Issuer"],
                    ValidAudience = builder.Configuration["Authenticate:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(builder.Configuration["Authenticate:SecretForKey"]))

                };
            });

            #if DEBUG
            builder.Services.AddTransient<ImailService,LocalmailService>();
            #else
            builder.Services.AddTransient<ImailService,CloudmailService>();
            #endif

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else if (app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
  

            app.Run();
        }
    }
}