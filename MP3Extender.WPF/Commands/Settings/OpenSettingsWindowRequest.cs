using System.Windows;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Settings;

namespace UI.Commands
{
	public class OpenSettingsWindowRequest
	{
		public Window Owner { get; set; }
	}

	public class OpenSettingsWindowCommandHandler : IRecipient<OpenSettingsWindowRequest>
	{
		private readonly SettingsViewModel _viewModel;

		public OpenSettingsWindowCommandHandler(SettingsViewModel viewModel) => _viewModel = viewModel;
		
		/// <inheritdoc />
		public void Receive(OpenSettingsWindowRequest message)
		{
			new SettingsWindow
				{
					Owner       = message.Owner,
					DataContext = _viewModel
				}
				.ShowDialog();
		}
	}
}