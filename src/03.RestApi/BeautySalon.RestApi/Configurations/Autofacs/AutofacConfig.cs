using Autofac;
using Autofac.Extensions.DependencyInjection;
using BeautySalon.Application.Banners;
using BeautySalon.Common.Dtos;
using BeautySalon.Common.Interfaces;
using BeautySalon.infrastructure.Persistence.Banners;
using BeautySalon.RestApi.Implementations;
using BeautySalon.Services.Banners;
using static System.Net.Mime.MediaTypeNames;

namespace BeautySalon.RestApi.Configurations.Autofacs;

public static class AutofacConfig
{
    public static ConfigureHostBuilder AddAutofac(this ConfigureHostBuilder builder)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        builder.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
        {
            // از context اطلاعات محیط را بگیر
            var env = context.HostingEnvironment.EnvironmentName;
            var contentRoot = context.HostingEnvironment.ContentRootPath;

            containerBuilder.RegisterModule(new AutofacModule(env, contentRoot));
        });

        //builder.ConfigureContainer<ContainerBuilder>(builder =>
        //{

        //    builder.RegisterModule(new AutofacModule());
        //});
        return builder;
    }
}

public class AutofacModule : Module
{
    private readonly string _env;
    private readonly string _contentRootPath;
public AutofacModule(string env, string contentRootPath)
    {
        _env = env;
        _contentRootPath = contentRootPath;
    }

    protected override void Load(ContainerBuilder builder)
    {
        var serviceAssembly = typeof(BannerAppService).Assembly;
        var infrastructureAssembly = typeof(EFBannerRepository).Assembly;
        var commonAssembly = typeof(MediaDto).Assembly;
        var presentationAssembly = typeof(AutofacConfig).Assembly;
        var applicationAssembly = typeof(BannerCommandHandler).Assembly;


        builder.RegisterType<HttpContextAccessor>()
          .As<IHttpContextAccessor>()
          .SingleInstance();

        builder.RegisterAssemblyTypes(
            serviceAssembly,
            infrastructureAssembly,
            commonAssembly,
            presentationAssembly,
            applicationAssembly)
           .AsSelf()
           .AsImplementedInterfaces()
           .InstancePerLifetimeScope();

        builder.RegisterType<JwtSettingImplementation>()
            .As<IJwtSettingService>()
            .SingleInstance()
           .WithParameter("environmentName", _env)
           .WithParameter("contentRootPath", _contentRootPath);


        base.Load(builder);
    }
}