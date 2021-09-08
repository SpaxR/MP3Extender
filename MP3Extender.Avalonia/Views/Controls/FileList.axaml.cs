using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MP3Extender.Avalonia.Views.Controls
{
	public class FileList : UserControl
	{
		public FileList()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}
	}
}