using Autofac.Extensions.DependencyInjection;
using BeautySalon.Common.Interfaces;
using BeautySalon.infrastructure;
using BeautySalon.RestApi.Configurations.Autofacs;
using BeautySalon.RestApi.Configurations.ConnectionStrings;
using BeautySalon.RestApi.Configurations.Exceptions;
using BeautySalon.RestApi.Configurations.JwtConfigs;
using BeautySalon.RestApi.Configurations.RegisterAdmin;
using BeautySalon.RestApi.Configurations.SwaggerConfigurations;
using BeautySalon.RestApi.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var (configuration, connectionString) = ConnectionStringConfig.LoadConfigAndConnectionString(
    builder.Environment.EnvironmentName,
    builder.Environment.ContentRootPath);

builder.Configuration.AddConfiguration(configuration);

builder.Host.AddAutofac();

builder.Services.AddDbContext<EFDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSingleton<AdminInitializer>();
builder.Services.AddHostedService<AdminInitializerHostedService>();

builder.Services.AddSingleton<IJwtSettingService>(sp =>
    new JwtSettingImplementation(builder.Environment.EnvironmentName, builder.Environment.ContentRootPath));

builder.Services.AddJwtAuthentication();

builder.Services.AddControllers();
builder.Services.AddSwaggerConfigGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCustomExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    c.RoutePrefix = "swagger";
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
