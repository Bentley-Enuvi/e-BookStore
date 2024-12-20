using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using eBookStoreAPI.Application.ApiUtilities.Interfaces;
using eBookStoreAPI.Application.ApiUtilities.Services;
using eBookStoreAPI.Application.ApiUtilities.Shared;
using eBookStoreAPI.Application.UserAuthentication.Command.Login;
using eBookStoreAPI.Application.UserAuthentication.Command.Register;
using eBookStoreAPI.Infrastructure;
using eBookStoreAPI.Infrastructure.DataAccess;
using eBookStoreAPI.Infrastructure.DataAccess.Repositories;
using System.Net;
using System.Text;
using System.Text.Json;
using DataAccess.Repositories;
using eBookStoreAPI.Application.Book.Query.eBookStoreAPI.SearchBook;
using eBookStoreAPI.Application.Products.Command.AddBook;
using Npgsql;
using System.Data;

namespace eBookStoreAPI.Presentation.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure JwtSettings
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        // Register database context and repositories
        services.AddScoped<IDatabaseContext, DatabaseContext>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IPurchaseHistoryRepository, PurchaseHistoryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

        // Register services
        services.AddScoped<IPurchaseService, PurchaseService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IDbConnection>(sp =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return new NpgsqlConnection(connectionString);
        });


        // Add MediatR handlers
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<AddBookCommandHandler>();
            cfg.RegisterServicesFromAssemblyContaining<SearchBookQueryHandler>();
            cfg.RegisterServicesFromAssemblyContaining<LoginUserCommandHandler>();
            cfg.RegisterServicesFromAssemblyContaining<RegisterUserCommandHandler>();

        });

        // Configure JWT Authentication
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

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
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.ContentType = "application/json";
                    var response = new ApiResponse<string>(false, "Unauthorized access", "Access denied.");
                    return context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            };
        });

        // Add CORS policy
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        // Add SignalR
        services.AddSignalR();

        // Add Controllers
        services.AddControllers();
        services.AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssemblyContaining<Program>();
        });

        // Add Swagger with JWT Bearer support
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }
}

