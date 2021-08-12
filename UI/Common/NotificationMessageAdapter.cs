using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace UI
{
	public class NotificationMessageAdapter<T> : INotificationHandler<T>
		where T : class, INotification
	{
		private readonly IMessenger _messenger;

		public NotificationMessageAdapter(IMessenger messenger) => _messenger = messenger;

		public Task Handle(T notification, CancellationToken cancellationToken)
		{
			_messenger.Send(notification);
			return Task.CompletedTask;
		}
	}

	public class MessageNotificationAdapter<T> : IRecipient<T> where T : class
	{
		private readonly IMediator _mediator;

		public MessageNotificationAdapter(IMediator mediator) => _mediator = mediator;

		public void Receive(T message) => _mediator.Publish(message);
	}


	public class MessageRequestAdapter<TRequest, TResponse> : IRecipient<TRequest> where TRequest : class
	{
		private readonly IMediator _mediator;

		public MessageRequestAdapter(IMediator mediator) => _mediator = mediator;

		public void Receive(TRequest message) => Task.Run(() => _mediator.Send(message));
	}
}