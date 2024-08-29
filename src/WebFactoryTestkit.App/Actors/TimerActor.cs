using Akka.Hosting;

namespace WebFactoryTestkit.App.Actors;

public class TimerActor : ReceiveActor, IWithTimers
{
    private readonly IActorRef _helloActor;

    public TimerActor(IRequiredActor<EchoActor> helloActor)
    {
        _helloActor = helloActor.ActorRef;
        Receive<string>(message =>
        {
            _helloActor.Tell(new Hello(message));
        });

        // ignore
        Receive<HelloAck>(_ => { });
    }

    protected override void PreStart()
    {
        Timers.StartPeriodicTimer("hello-key", "hello", TimeSpan.FromSeconds(1));
    }

    public ITimerScheduler Timers { get; set; } = null!; // gets set by Akka.NET
}