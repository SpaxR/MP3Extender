using MP3Extender.MetroUI.Views;
using MP3Extender.MetroUI.Views.About;
using MP3Extender.MetroUI.Views.Editor;
using MP3Extender.MetroUI.Views.Settings;
using NSubstitute;
using Xunit;

namespace Tests.Unit.ViewModels
{
	public class MainViewModelTests : TestBase<MainViewModel>
	{
		private readonly EditorViewModel   _editorVMMock;
		private readonly AboutViewModel    _aboutVMMock;
		private readonly SettingsViewModel _settingsVMMock;

		public MainViewModelTests()
		{
			var fileEditorVMMock = Substitute.For<FileEditorViewModel>(SettingsMock, FileSystemMock);
			_aboutVMMock    = Substitute.For<AboutViewModel>();
			_settingsVMMock = Substitute.For<SettingsViewModel>(SettingsMock);
			_editorVMMock   = Substitute.For<EditorViewModel>(SettingsMock, fileEditorVMMock);
		}

		/// <inheritdoc />
		protected override MainViewModel CreateSUT() => new(_editorVMMock, _settingsVMMock, _aboutVMMock);

		[Fact]
		public void Sanity()
		{
			Assert.NotNull(SUT);
			Assert.Equal(_editorVMMock,   SUT.EditorVM);
			Assert.Equal(_aboutVMMock,    SUT.AboutVM);
			Assert.Equal(_settingsVMMock, SUT.SettingsVM);
		}
	}
}