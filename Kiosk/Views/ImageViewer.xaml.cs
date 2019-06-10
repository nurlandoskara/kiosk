using Kiosk.Models;
using Kiosk.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kiosk.Views
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class ImageViewer
    {
        private bool _isMaximized;
        private int _column;
        private int _row;
        private int _rowSpan;
        private int _columnSpan;

        public ImageViewer(ObservableCollection<ImageItem> items)
        {
            InitializeComponent();
            DataContext = new ViewerViewModel(items);
        }

        private void FlipView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_isMaximized)
            {
                FlipView.SetValue(Grid.RowProperty, _column);
                FlipView.SetValue(Grid.ColumnProperty, _row);
                FlipView.SetValue(Grid.ColumnSpanProperty, _columnSpan);
                FlipView.SetValue(Grid.RowSpanProperty, _rowSpan);
            }
            else
            {
                _column = (int)FlipView.GetValue(Grid.ColumnProperty);
                _row = (int)FlipView.GetValue(Grid.RowProperty);
                _rowSpan = (int)FlipView.GetValue(Grid.RowSpanProperty);
                _columnSpan = (int)FlipView.GetValue(Grid.ColumnSpanProperty);
                FlipView.SetValue(Grid.RowProperty, 0);
                FlipView.SetValue(Grid.ColumnProperty, 0);
                FlipView.SetValue(Grid.ColumnSpanProperty, Grid.ColumnDefinitions.Count);
                FlipView.SetValue(Grid.RowSpanProperty, Grid.RowDefinitions.Count);
            }
            _isMaximized = !_isMaximized;
        }
    }
}
