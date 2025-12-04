using Autofac.Extensions.DependencyInjection;
using BeautySalon.Common.Interfaces;
using BeautySalon.infrastructure;
using BeautySalon.RestApi.BackgroundJobs;
using BeautySalon.RestApi.Configurations.Autofacs;
using BeautySalon.RestApi.Configurations.ConnectionStrings;
using BeautySalon.RestApi.Configurations.Exceptions;
using BeautySalon.RestApi.Configurations.HangfireConfigurations;
using BeautySalon.RestApi.Configurations.JwtConfigs;
using BeautySalon.RestApi.Configurations.RegisterAdmin;
using BeautySalon.RestApi.Configurations.SwaggerConfigurations;
using BeautySalon.RestApi.Implementations;
using BeautySalon.RestApi.Implementations.FireBaseNotification;
using BeautySalon.RestApi.Implementations.GoogleCeridentials;
using BeautySalon.RestApi.Implementations.SMSSettings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var (configuration, connectionString) = ConnectionStringConfig
    .LoadConfigAndConnectionString(
    builder.Environment.EnvironmentName,
    builder.Environment.ContentRootPath);

builder.Configuration.AddConfiguration(configuration);
builder.Services.AddHttpClient();
builder.Host.AddAutofac();

builder.Services.AddDbContext<EFDataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSingleton<AdminInitializer>();
builder.Services.AddHostedService<AdminInitializerHostedService>();

builder.Services.HangFireConfigurationService(builder.Environment);

builder.Services.AddSingleton<IJwtSettingService>(sp =>
    new JwtSettingImplementation(
    builder.Environment.EnvironmentName,
    builder.Environment.ContentRootPath));

builder.Services.AddSingleton<ISMSSetting>(sm =>
    new SMSSettingsImplementation(
    builder.Environment.EnvironmentName,
    builder.Environment.ContentRootPath));


builder.Services.AddSingleton<IFireBaseSettingInfo>(fb =>
    new FireBaseSettingInfoImplimention(
    builder.Environment.EnvironmentName,
    builder.Environment.ContentRootPath));

builder.Services.AddSingleton<IGoogleCredentialRootPath>(google =>
    new GoogleCredentialRootPathImplemention(
    builder.Environment.EnvironmentName,
    builder.Environment.ContentRootPath));

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
app.UseAppHangfire();

app.RegisterHangfireJobs(builder.Environment);

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
