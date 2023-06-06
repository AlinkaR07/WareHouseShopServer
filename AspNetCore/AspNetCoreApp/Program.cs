using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using System.Text.Json;
using AspNetCoreApp.Data;
using Microsoft.AspNetCore.Builder;
using AspNetCoreApp.DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Hosting;
using NLog.Extensions.Logging;
using NLog;
using NLog.Web;
using Serilog;
using AspNetCoreApp.BLL;
using AspNetCoreApp.DAL.Repository;
using NuGet.Protocol.Core.Types;

var builder = WebApplication.CreateBuilder(args);

var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
NLog.LogManager.Shutdown();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
})
.UseNLog();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    builder =>
    {
        builder.WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod();

    });
});
//builder.Services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("Shop"));
// Add services to the container.
builder.Services.AddIdentity<User, IdentityRole>()
.AddEntityFrameworkStores<Context>();
builder.Services.AddDbContext<Context>();

var options = new JsonSerializerOptions()
{
    NumberHandling = JsonNumberHandling.AllowReadingFromString |
     JsonNumberHandling.WriteAsString
};

builder.Services.AddControllers().AddJsonOptions(x =>
x.JsonSerializerOptions.ReferenceHandler =
ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped(typeof(DbRepos));
builder.Services.AddScoped(typeof(DbCrudOperations));
builder.Services.AddTransient<IDbRepos, DbRepos>();
builder.Services.AddTransient<IDbCrud, DbCrudOperations>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "WareHouseApp";
    options.LoginPath = "/";
    options.AccessDeniedPath = "/";
    options.LogoutPath = "/";
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    //  401      
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var Context =
    scope.ServiceProvider.GetRequiredService<Context>();
    await FoodShopContextSeed.SeedAsync(Context);
    await IdentitySeed.CreateUserRoles(scope.ServiceProvider);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseCors();

app.MapControllers();

app.Run();