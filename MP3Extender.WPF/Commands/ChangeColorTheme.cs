using System;
using System.Windows;
using System.Windows.Input;
using AdonisUI;

namespace UI.Commands
{
	public class ChangeColorTheme : ICommand
	{
		/// <inheritdoc />
		public bool CanExecute(object parameter) => true;

		/// <inheritdoc />
		public void Execute(object parameter)
		{
			Uri theme = parameter switch
			{
				"Light"   => ResourceLocator.LightColorScheme,
				"Dark"    => ResourceLocator.DarkColorScheme,
				"Classic" => ResourceLocator.ClassicTheme,
				_         => null
			};

			if (theme != null)
			{
				ResourceLocator.SetColorScheme(Application.Current.Resources, theme);
			}
		}

		/// <inheritdoc />
		public event EventHandler CanExecuteChanged;
	}
}