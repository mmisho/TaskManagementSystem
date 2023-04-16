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

namespace TaskManagement.API
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }
        public string ConnectionString { get;} 

        public StartUp(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.ConnectionString = configuration.GetConnectionString("TaskManagementDb");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository,RoleRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
