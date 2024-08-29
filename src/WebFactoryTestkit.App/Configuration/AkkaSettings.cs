// -----------------------------------------------------------------------
//  <copyright file="AkkaSettings.cs" company="Petabridge, LLC Project">
//      Copyright (C) 2015-2024 Petabridge, LLC <https://petabridge.com/>
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