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
        public bool IsMaximized;
        private int _column;
        private int _row;
        private int _columnSpan;
        private int _rowSpan;
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
            DataContext =  new MainViewModel(this);
        }
        
        private void MainWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_screenSaver)
            {
                _timer.Stop();
                _timer.Start();
            }
        }
        
        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_screenSaver)
            {
                ScreenSaver.Visibility = Visibility.Collapsed;
                ScreenSaver.Stop();
                _screenSaver = false;
                _timer.Start();
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsMaximized)
            {
                CenterContent.SetValue(Grid.RowProperty, _column);
                CenterContent.SetValue(Grid.ColumnProperty, _row);
                CenterContent.SetValue(Grid.ColumnSpanProperty, _columnSpan);
                CenterContent.SetValue(Grid.RowSpanProperty, _rowSpan);
            }
            else
            {
                _column = (int)CenterTransition.GetValue(Grid.ColumnProperty);
                _row = (int)CenterTransition.GetValue(Grid.RowProperty);
                _rowSpan = (int)CenterTransition.GetValue(Grid.RowSpanProperty);
                _columnSpan = (int)CenterTransition.GetValue(Grid.ColumnSpanProperty);
                CenterContent.SetValue(Grid.RowProperty, 0);
                CenterContent.SetValue(Grid.ColumnProperty, 0);
                CenterContent.SetValue(Grid.ColumnSpanProperty, Grid.ColumnDefinitions.Count);
                CenterContent.SetValue(Grid.RowSpanProperty, Grid.RowDefinitions.Count);
            }
            IsMaximized = !IsMaximized;
        }
    }
}
