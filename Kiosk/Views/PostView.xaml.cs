using System.Windows.Controls;
using Kiosk.ViewModels;

namespace Kiosk.Views
{
    /// <summary>
    /// Interaction logic for PostView.xaml
    /// </summary>
    public partial class PostView : UserControl
    {
        public PostView()
        {
            InitializeComponent();
            DataContext = new PostViewModel();
        }
    }
}
