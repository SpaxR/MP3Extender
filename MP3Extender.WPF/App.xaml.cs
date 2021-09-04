using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.WPF.Localization;
using MP3Extender.WPF.ViewModels;

namespace MP3Extender.WPF
{
	[ExcludeFromCodeCoverage(Justification = "UI-Class")]
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
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);

			// Application
			services.AddTransient<MainViewModel>();
			services.AddTransient<FileListViewModel>();
			services.AddTransient<SettingsViewModel>();

			// Infrastructure
			services.AddSingleton<Application.Settings>();
			services.AddSingleton<ILocalizationProvider>(new LocalizationProvider(Language.ResourceManager));
			services.AddTransient<IDialogFactory, DialogFactory>();

			return services.BuildServiceProvider();
		}
	}
}