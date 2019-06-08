using Kiosk.Models;
using Kiosk.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Kiosk.Views
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class ImageViewer
    {
        public ImageViewer(ObservableCollection<ImageItem> items, ImageItem selectedItem)
        {
            InitializeComponent();
#if !DEBUG
            this.Topmost = true;
#endif
            DataContext = new ViewerViewModel(items, selectedItem);
        }

        private void FlipView_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Close();
        }
    }
}
