using MediatR;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace MP3Extender.WPF
{
	public class MessageNotificationAdapter<T> : IRecipient<T> where T : class
	{
		private readonly IMediator _mediator;

		public MessageNotificationAdapter(IMediator mediator) => _mediator = mediator;

		public void Receive(T message) => _mediator.Publish(message);
	}
}