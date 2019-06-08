using System.Windows.Media;
using Kiosk.Classes;

namespace Kiosk.Models
{
    public class ImageItem
    {
        public string Url { get; set; }
        public ImageSource Source { get; set; }
        public string Description { get; set; }
        public ItemType ItemType { get; set; }
    }
}
