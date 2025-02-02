using AutoMapper;
using Scalar.AspNetCore;
using SmartFactoryApplication.Config.AutoMapper;
using SmartFactoryApplication.Inventory.Interfaces;
using SmartFactoryApplication.Inventory.Service;
using SmartFactoryDomain.Interfaces.Repository.Inventory;
using SmartFactoryInfrastructure.Data.Repositories.Inventory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IMaterialRepository, MaterialRepository>();

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
