// -----------------------------------------------------------------------
//  <copyright file="ReplyActorIntegrationSpecs.cs" company="Petabridge, LLC Project">
//      Copyright (C) 2015-2024 Petabridge, LLC <https://petabridge.com/>
// </copyright>
// -----------------------------------------------------------------------

using Akka.Actor;
using Akka.Hosting;
using Akka.TestKit.Xunit;
using Microsoft.Extensions.DependencyInjection;
using WebFactoryTestkit.App.Actors;
using Xunit.Abstractions;

namespace WebFactoryTestKit.Tests;

public class ReplyActorIntegrationSpecs : TestKit, IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ReplyActorIntegrationSpecs(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
        : base(factory.Services.GetRequiredService<ActorSystem>(), output)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ShouldPingActorsDirectly()
    {
        // arrange
        var registry = _factory.Services.GetRequiredService<ActorRegistry>();
        var echoActor = await registry.GetAsync<ReplyActor>();

        // act
        echoActor.Tell(new Hello("Hello, Akka.NET!"));

        // assert
        await ExpectMsgAsync<HelloAck>();
    }

    [Fact]
    public async Task ShouldPingActorsViaHttp()
    {
        // arrange
        using var client = _factory.CreateClient();
        var registry = _factory.Services.GetRequiredService<ActorRegistry>();
        var echoActor = await registry.GetAsync<ReplyActor>();
        var probe = CreateTestProbe();

        // act
        echoActor.Tell(new Subscribe(probe));
        var response = await client.GetAsync("echo");
        response.EnsureSuccessStatusCode();

        // assert
        await probe.ExpectMsgAsync<HelloAck>();
    }
}