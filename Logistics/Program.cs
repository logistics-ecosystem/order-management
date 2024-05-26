using Logistics.DBContext;
using Logistics.Models;
using Logistics.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("OrdersDatabase"));

//builder.Services.AddSingleton<IMongoDBSettings>(sp =>
//            sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

//builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
//            new MongoClient(builder.Configuration.GetValue<string>("OrdersDatabase:ConnectionString")));

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
