// -----------------------------------------------------------------------
//  <copyright file="TimerActor.cs" company="Petabridge, LLC Project">
//      Copyright (C) 2015-2024 Petabridge, LLC <https://petabridge.com/>
// </copyright>
// -----------------------------------------------------------------------

using Akka.Hosting;

namespace WebFactoryTestkit.App.Actors;

public class TimerActor : ReceiveActor, IWithTimers
{
    private readonly IActorRef _helloActor;

    public TimerActor(IRequiredActor<ReplyActor> helloActor)
    {
        _helloActor = helloActor.ActorRef;
        Receive<string>(message => { _helloActor.Tell(new Hello(message)); });

        // ignore
        Receive<HelloAck>(_ => { });
    }

    public ITimerScheduler Timers { get; set; } = null!; // gets set by Akka.NET

    protected override void PreStart()
    {
        Timers.StartPeriodicTimer("hello-key", "hello", TimeSpan.FromSeconds(1));
    }
}