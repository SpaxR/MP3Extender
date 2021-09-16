using MP3Extender.Application;
using MP3Extender.MetroUI.Common;

namespace MP3Extender.MetroUI.Views.Editor
{
	public class EditorViewModel : ViewModelBase
	{
		public ISettings Settings    { get; }

		public EditorViewModel(ISettings settings)
		{
			Settings    = settings;
		}
	}
}