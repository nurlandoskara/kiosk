using Kiosk.Classes;
using System.Windows.Input;

namespace Kiosk.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private LeftMenuContent _leftTransitionContent;
        private RightMenuContent _rightTransitionContent;

        public LeftMenuContent LeftTransitionContent
        {
            get => _leftTransitionContent;
            set
            {
                _leftTransitionContent = value;
                NotifyOfPropertyChange(() => LeftTransitionContent);
            }
        }

        public RightMenuContent RightTransitionContent
        {
            get => _rightTransitionContent;
            set
            {
                _rightTransitionContent = value;
                NotifyOfPropertyChange(() => RightTransitionContent);
            }
        }

        public ICommand MuseumCommand { get; set; }
        public MainViewModel()
        {
            MuseumCommand = new Command(OpenMuseum, CanExecuteCommand);
            LeftTransitionContent = new LeftMenuContent();
            RightTransitionContent = new RightMenuContent();
        }

        private void OpenMuseum(object parameter)
        {
            LeftTransitionContent = null;
            Transition(true);
        }
        
        private void Transition(bool isAppear)
        {
            if (isAppear)
            {
                LeftTransitionContent = new LeftMenuContent();
                RightTransitionContent = new RightMenuContent();
            }
            else
            {
                LeftTransitionContent = null;
                RightTransitionContent = null;
            }
        }
    }
}
