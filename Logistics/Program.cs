using Logistics.DBContext;
using Logistics.Mapper;
using Logistics.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Logistics.Schedules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
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

builder.Services.AddQuartz(q =>
{
    //q.UseMicrosoftDependencyInjectionJobFactory();
    var jobKey = new JobKey("DeadlineJob");
    q.AddJob<DeadlineJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
    .ForJob(jobKey)
    .WithIdentity("DeadlineJob-trigger")
    .WithCronSchedule("0 0/1 * * * ?"));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// WebSockets
app.UseWebSockets();
app.UseWebSocketMiddleware();
app.MapGrpcService<UserManagementService>();

app.MapControllers();

app.Run();
