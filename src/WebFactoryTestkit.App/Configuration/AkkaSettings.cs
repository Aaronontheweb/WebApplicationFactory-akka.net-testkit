// -----------------------------------------------------------------------
//  <copyright file="AkkaSettings.cs" company="Akka.NET Project">
//      Copyright (C) 2013-2024 .NET Foundation <https://github.com/akkadotnet/akka.net>
// </copyright>
// -----------------------------------------------------------------------

namespace WebFactoryTestkit.App.Configuration;

public enum PersistenceMode
{
    InMemory,
    SqlServer,
    Postgres,
    Sqlite
}

public class AkkaSettings
{
    public PersistenceMode PersistenceMode { get; set; } = PersistenceMode.Postgres;
    
    public string ConnectionString { get; set; } = "";
}