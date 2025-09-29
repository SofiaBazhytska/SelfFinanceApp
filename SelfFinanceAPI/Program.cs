using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SelfFinance.Core.Data;
using SelfFinance.Core.Repositories;
using SelfFinance.Core.Services;
using SelfFinanceAPI.Filters;
using System.Text;
using SelfFinanceWeb.Authentication;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var token = builder.Configuration["AppSettings:Token"];

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<SelfFinanceAPIContext>(options =>
        options.UseInMemoryDatabase("TestDb"));
}
else
{
    var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_SELF_FINANCE_API");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new Exception("Environment variable DB_CONNECTION_SELF_FINANCE_API is not set.");
    }

    builder.Services.AddDbContext<SelfFinanceAPIContext>(options =>
        options.UseSqlServer(connectionString));
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSelfFinanceWeb", policy =>
    {
        policy.WithOrigins("https://localhost:7196")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true
        };
    });


builder.Services.AddScoped<OperationRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<OperationService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSelfFinanceWeb");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SelfFinanceAPIContext>();
    db.Database.Migrate();
}

app.Run();
public partial class Program { }
