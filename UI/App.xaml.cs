using System;
using System.Reflection;
using System.Windows;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using UI.Controls.FileList;

namespace UI
{
	/// <summary>Interaction logic for App.xaml</summary>
	public partial class App
	{
		public new static App Current => (App)Application.Current;

		public IServiceProvider Services { get; }

		public App()
		{
			Services = ConfigureServices(new ServiceCollection());
		}

		private static IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);

			services.AddTransient<MainViewModel>();
			services.AddTransient<FileListViewModel>();

			return services.BuildServiceProvider();
		}
	}
}