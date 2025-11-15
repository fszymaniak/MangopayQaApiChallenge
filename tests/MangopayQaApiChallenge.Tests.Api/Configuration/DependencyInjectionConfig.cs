using Autofac;
using MangoPay.SDK;

namespace MangopayQaApiChallenge.Tests.Api.Configuration;

public class DependencyInjectionConfig
{
    public static IContainer BuildContainer(MangoPayApi api)
    {
        var builder = new ContainerBuilder();

        // Register MangoPayApi
        builder.RegisterInstance(api).AsSelf().SingleInstance();

        // Register Factories
        builder.RegisterType<UserFactory>().As<IUserFactory>().SingleInstance();
        builder.RegisterType<WalletFactory>().As<IWalletFactory>().SingleInstance();
        builder.RegisterType<CardFactory>().As<ICardFactory>().SingleInstance();
        builder.RegisterType<PayInFactory>().As<IPayInFactory>().SingleInstance();

        // Register Steps
        builder.RegisterType<UserPayerSteps>().AsSelf().SingleInstance();
        builder.RegisterType<WalletSteps>().AsSelf().SingleInstance();
        builder.RegisterType<CardSteps>().AsSelf().SingleInstance();

        // Register Validators
        builder.RegisterType<StatusCodeValidator>().As<IStatusCodeValidator>().SingleInstance();
        builder.RegisterType<IdValidator>().As<IIdValidator>().SingleInstance();

        // Register Drivers
        builder.RegisterType<RestSharpDriver>().As<IRestSharpDriver>().SingleInstance();

        // Register Providers
        builder.RegisterType<PathProvider>().As<IPathProvider>().SingleInstance();
        builder.RegisterType<RequestDetailsProvider>().As<IRequestDetailsProvider>().SingleInstance();

        // Register Randomizers
        builder.RegisterType<UserValuesRandomizer>().As<IUserValuesRandomizer>().SingleInstance();

        return builder.Build();
    }
}
