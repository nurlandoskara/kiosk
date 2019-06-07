using System.Windows.Controls;
using System.Windows.Input;

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
            WebBrowser.Address = url;
        }

        private void WebBrowser_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
