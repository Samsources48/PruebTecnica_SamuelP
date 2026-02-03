using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Features.Seguridad.Interfaces;
using Infrastructure.Services;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SqlDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Services
            services.AddScoped<IJwtService, JwtService>();

            // JWT Configuration
            var jwtKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing");
            var jwtIssuer = configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer is missing");
            var jwtAudience = configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience is missing");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            return services;
        }
    }
}
