using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using Microsoft.Extensions.Options;
using LoanProject.Data.DbContect;
using Microsoft.EntityFrameworkCore;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Implementations;
using LoanProject.Repository.Abstractions;
using LoanProject.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#pragma warning disable CS0618 
Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Verbose()
      .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
      .Enrich.FromLogContext()
      .WriteTo.MSSqlServer(
          connectionString: builder.Configuration.GetConnectionString("connectionString"),
          tableName: "Logs",
          autoCreateSqlTable: true,
          columnOptions: new ColumnOptions(),
          restrictedToMinimumLevel: LogEventLevel.Verbose)
      .CreateLogger();
#pragma warning restore CS0618 
builder.Services.AddScoped<IPasswordHasher,IPasswordHasher>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped(typeof(ILoanServiceRepository<>), typeof(LoanServiceRepository<>));
builder.Services.AddTransient<LoggingMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();

app.Run();
