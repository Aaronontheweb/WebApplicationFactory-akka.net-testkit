# `WebApplicationFactory` + Akka.NET TestKit Sample

Shows to integrate the vanilla `Akka.TestKit.XUnit` package with `Microsoft.AspNetCore.Mvc.Testing` and `Microsoft.AspNetCore.TestHost`.

## Rationale

We still use [Akka.Hosting](https://github.com/akkadotnet/Akka.Hosting) to configure the `ActorSystem` and all of the Akka.NET componentry, but we chose not to use the Akka.Hosting.TestKit alongside the `WebApplicationFactory` because both of those components have very strong opinions about how to best manage the `IHostBuilder` and its lifecycle. 


It ended up being simpler just to use the regular `TestKit` and pass in the `ActorSystem` into its CTOR via the following:

```csharp
public class ReplyActorIntegrationSpecs : TestKit, IClassFixture<CustomWebApplicationFactory<Program>>
{
    private CustomWebApplicationFactory<Program> _factory;

    public ReplyActorIntegrationSpecs(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
        : base(factory.Services.GetRequiredService<ActorSystem>(), output)
    {
        _factory = factory;
    }

    // test methods

}
```

This uses xUnit's `IClassFixture` to re-use the same `ActorSystem` across all test instances - but you could redesign it to work as a collection fixture or an individual test fixture. We leave that as an exercise to the reader.