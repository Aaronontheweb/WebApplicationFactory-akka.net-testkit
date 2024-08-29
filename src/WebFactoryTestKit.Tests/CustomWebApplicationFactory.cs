// -----------------------------------------------------------------------
//  <copyright file="CustomWebApplicationFactory.cs" company="Petabridge, LLC Project">
//      Copyright (C) 2015-2024 Petabridge, LLC <https://petabridge.com/>
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

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}