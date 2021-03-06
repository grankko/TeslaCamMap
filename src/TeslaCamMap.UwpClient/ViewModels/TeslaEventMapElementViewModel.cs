﻿using TeslaCamMap.UwpClient.Model;
using TeslaCamMap.UwpClient.Services;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;

namespace TeslaCamMap.UwpClient.ViewModels
{
    public class TeslaEventMapElementViewModel : ViewModelBase
    {
        private FileSystemService _fileSystemService;
        public TeslaEvent Model { get; set; }
        public Geopoint Location { get; set; }

        private BitmapImage _thumbnailImage;
        public BitmapImage ThumbnailImage
        {
            get { return _thumbnailImage; }
            set
            {
                _thumbnailImage = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        private async void TeslaEventMapElementViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Load thumbnail image for the selected event.
            if (e.PropertyName == nameof(IsSelected) && IsSelected && Model.ThumbnailFile != null)
                ThumbnailImage = await _fileSystemService.LoadImageFromStorageFile(Model.ThumbnailFile);
        }

        public TeslaEventMapElementViewModel(TeslaEvent model)
        {
            Model = model;
            _fileSystemService = new FileSystemService();
            Location = new Geopoint(new BasicGeoposition() { Latitude = model.EstimatedLatitude, Longitude = model.EstimatedLongitude });
            this.PropertyChanged += TeslaEventMapElementViewModel_PropertyChanged;
        }
    }
}
