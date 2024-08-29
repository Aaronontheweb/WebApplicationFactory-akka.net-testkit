// -----------------------------------------------------------------------
//  <copyright file="Messages.cs" company="Petabridge, LLC Project">
//      Copyright (C) 2015-2024 Petabridge, LLC <https://petabridge.com/>
// </copyright>
// -----------------------------------------------------------------------

namespace WebFactoryTestkit.App.Actors;

public sealed record Hello(string Msg);

public sealed record HelloAck(int HelloCount);

public sealed record Subscribe(IActorRef Subscriber);

public sealed record Unsubscribe(IActorRef Subscriber);