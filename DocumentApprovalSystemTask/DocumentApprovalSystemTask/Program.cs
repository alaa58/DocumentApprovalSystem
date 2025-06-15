//
using System.Text;
using DocumentApprovalSystemTask.Application.Commands;
using DocumentApprovalSystemTask.Application.Interfaces;
using DocumentApprovalSystemTask.Application.Mappings;
using DocumentApprovalSystemTask.Domain.Entities;
using DocumentApprovalSystemTask.Domain.Interfaces;
using DocumentApprovalSystemTask.Infrastructure.Data;
using DocumentApprovalSystemTask.Infrastructure.Repositories;
using DocumentApprovalSystemTask.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace DocumentApprovalSystemTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            //DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //mapper
            builder.Services.AddAutoMapper(typeof(AuthProfile).Assembly);
            builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));

            builder.Services.AddHttpContextAccessor();



            //AuthService
            builder.Services.AddScoped<IAuthService, AuthService>();

            //MediatR
            builder.Services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(AddFileCommandHandler).Assembly));

            builder.Services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(AddFileApprovalCommand).Assembly));


            //Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            // JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                Console.WriteLine("JWT KEY: " + builder.Configuration["JWT:Key"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["JWT:Iss"],
                    ValidAudience = builder.Configuration["JWT:Aud"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });
            // Swagger with JWT Auth
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Document Approval System API",
                    Description = "API for File Tracker"
                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer ey...\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication(); 

            app.UseAuthorization();

            app.UseStaticFiles(); 

            app.MapControllers();

            app.Run();
        }
    }
}
