using Kiosk.Models;
using Kiosk.ViewModels;
using System.Collections.ObjectModel;

namespace Kiosk.Views
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class WebViewer
    {
        public WebViewer(ObservableCollection<ImageItem> items, ImageItem selectedItem)
        {
            InitializeComponent();
            DataContext = new ViewerViewModel(items, selectedItem);
        }
    }
}
