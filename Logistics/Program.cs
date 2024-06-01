using Logistics.DBContext;
using Logistics.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//MongoDB
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("OrdersDatabase"));

builder.Services.AddSingleton<IOrderService, OrderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();

//PostgreSQL
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IPostgreService, PostgreService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGrpcService<ToDoAnalyticsService>(); 

// WebSockets
app.UseWebSockets();
app.UseWebSocketMiddleware();

app.MapControllers();

app.Run();
