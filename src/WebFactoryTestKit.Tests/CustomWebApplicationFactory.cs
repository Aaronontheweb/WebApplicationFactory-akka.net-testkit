// -----------------------------------------------------------------------
//  <copyright file="CustomWebApplicationFactory.cs" company="Akka.NET Project">
//      Copyright (C) 2013-2024 .NET Foundation <https://github.com/akkadotnet/akka.net>
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebFactoryTestKit.Tests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // disable the default configuration, which uses Postgres
        builder.UseSetting("AkkaSettings:PersistenceMode", "InMemory");
    }
}