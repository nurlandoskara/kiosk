using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kiosk.ViewModels;

namespace Kiosk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _isMaximized;
        private int _column;
        private int _row;
        private int _columnSpan;
        private int _rowSpan;
        private MainViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnContentRendered(object sender, EventArgs e)
        {
            DataContext = _viewModel = new MainViewModel();
        }

        private void CenterTransition_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_isMaximized)
            {
                CenterTransition.SetValue(Grid.RowProperty, _column);
                CenterTransition.SetValue(Grid.ColumnProperty, _row);
                CenterTransition.SetValue(Grid.ColumnSpanProperty, _columnSpan);
                CenterTransition.SetValue(Grid.RowSpanProperty, _rowSpan);
            }
            else
            {
                _column = (int) CenterTransition.GetValue(Grid.ColumnProperty);
                _row = (int)CenterTransition.GetValue(Grid.RowProperty);
                _rowSpan = (int)CenterTransition.GetValue(Grid.RowSpanProperty);
                _columnSpan = (int)CenterTransition.GetValue(Grid.ColumnSpanProperty);
                CenterTransition.SetValue(Grid.RowProperty, 0);
                CenterTransition.SetValue(Grid.ColumnProperty, 0);
                CenterTransition.SetValue(Grid.ColumnSpanProperty, Grid.ColumnDefinitions.Count);
                CenterTransition.SetValue(Grid.RowSpanProperty, Grid.RowDefinitions.Count);
            }
            _isMaximized = !_isMaximized;
        }

        private void CenterTransition_OnMouseLeave(object sender, MouseEventArgs e)
        {
            _isMaximized = !_isMaximized;
            _viewModel.Transition(true);
        }
    }
}
