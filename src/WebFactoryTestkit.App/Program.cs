using Akka.Hosting;
using WebFactoryTestkit.App.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAkka("MyActorSystem", (akkaBuilder, sp) =>
{
    akkaBuilder.AddAppActors();
});

var app = builder.Build();
app.MapControllers();
await app.RunAsync();