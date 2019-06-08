using System.Collections.ObjectModel;
using System.Linq;
using Kiosk.Models;

namespace Kiosk.ViewModels
{
    public class ViewerViewModel: BaseViewModel
    {
        private ObservableCollection<ImageItem> _items;

        public ObservableCollection<ImageItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        public ImageItem SelectedItem { get; set; }
        public ViewerViewModel(ObservableCollection<ImageItem> items, ImageItem selectedItem)
        {
            Items = items;
            SelectedItem = Items.FirstOrDefault(x => x.Url == selectedItem.Url);
        }
    }
}
