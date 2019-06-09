using System.Collections.ObjectModel;
using System.Linq;
using Kiosk.Models;

namespace Kiosk.ViewModels
{
    public class PostViewModel: BaseViewModel
    {
        public ObservableCollection<Post> Posts { get; set; }
        public PostViewModel()
        {
            using (var context = new ModelContainer())
            {
                Posts = new ObservableCollection<Post>(context.Posts.ToList());
            }
        }
    }
}
