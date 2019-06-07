using System;
using System.Windows.Controls;
using Kiosk.Classes;
using System.Windows.Input;
using Kiosk.Views;

namespace Kiosk.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private UserControl _leftTransitionContent;
        private UserControl _rightTransitionContent;
        private bool _isCenterContentVisible;
        private UserControl _centerTransitionContent;

        public UserControl LeftTransitionContent
        {
            get => _leftTransitionContent;
            set
            {
                _leftTransitionContent = value;
                NotifyOfPropertyChange(() => LeftTransitionContent);
            }
        }

        public UserControl RightTransitionContent
        {
            get => _rightTransitionContent;
            set
            {
                _rightTransitionContent = value;
                NotifyOfPropertyChange(() => RightTransitionContent);
            }
        }

        public UserControl CenterTransitionContent
        {
            get => _centerTransitionContent;
            set
            {
                _centerTransitionContent = value;
                NotifyOfPropertyChange(() => CenterTransitionContent);
            }
        }

        public ICommand MuseumCommand { get; set; }
        public ICommand VirtualCommand { get; set; }
        public ICommand Gallery3DCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        public ICommand GalleryPhotoCommand { get; set; }
        public ICommand BukletCommand { get; set; }
        public ICommand Model3DCommand { get; set; }
        public ICommand CommentCommand { get; set; }

        public bool IsCenterContentVisible
        {
            get => _isCenterContentVisible;
            set
            {
                _isCenterContentVisible = value;
                NotifyOfPropertyChange(() => IsCenterContentVisible);
            }
        }
        
        public MainViewModel()
        {
            MuseumCommand = new Command(OpenMuseum, CanExecuteCommand);
            VirtualCommand = new Command(OpenVirtual, CanExecuteCommand);
            Gallery3DCommand = new Command(OpenGallery3D, CanExecuteCommand);
            HistoryCommand = new Command(OpenHistory, CanExecuteCommand);
            GalleryPhotoCommand = new Command(OpenGalleryPhoto, CanExecuteCommand);
            BukletCommand = new Command(OpenBuklet, CanExecuteCommand);
            Model3DCommand = new Command(OpenModel3D, CanExecuteCommand);
            CommentCommand = new Command(OpenComment, CanExecuteCommand);
            Transition(true);
        }
        private void Transition(bool isAppear)
        {
            if (isAppear)
            {
                LeftTransitionContent = new LeftMenuContent();
                RightTransitionContent = new RightMenuContent();
                IsCenterContentVisible = false;
            }
            else
            {
                LeftTransitionContent = null;
                RightTransitionContent = null;
                IsCenterContentVisible = true;
            }
        }
        
        private void OpenComment(object parameter)
        {
            Transition(false);
        }

        private void OpenModel3D(object parameter)
        {
            Transition(false);
        }

        private void OpenBuklet(object parameter)
        {
            Transition(false);
        }

        private void OpenGalleryPhoto(object parameter)
        {
            Transition(false);
        }

        private void OpenHistory(object parameter)
        {
            Transition(false);
        }

        private void OpenGallery3D(object parameter)
        {
            Transition(false);
        }

        private void OpenVirtual(object parameter)
        {
            Transition(false);
            var web = new WebContent("http://localhost/VirtualTour/");
            CenterTransitionContent = web;
        }

        private void OpenMuseum(object parameter)
        {
            Transition(false);
        }
    }
}
