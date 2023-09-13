using ECommerceCore.Exceptions;
using ECommerceCore.Repositories;
using ECommerceCore.Services;
using ECommerceRepository;
using ECommerceService;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using ECommerceService.Services;
using Serilog;
using Serilog.Core;
using Microsoft.AspNetCore.Identity;
using ECommerceCore.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Logger logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
builder.Services.AddTransient<IProductSortingService,ProductSortingService>();
builder.Services.AddScoped<IShoppingCartItemService,ShoppingCartItemService>();
builder.Services.AddAutoMapper(typeof(VMMapper).Assembly);
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.ReportApiVersions = true;
    config.AssumeDefaultVersionWhenUnspecified = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
using (IServiceScope scope = app.Services.CreateScope())
{
    ILoggerFactory loggerFactory = (ILoggerFactory)scope.ServiceProvider.GetRequiredService(typeof(ILoggerFactory));
    app.ConfigureExceptionHandler(loggerFactory);
}
app.UseAuthorization();
await RoleInitializer.SeedRoles(app);
app.MapControllers();

app.Run();
