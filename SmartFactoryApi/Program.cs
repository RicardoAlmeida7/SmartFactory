using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SmartFactoryApplication.Config.AutoMapper;
using SmartFactoryApplication.Inventory.Interfaces;
using SmartFactoryApplication.Inventory.Service;
using SmartFactoryData.Context;
using SmartFactoryData.Repositories.Inventory;
using SmartFactoryDomain.Interfaces.Repository.Inventory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region Database configuration

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

#endregion

#region AutoMapper configuration
var mappingConfig = new MapperConfiguration(profile =>
    {
        profile.AddProfile(new InventoryMappingProfile());
    }
);

IMapper mapper = mappingConfig.CreateMapper();

builder.Services.AddSingleton(mapper);
#endregion


#region IOC configuration
// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();

// Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
