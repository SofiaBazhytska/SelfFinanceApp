using Microsoft.EntityFrameworkCore;
using SelfFinance.Core.Data;
using SelfFinance.Core.Repositories;
using SelfFinance.Core.Services;
using SelfFinanceAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
        policy.WithOrigins("https://selffinance-web-bhbgajeugsfueuar.polandcentral-01.azurewebsites.net")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<OperationRepository>();
builder.Services.AddScoped<CategoryRepository>();

builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<OperationService>();
builder.Services.AddScoped<CategoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSelfFinanceWeb");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SelfFinanceAPIContext>();
    db.Database.Migrate();
}

app.Run();
public partial class Program { }
