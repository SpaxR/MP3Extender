using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Controls.FileList;
using UI.Dialogs;

namespace UI
{
	/// <summary>Interaction logic for App.xaml</summary>
	[ExcludeFromCodeCoverage]
	public partial class App
	{
		public new static App Current => (App)Application.Current;

		public IServiceProvider Services { get; }

		public App()
		{
			Services = ConfigureServices(new ServiceCollection());
		}

		/// <inheritdoc />
		protected override void OnStartup(StartupEventArgs e)
		{
			new MainWindow { DataContext = Services.GetRequiredService<IMainViewModel>() }
				.Show();
		}

		private static IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);

			services.AddTransient<IMainViewModel, MainViewModel>();
			services.AddTransient<IFileListViewModel, FileListViewModel>();
			services.AddTransient<IDialogFactory, DialogFactory>();

			return services.BuildServiceProvider();
		}
	}
}