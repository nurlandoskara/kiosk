using Kiosk.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

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
        private readonly DispatcherTimer _timer;
        private bool _screenSaver;

        public MainWindow()
        {
            InitializeComponent();
#if !DEBUG
            this.Topmost = true;
#endif
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 1, 0);
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            ScreenSaver.Visibility = Visibility.Visible;
            ScreenSaver.Play();
            _screenSaver = true;
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
            _viewModel.Transition(true);
        }

        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_screenSaver)
            {
                _timer.Stop();
                _timer.Start();
            }
        }

        private void ScreenSaver_OnGotTouchCapture(object sender, TouchEventArgs e)
        {
            if (_screenSaver)
            {
                ScreenSaver.Visibility = Visibility.Collapsed;
                ScreenSaver.Stop();
                _screenSaver = false;
                _timer.Start();
            }
        }
    }
}
