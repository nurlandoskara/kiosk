using System;
using System.Windows.Controls;

namespace Kiosk.Views
{
    /// <summary>
    /// Interaction logic for WebContent.xaml
    /// </summary>
    public partial class WebContent : UserControl
    {
        public WebContent(string url)
        {
            InitializeComponent();
            WebBrowser.Navigate(new Uri(url));
        }
    }
}
