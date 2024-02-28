using FluentValidation;
using InventorySystemApp.API.Configuration;
using InventorySystemApp.Data;
using InventorySystemApp.Data.IRepository;
using InventorySystemApp.Data.Repository;
using InventorySystemApp.Model.Dtos;
using InventorySystemApp.Service.IService;
using InventorySystemApp.Service.IServices;
using InventorySystemApp.Service.Service;
using InventorySystemApp.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Database Config
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("default")));

//Service Config
builder.Services.AddScoped(typeof(IRepositoryGeneric<>), typeof(RepositoryGeneric<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTransactionRepository, ProductTransactionRepository>();
builder.Services.AddScoped<IInventoryTransactionRepository, InventoryTransactionRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();
builder.Services.AddScoped<IProductTransactionService, ProductTransactionService>();

//FluentValidation config
builder.Services.AddScoped<IValidator<InventoryDto>, InventoryDtoValidator>();
builder.Services.AddScoped<IValidator<ProductDto>, ProductDtoValidator>();
builder.Services.AddScoped<IValidator<SellProductDto>, SellProductDtoValidator>();
builder.Services.AddScoped<IValidator<PurchaseInventoryDto>, PurchaseInventoryDtoValidator>();
builder.Services.AddScoped<IValidator<ProduceProductDto>, ProduceProductDtoValidator>();


//Automapper config
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());// Assembly.GetExecutingAssembly());
//AutoMapperConfig.Configure(builder.Services);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


FluentValidationConfig.Configure();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
