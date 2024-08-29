// -----------------------------------------------------------------------
//  <copyright file="Messages.cs" company="Akka.NET Project">
//      Copyright (C) 2013-2024 .NET Foundation <https://github.com/akkadotnet/akka.net>
// </copyright>
// -----------------------------------------------------------------------

namespace WebFactoryTestkit.App.Actors;

public sealed record Hello(string Msg);
public sealed record HelloAck(int HelloCount);

public sealed record Subscribe(IActorRef Subscriber);
public sealed record Unsubscribe(IActorRef Subscriber);