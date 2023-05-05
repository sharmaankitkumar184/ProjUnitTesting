using Microsoft.EntityFrameworkCore;
using ProjUnitTesting.Models;
using ProjUnitTesting.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var ConnectionString = builder.Configuration.GetConnectionString("shopingCartDB");

builder.Services.AddDbContextPool<DataContext>(option =>
option.UseSqlServer(ConnectionString)
);
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
