using System.ComponentModel;
using System.Globalization;
using System.Resources;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace MP3Extender.MetroUI.Localization
{
	public interface ILocalizationProvider : INotifyPropertyChanged
	{
		public string this[string key] { get; }
		public CultureInfo CurrentCulture { get; set; }
	}


	public class LocalizationProvider : ObservableObject, ILocalizationProvider
	{
		private readonly ResourceManager _manager;

		public string this[string key] => _manager.GetString(key, CurrentCulture);

		public CultureInfo CurrentCulture
		{
			get => CultureInfo.CurrentUICulture;
			set
			{
				OnPropertyChanging();
				CultureInfo.CurrentUICulture = value;
				OnPropertyChanged();
			}
		}

		public LocalizationProvider(ResourceManager manager) => _manager = manager;
	}
}