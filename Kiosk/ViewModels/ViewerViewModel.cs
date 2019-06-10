using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using Kiosk.Models;

namespace Kiosk.ViewModels
{
    public class ViewerViewModel: BaseViewModel
    {
        private ObservableCollection<ImageItem> _items;
        private ImageItem _selectedItem;
        private int _selectedIndex;

        public ObservableCollection<ImageItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        public ImageItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value > Items.Count - 1) value = 0;
                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
            }
        }


        public ViewerViewModel(ObservableCollection<ImageItem> items, ImageItem selectedItem)
        {
            Items = items;
            SelectedItem = Items.FirstOrDefault(x => x.Url == selectedItem.Url);
            var timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 30);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SelectedIndex += 1;
        }
    }
}
