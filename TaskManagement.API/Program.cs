using Infrastructure.DataAcces;
using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using TaskManagement.API;

var builder = WebApplication.CreateBuilder(args);

var startup = new StartUp(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
var context = serviceScope.ServiceProvider.GetService<EFDbContext>();
EFDbInit.Initialize(context!, serviceScope);

startup.Configure(app, builder.Environment);

app.Run();

