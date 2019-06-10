using Caliburn.Micro;
using Kiosk.Classes;
using Kiosk.Models;
using Kiosk.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Kiosk.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private UserControl _leftTransitionContent;
        private UserControl _rightTransitionContent;
        private bool _isCenterContentVisible;
        private UserControl _centerTransitionContent;
        private int _selectedCenterContentIndex;
        private ImageItem _selectedImage;
        private MainWindow _mainWindow;
        private bool _isGallery;
        private int _column;
        private int _row;
        private int _columnSpan;

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
        public ICommand CloseCommand { get; set; }

        public bool IsCenterContentVisible
        {
            get => _isCenterContentVisible;
            set
            {
                _isCenterContentVisible = value;
                NotifyOfPropertyChange(() => IsCenterContentVisible);
            }
        }
        
        public ObservableCollection<ImageItem> ImagesList { get; set; }

        public int SelectedCenterContentIndex
        {
            get => _selectedCenterContentIndex;
            set
            {
                _selectedCenterContentIndex = value;
                NotifyOfPropertyChange(() => SelectedCenterContentIndex);
                OpenViewer();
            }
        }

        public ImageItem SelectedImage
        {
            get => _selectedImage;
            set
            {
                if(value == _selectedImage) return;
                _selectedImage = value;
                NotifyOfPropertyChange(() => SelectedImage);
                OpenViewer();
            }
        }

        public int Column
        {
            get => _column;
            set
            {
                _column = value;
                NotifyOfPropertyChange(() => Column);
            }
        }

        public int Row
        {
            get => _row;
            set
            {
                _row = value;
                NotifyOfPropertyChange(() => Row);
            }
        }

        public int ColumnSpan
        {
            get => _columnSpan;
            set
            {
                _columnSpan = value;
                NotifyOfPropertyChange(() => ColumnSpan);
            }
        }

        public MainViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            Row = 0;
            Column = 1;
            ColumnSpan = 1;
            MuseumCommand = new Command(OpenMuseum, CanExecuteCommand);
            VirtualCommand = new Command(OpenVirtual, CanExecuteCommand);
            Gallery3DCommand = new Command(OpenGallery3D, CanExecuteCommand);
            HistoryCommand = new Command(OpenHistory, CanExecuteCommand);
            GalleryPhotoCommand = new Command(OpenGalleryPhoto, CanExecuteCommand);
            BukletCommand = new Command(OpenBuklet, CanExecuteCommand);
            Model3DCommand = new Command(OpenModel3D, CanExecuteCommand);
            CommentCommand = new Command(OpenComment, CanExecuteCommand);
            CloseCommand = new Command(Close, CanExecuteCommand);
            Transition(true);
        }

        private void Close(object parameter)
        {
            if (_isGallery)
            { 
                _isGallery = false;
                OpenGallery();
            }
            else
            {
                Transition(true);
            }
        }
        private void OpenViewer()
        {
            if (SelectedImage != null)
            {
                if (SelectedImage.ItemType == ItemType.Html)
                {
                    var viewer = new WebViewer(SelectedImage);
                    CenterTransitionContent.Content = viewer;
                }

                _isGallery = true;
            }
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
            Transition(false);
            var view = new PostView();
            CenterTransitionContent = view;
        }

        private void OpenGallery()
        {
            Transition(false);
            const string path = "C:\\xampp\\htdocs\\Exponates";
            var list = LoadData.GetFolders(path);
            ImagesList = new BindableCollection<ImageItem>();
            foreach (var folder in list)
            {
                try
                {
                    var image = new BitmapImage(new Uri($"{path}\\{folder}\\preview.png"));
                    var description = string.Empty;
                    var descriptionPath = $"{path}\\{folder}\\description.txt";
                    if (File.Exists(descriptionPath))
                    {
                        using (var sr = new StreamReader(descriptionPath))
                        {
                            description = sr.ReadToEnd();
                        }
                    }

                    ImagesList.Add(new ImageItem
                        { Source = image, Url = $"http:\\\\localhost\\Exponates\\{folder}\\start.html", ItemType = ItemType.Html, Description = description });
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            CenterTransitionContent = new Gallery();
        }
        private void OpenModel3D(object parameter)
        {
            OpenGallery();
        }

        private void OpenBuklet(object parameter)
        {
        }

        private void OpenGalleryPhoto(object parameter)
        {
            Transition(false);
            const string path = "C:\\xampp\\htdocs\\PhotoGallery";
            LoadImages(path);
            CenterTransitionContent = new ImageViewer(ImagesList);
        }

        private void OpenHistory(object parameter)
        {
        }

        private void OpenGallery3D(object parameter)
        {
            Transition(false);
            const string path = "C:\\xampp\\htdocs\\3DGallery";
            LoadImages(path);
            CenterTransitionContent = new ImageViewer(ImagesList);
        }

        private void LoadImages(string path)
        {
            var list = LoadData.GetFiles(path);
            ImagesList = new BindableCollection<ImageItem>();
            foreach (var image in list)
            {
                try
                {
                    var description = string.Empty;
                    var descriptionPath =
                        $"{path}\\{image.Name.Remove(image.Name.ToLower().IndexOf(".jpg", StringComparison.Ordinal))}.txt";
                    if (File.Exists(descriptionPath))
                    {
                        using (var sr = new StreamReader(descriptionPath))
                        {
                            description = sr.ReadToEnd();
                        }
                    }
                    ImagesList.Add(new ImageItem
                        { Source = new BitmapImage(new Uri(image.FullPath)), ItemType = ItemType.Image, Url = image.FullPath, Description = description });
                }
                catch (Exception e)
                {
                    continue;
                }
            }
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
