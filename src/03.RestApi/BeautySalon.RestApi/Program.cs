using Autofac.Extensions.DependencyInjection;
using BeautySalon.infrastructure;
using BeautySalon.RestApi.Configurations.Autofacs;
using BeautySalon.RestApi.Configurations.Exceptions;
using BeautySalon.RestApi.Configurations.SwaggerConfigurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.Json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var privateConfigPath = Path.Combine(builder.Environment.ContentRootPath, "Private", "secrets.json");

if (File.Exists(privateConfigPath))
{
    builder.Configuration.AddEnvironmentVariables();
    Console.WriteLine($" Secrets file found at:!!!!! ");
}
else
{
    Console.WriteLine($" Secrets file not found at: {privateConfigPath}");
}

// Add services to the container.
builder.Host.AddAutofac();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

connectionString = connectionString
    .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "")
    .Replace("${DB_PASS}", Environment.GetEnvironmentVariable("DB_PASS") ?? "");


builder.Services.AddDbContext<EFDataContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddControllers();

builder.Services.AddSwaggerConfigGen();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCustomExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    options.RoutePrefix = "swagger";
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});


app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
