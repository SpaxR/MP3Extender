using Microsoft.Toolkit.Mvvm.Messaging;
using MP3Extender.Application;
using MP3Extender.WPF;
using MP3Extender.WPF.Factories;
using MP3Extender.WPF.Services;
using NSubstitute;
using Xunit;

namespace Tests.Unit.Services
{
	public class FileSystemServiceTests : TestBase<FileSystemService>
	{
		private readonly ISettings      _settingsMock      = Substitute.For<ISettings>();
		private readonly IDialogFactory _dialogFactoryMock = Substitute.For<IDialogFactory>();

		/// <inheritdoc />
		protected override FileSystemService CreateSUT()
			=> new(MessengerMock, _dialogFactoryMock, _settingsMock);

		private IFolderBrowserDialog SetupFolderBrowser(string path)
		{
			var dialog = Substitute.For<IFolderBrowserDialog>();

			if (path != null)
			{
				dialog.ShowDialog().Returns(true);
				dialog.SelectedPath.Returns(path);
			}
			else
			{
				dialog.ShowDialog().Returns(false);
			}

			_dialogFactoryMock.CreateFolderBrowserDialog().Returns(dialog);

			return dialog;
		}


		[Fact]
		public void ChangingRootDirectory_ShowsFolderBrowserDialog()
		{
			var dialog = SetupFolderBrowser(null);

			SUT.ChangeRootDirectory();

			dialog.Received(1).ShowDialog();
		}

		[Fact]
		public void ChangingRootDirectory_WhenSuccessful_SendsDirectoryChangedEvent()
		{
			SetupFolderBrowser("SOME PATH");

			SUT.ChangeRootDirectory();

			MessengerMock
				.Received(1)
				.Send(Arg.Is<DirectoryChangedEvent>(ev => "SOME PATH".Equals(ev.Path)));
		}

		[Fact]
		public void GivenConfig_WhenLoaded_RootDirectoryPathEqualsConfig()
		{
			_settingsMock.RootFolder.Returns("SOME FOLDER");

			string path = SUT.RootDirectoryPath;

			Assert.Equal("SOME FOLDER", path);
		}
	}
}