# `WebApplicationFactory` + Akka.NET TestKit Sample

Shows to integrate the vanilla `Akka.TestKit.XUnit` package with `Microsoft.AspNetCore.Mvc.Testing` and `Microsoft.AspNetCore.TestHost`.

## Rationale

We still use [Akka.Hosting](https://github.com/akkadotnet/Akka.Hosting) to configure the `ActorSystem` and all of the Akka.NET componentry, but we chose not to use the Akka.Hosting.TestKit alongside the `WebApplicationFactory` because both of those components have very strong opinions about how to best manage the `IHostBuilder` and its lifecycle. 


It ended up being simpler just to use the regular `TestKit` and pass in the `ActorSystem` into its CTOR via the following:

```csharp
public class ReplyActorIntegrationSpecs : TestKit
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    
    public ReplyActorIntegrationSpecs(ITestOutputHelper output)
        : this(new CustomWebApplicationFactory<Program>(), output)
    {
    }
    
    private ReplyActorIntegrationSpecs(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
        : base(factory.Services.GetRequiredService<ActorSystem>(), output)
    {
        _factory = factory;
    }

    // test methods

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            try
            {
                _factory.Dispose();
                base.Dispose(disposing);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
    }
}
```

We use a public CTOR to satisfy the xUnit test runner, but the private CTOR is used to instantiate our `CustomWebApplicationFactory<Program>` and resolve the `ActorSystem` started by Akka.Hosting, which gets passed into our test context via the `base` class constructor call. This will create an instance of this `ActorSystem` + `CustomWebApplicationFactory<Program>` per each test, which is the cleanest way to do it.

We also override the Akka.TestKit's `Dispose(bool disposing)` method to explicitly terminate the `CustomWebApplicationFactory<Program>`.