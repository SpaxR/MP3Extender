using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.Application;
using MP3Extender.Application.Services;
using MP3Extender.MetroUI.Views;

namespace MP3Extender.MetroUI
{
	/// <summary>Interaction logic for App.xaml</summary>
	[ExcludeFromCodeCoverage(Justification = "UI-Class")]
	public partial class App
	{
		public App()
		{
			var services = new ServiceCollection();

			services
				.AddViewModels()
				.AddLocalization()
				.AddSingleton<IMessenger>(StrongReferenceMessenger.Default)
				.AddSingleton<ISettings, Settings>()
				.AddSingleton<IFileSystemService, FileSystemService>();

			Ioc.Default.ConfigureServices(services.BuildServiceProvider());
		}

		/// <inheritdoc />
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			new MainWindow
			{
				DataContext = Ioc.Default.GetService<MainViewModel>()
			}.Show();
		}
	}
}