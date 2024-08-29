namespace WebFactoryTestkit.App.Actors;

public class ReplyActor : ReceiveActor
{
    private readonly ILoggingAdapter _log = Context.GetLogger();
    private int _helloCounter;
    
    private readonly HashSet<IActorRef> _subscribers = new HashSet<IActorRef>();
    
    public ReplyActor()
    {
        Receive<Hello>(message =>
        {
           _log.Info("Recv: {0} {1}", message, _helloCounter++);
           var reply = new HelloAck(_helloCounter);
           Sender.Tell(reply);
           
           foreach(var s in _subscribers)
           {
               s.Tell(reply);
           }
        });
        
        Receive<Subscribe>(message =>
        {
            _subscribers.Add(message.Subscriber);
            
            // automatically unsub dead actors
            Context.WatchWith(message.Subscriber, new Unsubscribe(message.Subscriber));
        });
        
        Receive<Unsubscribe>(message =>
        {
            _subscribers.Remove(message.Subscriber);
        });
    }
}