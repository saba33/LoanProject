using LoanProject.Data.DbContect;
using LoanProject.Repository.Abstractions;
using LoanProject.Repository.Implementations;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and your token",
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
     });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
    builder => builder.WithOrigins("http://localhost:3000", "http://localhost:5000")
   .AllowAnyMethod()
   .AllowAnyHeader());
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
builder.Services.AddScoped<ICRMService, CRMService>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ILoanService, LoanService>();
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
