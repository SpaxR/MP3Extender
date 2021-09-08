using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MP3Extender.Avalonia.Views
{
	public class ConfigurationWindow : Window
	{
		public ConfigurationWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}