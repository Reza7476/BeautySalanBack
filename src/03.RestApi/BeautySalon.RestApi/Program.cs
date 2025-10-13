using Autofac.Extensions.DependencyInjection;
using BeautySalon.infrastructure;
using BeautySalon.RestApi.Configurations.Autofacs;
using BeautySalon.RestApi.Configurations.ConnectionStrings;
using BeautySalon.RestApi.Configurations.Exceptions;
using BeautySalon.RestApi.Configurations.SwaggerConfigurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var (configuration, connectionString) = ConnectionStringConfig.LoadConfigAndConnectionString(builder.Environment.EnvironmentName, builder.Environment.ContentRootPath);

builder.Configuration.AddConfiguration(configuration);

// Add services to the container.
builder.Host.AddAutofac();

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
