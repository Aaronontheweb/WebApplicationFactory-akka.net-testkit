// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Petabridge, LLC Project">
//      Copyright (C) 2015-2024 Petabridge, LLC <https://petabridge.com/>
// </copyright>
// -----------------------------------------------------------------------

using Akka.Hosting;
using Microsoft.Extensions.Options;
using WebFactoryTestkit.App.Actors;
using WebFactoryTestkit.App.Configuration;

// get ASP.NET Environment
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{env}.json", true)
    .AddEnvironmentVariables();

builder.Services.Configure<AkkaSettings>(builder.Configuration.GetSection(nameof(AkkaSettings)));

builder.Services.AddAkka("MyActorSystem", (akkaBuilder, sp) =>
{
    akkaBuilder.AddAppActors();
    akkaBuilder.ConfigurePersistence(sp.CreateScope().ServiceProvider
        .GetRequiredService<IOptionsSnapshot<AkkaSettings>>().Value);
});

var app = builder.Build();

app.MapGet("/echo",
    (IRequiredActor<ReplyActor> actor) =>
        actor.ActorRef.Ask<HelloAck>(new Hello("Hello, Akka.NET!"), TimeSpan.FromSeconds(3)));

await app.RunAsync();