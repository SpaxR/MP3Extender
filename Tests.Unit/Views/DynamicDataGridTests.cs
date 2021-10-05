using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MP3Extender.MetroUI.Views.Editor;
using Xunit;

namespace Tests.Unit.Views
{
	public class DynamicDataGridTests : TestBase<DynamicDataGrid>
	{
		private readonly ObservableCollection<DataGridColumn> _columns = new();

		public DynamicDataGridTests()
		{
			_ = SUT;
		}
		
		/// <inheritdoc />
		protected override DynamicDataGrid CreateSUT() => new()
		{
			DynamicColumns = _columns
		};

		[UIFact]
		public void GivenColumns_WhenColumnAdded_ThenColumnsContainsNewColumn()
		{
			var column = new DataGridTextColumn();

			_columns.Add(column);

			Assert.Contains(column, SUT.Columns);
		}

		[UIFact]
		public void GivenColumns_WhenColumnDeleted_ThenColumnsDoesNotContainColumn()
		{
			var column = new DataGridTextColumn();

			_columns.Add(column);
			_columns.Remove(column);

			Assert.DoesNotContain(column, SUT.Columns);
		}

		[UIFact]
		public void GivenColumns_WhenColumnChanged_ThenColumnsGetsUpdated()
		{
			var column    = new DataGridTextColumn();
			var newColumn = new DataGridTextColumn();

			_columns.Add(column);
			_columns[0] = newColumn;

			Assert.Contains(newColumn, SUT.Columns);
		}

		[UIFact]
		public void GivenColumns_WhenCollectionRemoved_ThenColumnsGetsCleared()
		{
			SUT.DynamicColumns = null;

			Assert.Empty(SUT.Columns);
		}
	}
}