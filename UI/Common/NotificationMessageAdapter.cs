using System;
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

		/// <inheritdoc />
		public Task Handle(T notification, CancellationToken cancellationToken)
		{
			Console.WriteLine("Message to Notification");
			_messenger.Send(notification);
			return Task.CompletedTask;
		}
	}

	public class MessageNotificationAdapter<T> : IRecipient<T> where T : class
	{
		private readonly IMediator _mediator;

		public MessageNotificationAdapter(IMediator mediator) => _mediator = mediator;

		/// <inheritdoc />
		public void Receive(T message)
		{
			Console.WriteLine("Notification to Message");
			_mediator.Publish(message);
		}
	}




	public class MessageRequestAdapter<TRequest, TResponse> : IRecipient<TRequest> where TRequest : class
	{
		private readonly IMediator _mediator;

		public MessageRequestAdapter(IMediator mediator) => _mediator = mediator;

		/// <inheritdoc />
		public void Receive(TRequest message)
		{
			Task.Run(() => _mediator.Send(message));
		}
	}
}