using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Controls.FileList;
using UI.Dialogs;
using UI.Localization;

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
			new MainWindow { DataContext = Ioc.Default.GetRequiredService<IMainViewModel>() }
				.Show();
		}

		private static IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);
			services.AddSingleton<ILocalizationProvider>(new LocalizationProvider(Language.ResourceManager));

			services.AddTransient<IMainViewModel, MainViewModel>();
			services.AddTransient<IFileListViewModel, FileListViewModel>();
			services.AddTransient<IDialogFactory, DialogFactory>();

			return services.BuildServiceProvider();
		}
	}
}