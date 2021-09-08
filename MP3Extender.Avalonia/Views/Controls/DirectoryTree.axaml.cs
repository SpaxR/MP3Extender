using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MP3Extender.Avalonia.Views.Controls
{
	public class DirectoryTree : UserControl
	{
		public DirectoryTree()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}