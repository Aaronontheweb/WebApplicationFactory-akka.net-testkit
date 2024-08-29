namespace WebFactoryTestkit.App.Actors;

public class EchoActor : ReceiveActor
{
    private readonly ILoggingAdapter _log = Context.GetLogger();
    private int _helloCounter;
    
    public EchoActor()
    {
        Receive<Hello>(message =>
        {
           _log.Info("{0} {1}", message, _helloCounter++);
           Sender.Tell(new HelloAck(_helloCounter));
        });
    }
}