using Autofac;
using Autofac.Extensions.DependencyInjection;
using BeautySalon.Application.Banners;
using BeautySalon.Common.Dtos;
using BeautySalon.infrastructure.Persistence.Banners;
using BeautySalon.Services.Banners;

namespace BeautySalon.RestApi.Configurations.Autofacs;

public static class AutofacConfig
{
    public static ConfigureHostBuilder AddAutofac(this ConfigureHostBuilder builder)
    {

        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterModule(new AutofacModule());
        });
        return builder;
    }
}

public class AutofacModule : Module
{
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

        base.Load(builder);
    }
}