namespace Core.Messaging;

public interface IMessageSender
{
    Task PublishMessageAsync<T> (T message, CancellationToken cancellationToken = default) where T : class;
}