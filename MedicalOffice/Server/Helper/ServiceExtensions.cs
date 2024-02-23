﻿
using MedicalOffice.Server.Context;
using MedicalOffice.Server.Helpers;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MedicalOffice.Server.Helper;

public static class ServiceExtensions
{
    public static void AddBaseServices(this IServiceCollection services, WebApplicationBuilder builder)
    {

        services.AddControllersWithViews().AddJsonOptions(x =>
                 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

        services.AddScoped<ProtectPassword>();
        services.AddScoped<FileHelper>();
        services.AddHttpContextAccessor();
    }

    public static void AddSqlServerServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connection = builder.Configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connection));
    }

    public static void AddCorsServices(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("https://localhost:7084") // Allow requests from this origin
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });
    }

    public static void AddAuthServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
             ClockSkew = TimeSpan.Zero,
         });
    }

}
