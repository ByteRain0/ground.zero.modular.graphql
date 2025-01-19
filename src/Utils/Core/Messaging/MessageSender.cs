using MassTransit;

namespace Core.Messaging;

internal class MessageSender : IMessageSender
{
    private readonly IBus _bus;

    public MessageSender(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken) where T : class
    {
        await _bus.Publish(message, cancellationToken);
    }
}