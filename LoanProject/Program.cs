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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using AutoMapper;
using LoanProject.Services.Infrastructure.WorkerServices;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
    builder => builder.AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader()
   .AllowCredentials());
});
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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWTConfiguration:Secret").Value))
    };
});

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<Random>();
builder.Services.AddScoped<LoanStatusService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();

app.Run();
