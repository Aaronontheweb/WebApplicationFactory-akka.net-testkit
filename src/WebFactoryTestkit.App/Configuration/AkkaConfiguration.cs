// -----------------------------------------------------------------------
//  <copyright file="AkkaConfiguration.cs" company="Akka.NET Project">
//      Copyright (C) 2013-2024 .NET Foundation <https://github.com/akkadotnet/akka.net>
// </copyright>
// -----------------------------------------------------------------------

using Akka.Hosting;
using WebFactoryTestkit.App.Actors;

namespace WebFactoryTestkit.App.Configuration;

public static class AkkaConfiguration
{
    public static AkkaConfigurationBuilder AddAppActors(this AkkaConfigurationBuilder builder)
    {
        builder.WithActors((system, registry, resolver) =>
            {
                var helloActor = system.ActorOf(Props.Create(() => new EchoActor()), "hello-actor");
                registry.Register<EchoActor>(helloActor);
            })
            .WithActors((system, registry, resolver) =>
            {
                var timerActorProps =
                    resolver.Props<TimerActor>(); // uses Msft.Ext.DI to inject reference to helloActor
                var timerActor = system.ActorOf(timerActorProps, "timer-actor");
                registry.Register<TimerActor>(timerActor);
            });
        return builder;
    }

    public static AkkaConfigurationBuilder ConfigurePersistence(this AkkaConfigurationBuilder builder,
        AkkaSettings settings)
    {
        return builder;
    }
}