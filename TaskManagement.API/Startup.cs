#nullable disable
using Application.UserManagement.Commands.CreateUser;
using Domain.RoleManagement;
using Domain.RoleManagement.Repositories;
using Domain.Shared;
using Domain.UserManagement;
using Domain.UserManagement.Repository;
using Infrastructure.DataAcces;
using Infrastructure.Repositories.RoleManagement;
using Infrastructure.Repositories.UserManagement;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Domain.TaskManagement.Repositories;
using Infrastructure.Repositories.TaskManagement;
using Application.Shared.Enums;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Shared.Options;
using Infrastructure.Shared;

namespace TaskManagement.API
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }
        public string ConnectionString { get; }

        public StartUp(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.ConnectionString = configuration.GetConnectionString("TaskManagementDb");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescription => apiDescription.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the bearer scheme.",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        new string[] { }
                    },
                });
            });

            services.AddDbContext<EFDbContext>(opt =>
                opt.UseNpgsql(this.ConnectionString));

            services.AddMediatR(new[]
            {
                typeof(CreateUser).GetTypeInfo().Assembly,
            });
            services.AddIdentity<User, Role>(x =>
            {
                x.Password.RequireLowercase = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequiredLength = 6;
                x.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<EFDbContext>()
            .AddDefaultTokenProviders();
            services.AddValidatorsFromAssembly(typeof(CreateUser).GetTypeInfo().Assembly);

            ConfgiureJwt(services, Configuration);
            services.AddAuthorization(option =>
            {
                option.AddPolicy("TaskCreate", policy => policy.RequireClaim("Task_Permission", ClaimType.Task_Create.ToString()));
                option.AddPolicy("TaskUpdate", policy => policy.RequireClaim("Task_Permission", ClaimType.Task_Update.ToString()));
                option.AddPolicy("TaskDelete", policy => policy.RequireClaim("Task_Permission", ClaimType.Task_Delete.ToString()));
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
        public static void ConfgiureJwt(IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(JwtOptions.ConfigSection).Get<JwtOptions>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Audience = jwtOptions.ValidAudience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.ValidIssuer,
                    ValidAudiences = jwtOptions.ValidAudiences,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                };
            });
        }
    }
}
