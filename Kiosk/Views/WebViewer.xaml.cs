using Kiosk.Models;

namespace Kiosk.Views
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class WebViewer
    {
        public WebViewer(ImageItem selectedItem)
        {
            InitializeComponent();
            WebBrowser.Address = selectedItem.Url;
            Description.Text = selectedItem.Description;
        }

    }
}
