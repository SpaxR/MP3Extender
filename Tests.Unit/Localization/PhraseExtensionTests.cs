using System;
using System.Windows.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Moq;
using UI.Localization;
using Xunit;

namespace Tests.Unit.Localization
{
	public class PhraseExtensionTests : TestBase<PhraseExtension>
	{
		private string _key;

		private static readonly Mock<ILocalizationProvider> LocalizationMock = new();

		/// <inheritdoc />
		protected override PhraseExtension CreateSUT() => new(_key);

		static PhraseExtensionTests()
		{
			Ioc.Default.ConfigureServices(
				new ServiceCollection()
					.AddSingleton(LocalizationMock.Object)
					.BuildServiceProvider());
		}

		[Fact]
		public void GivenInstance_WhenConvertBack_ThenThrowsNotSupportedException()
		{
			Assert.Throws<NotSupportedException>(() => SUT.ConvertBack(null, null, null, null));
		}

		[Fact]
		public void GivenInstance_WhenConstructed_ThenDefaultValuesAreSet()
		{
			Assert.Equal(BindingMode.OneWay, SUT.Mode);
			Assert.Equal("[(0)]",            SUT.Path.Path);
			Assert.Equal(SUT,                SUT.Converter);
			Assert.Equal(SUT.Source,         LocalizationMock.Object);
		}

		[Fact]
		public void GivenKeyAndParameter_WhenConstructorInvoked_ThenConverterParameterIsSetAsArray()
		{
			var sut = new PhraseExtension(_key, "SOME VALUE");

			Assert.IsAssignableFrom<Array>(sut.ConverterParameter);
		}

		[Fact]
		public void GivenKeyAndArrayParameter_WhenConstructorInvoked_ThenConverterParameterIsSet()
		{
			string[] expectation = { "SOME VALUE" };
			var      sut         = new PhraseExtension(_key, expectation);

			Assert.Equal(expectation, sut.ConverterParameter);
		}
		
		[Fact]
		public void GivenValidData_WhenParameterIsArray_ThenReturnsFormattedString()
		{
			const string expectation = "FORMATTED VALUE";

			string result = (string)SUT.Convert("{0} {1}", null, new[] { "FORMATTED", "VALUE" }, null);

			Assert.Equal(expectation, result);
		}

		[Theory]
		[InlineData("TOO FEW {0} {1} {2}", new[] { "VALUE 1" })]
		[InlineData("TOO MANY",            new[] { "VALUE 1", "VALUE 2" })]
		public void GivenInvalidParameter_WhenConverted_ThenReturnsFormattedString(
			string format, params object[] values)
		{
			string result = (string)SUT.Convert(format, null, values, null);

			Assert.Equal(format, result);
		}
	}
}