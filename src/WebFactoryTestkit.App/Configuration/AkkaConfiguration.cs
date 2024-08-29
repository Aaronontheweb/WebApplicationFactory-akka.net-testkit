// -----------------------------------------------------------------------
//  <copyright file="AkkaConfiguration.cs" company="Akka.NET Project">
//      Copyright (C) 2013-2024 .NET Foundation <https://github.com/akkadotnet/akka.net>
// </copyright>
// -----------------------------------------------------------------------

using Akka.Hosting;
using Akka.Persistence.Hosting;
using Akka.Persistence.Sql.Config;
using Akka.Persistence.Sql.Hosting;
using LinqToDB;
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
        switch (settings.PersistenceMode)
        {
            case PersistenceMode.InMemory:
                builder
                    .WithInMemoryJournal()
                    .WithInMemorySnapshotStore();
                break;
            case PersistenceMode.SqlServer:
                builder
                    .WithSqlPersistence(
                        connectionString: settings.ConnectionString,
                        providerName: ProviderName.SqlServer2022,
                        tagStorageMode: TagMode.TagTable,
                        deleteCompatibilityMode: true,
                        useWriterUuidColumn: true,
                        autoInitialize: true);
                break;
            case PersistenceMode.Postgres:
                builder
                    .WithSqlPersistence(
                        connectionString: settings.ConnectionString,
                        providerName: ProviderName.PostgreSQL,
                        tagStorageMode: TagMode.TagTable,
                        deleteCompatibilityMode: true,
                        useWriterUuidColumn: true,
                        autoInitialize: true);
                break;
            case PersistenceMode.Sqlite:
                builder
                    .WithSqlPersistence(
                        connectionString: settings.ConnectionString,
                        providerName: ProviderName.SQLite,
                        tagStorageMode: TagMode.TagTable,
                        deleteCompatibilityMode: true,
                        useWriterUuidColumn: true,
                        autoInitialize: true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        return builder;
    }
}