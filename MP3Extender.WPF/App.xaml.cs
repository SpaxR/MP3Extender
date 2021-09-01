using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Configuration;
using UI.Controls.FileList;
using UI.Dialogs;
using UI.Localization;
using UI.Settings;

namespace UI
{
	/// <summary>Interaction logic for App.xaml</summary>
	[ExcludeFromCodeCoverage]
	public partial class App
	{
		public App()
		{
			var services = ConfigureServices(new ServiceCollection());
			Ioc.Default.ConfigureServices(services);
		}

		/// <inheritdoc />
		protected override void OnStartup(StartupEventArgs e)
		{
			new MainWindow
				{
					DataContext = Ioc.Default.GetRequiredService<MainViewModel>()
				}
				.Show();
		}

		private static IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);

			// Application
			services.AddTransient<MainViewModel>();
			services.AddTransient<FileListViewModel>();
			services.AddTransient<SettingsViewModel>();

			// Infrastructure
			services.AddSingleton<Config>();
			services.AddSingleton<ILocalizationProvider>(new LocalizationProvider(Language.ResourceManager));
			services.AddTransient<IDialogFactory, DialogFactory>();

			return services.BuildServiceProvider();
		}
	}
}