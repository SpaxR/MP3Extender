using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace MP3Extender.WPF.Localization
{
	public class PhraseExtension : Binding, IValueConverter
	{
		public PhraseExtension(string key)
		{
			Mode      = BindingMode.OneWay;
			Source    = Ioc.Default.GetService<ILocalizationProvider>();
			Path      = new PropertyPath("[(0)]", key);
			Converter = this;
		}

		public PhraseExtension(string key, object parameter) : this(key)
		{
			ConverterParameter = parameter is Array ? parameter : new[] { parameter };
		}


		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				return string.Format(
					value as string       ?? string.Empty,
					parameter as object[] ?? Array.Empty<object>());
			}
			catch (Exception)
			{
				return value;
			}
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotSupportedException();
	}
}