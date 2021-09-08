using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MP3Extender.Avalonia.ViewModels;
using MP3Extender.Avalonia.Views;

namespace MP3Extender.Avalonia
{
	public class App : global::Avalonia.Application
	{
		public override void Initialize()
		{
			AvaloniaXamlLoader.Load(this);
		}

		public override void OnFrameworkInitializationCompleted()
		{
			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				desktop.MainWindow = new MainWindow
				{
					DataContext = new MainWindowViewModel(),
				};
			}

			base.OnFrameworkInitializationCompleted();
		}
	}
}