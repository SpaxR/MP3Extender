using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace MP3Extender.MetroUI.Views.Editor
{
	public class DynamicDataGrid : DataGrid
	{
		public static readonly DependencyProperty DynamicColumnsProperty = DependencyProperty.Register(
			nameof(DynamicColumns), typeof(ObservableCollection<DataGridColumn>), typeof(DynamicDataGrid),
			new PropertyMetadata(OnColumnsChanged));


		public ObservableCollection<DataGridColumn> DynamicColumns
		{
			get => (ObservableCollection<DataGridColumn>)GetValue(DynamicColumnsProperty);
			set => SetValue(DynamicColumnsProperty, value);
		}

		public DynamicDataGrid()
		{
			AutoGenerateColumns = false;
		}


		private static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is DynamicDataGrid grid)
			{
				if (e.OldValue is ObservableCollection<DataGridColumn> oldCollection)
				{
					oldCollection.CollectionChanged -= grid.UpdateColumns;
					grid.Columns.Clear();
				}

				if (e.NewValue is ObservableCollection<DataGridColumn> newCollection)
				{
					newCollection.CollectionChanged += grid.UpdateColumns;
					grid.UpdateColumns();
				}
			}
		}

		private void UpdateColumns(object sender, NotifyCollectionChangedEventArgs e) => UpdateColumns();

		private void UpdateColumns()
		{
			for (int i = 0; i < DynamicColumns.Count || i < Columns.Count; i++)
			{
				if (DynamicColumns.Count <= i)
				{
					Columns.RemoveAt(i);
				}
				else if (Columns.Count <= i)
				{
					Columns.Add(DynamicColumns[i]);
				}
				else
				{
					Columns[i] = DynamicColumns[i];
				}
			}
		}
	}
}