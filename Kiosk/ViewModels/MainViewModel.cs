using System;
using System.Windows.Controls;
using Kiosk.Classes;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Kiosk.Models;
using Kiosk.Views;

namespace Kiosk.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private UserControl _leftTransitionContent;
        private UserControl _rightTransitionContent;
        private bool _isCenterContentVisible;
        private UserControl _centerTransitionContent;
        private int _selectedCenterContentIndex;

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
        
        public IObservableCollection<ImageItem> ImagesList { get; set; }

        public int SelectedCenterContentIndex
        {
            get => _selectedCenterContentIndex;
            set
            {
                _selectedCenterContentIndex = value;
                NotifyOfPropertyChange(() => SelectedCenterContentIndex);
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
        public void Transition(bool isAppear)
        {
            if (isAppear)
            {
                LeftTransitionContent = new LeftMenuContent();
                RightTransitionContent = new RightMenuContent();
                CenterTransitionContent = null;
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
        }

        private void OpenModel3D(object parameter)
        {
            Transition(false);
            var list = LoadData.GetFolders("C:\\xampp\\htdocs\\Exponates");
            ImagesList = new BindableCollection<ImageItem>();
            foreach (var folder in list)
            {
                try
                {
                    var image = new BitmapImage(new Uri($"{folder}\\preview.jpg"));
                    ImagesList.Add(new ImageItem { Source = image });
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            CenterTransitionContent = new Gallery();
        }

        private void OpenBuklet(object parameter)
        {
        }

        private void OpenGalleryPhoto(object parameter)
        {
            Transition(false);
            var list = LoadData.GetFiles("C:\\xampp\\htdocs\\PhotoGallery");
            ImagesList = new BindableCollection<ImageItem>();
            foreach (var image in list)
            {
                try
                {
                    ImagesList.Add(new ImageItem { Source = new BitmapImage(new Uri(image)) });
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            CenterTransitionContent = new Gallery();
        }

        private void OpenHistory(object parameter)
        {
        }

        private void OpenGallery3D(object parameter)
        {
            Transition(false);
            var list = LoadData.GetFiles("C:\\xampp\\htdocs\\3DGallery");
            ImagesList = new BindableCollection<ImageItem>();
            foreach (var image in list)
            {
                try
                {
                    ImagesList.Add(new ImageItem { Source = new BitmapImage(new Uri(image)) });
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            CenterTransitionContent = new Gallery();
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
            var web = new WebContent("http://localhost/");
            CenterTransitionContent = web;
        }
    }
}
