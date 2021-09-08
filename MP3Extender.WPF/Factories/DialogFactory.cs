using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MP3Extender.WPF.ViewModels;

namespace MP3Extender.WPF.Factories
{
	public interface IDialogFactory
	{
		public IFolderBrowserDialog CreateFolderBrowserDialog();

		public SettingsWindow CreateSettingsWindow();
	}

	public class DialogFactory : IDialogFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public DialogFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}


		/// <inheritdoc />
		public IFolderBrowserDialog CreateFolderBrowserDialog() => new FolderBrowserDialog();

		/// <inheritdoc />
		[ExcludeFromCodeCoverage(Justification = "Instantiates and Shows Window, blocks until closed")]
		public SettingsWindow CreateSettingsWindow()
		{
			var window = new SettingsWindow
			{
				Owner       = _serviceProvider.GetService<MainWindow>(),
				DataContext = _serviceProvider.GetRequiredService<ISettingsViewModel>()
			};

			return window;
		}
	}
}