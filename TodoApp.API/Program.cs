using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TodoApp.API.Mappers;
using TodoApp.API.Mappers.Interfaces;
using TodoApp.API.Services;
using TodoApp.API.Services.Interfaces;
using TodoApp.BLL;
using TodoApp.DAL;

namespace TodoApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IJwtService, JwtService>();
            builder.Services.AddTransient<IAccountMapper, AccountMapper>();
            builder.Services.AddTransient<ITodoItemMapper, TodoItemMapper>();

            builder.Services.ConfigureBusinessLayerServices();
            builder.Services.ConfigureDataLayerServices(builder.Configuration);

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//musu auth schema bus kas nesa jwt, tas yra prisistates
              .AddJwtBearer(
              options =>
              {
                  var secretKey = builder.Configuration.GetSection("Jwt:Key").Value;
                  var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                      ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                      IssuerSigningKey = key
                  };
              });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt => {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API Teaching Demo",
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header is using Bearer scheme. \r\n\r\n" +
                               "Enter token. \r\n\r\n" +
                               "Example: \"d5f41g85d1f52a\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                opt.AddSecurityDefinition("Bearer", securitySchema);
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
