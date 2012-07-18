using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BingImageSearch.Common;
using BingImageSearch.Common;
using BingImageSearch.Model;
using BingImageSearch.Services;

namespace BingImageSearch.ViewModels
{
    public class PreferencesViewModel : BindableBase
    {
        private readonly ApplicationSettings _settings;

        public PreferencesViewModel(ApplicationSettings settings)
        {
            _settings = settings;
            ImageResultSize = _settings.ImageResultSize;
            Rating = _settings.Rating;
            ResetHistoryCommand = new AsyncDelegateCommand(ResetHistory);
        }

        public ICommand ResetHistoryCommand { get; set; }

        private ResultSize _imageResultSize;
        public ResultSize ImageResultSize 
        {
            get { return _imageResultSize; } 
            set 
            {
                if (value != ResultSize.Empty)
                {
                    base.SetProperty(ref _imageResultSize, value);
                    _settings.ImageResultSize = value;
                }
            } 
        }

        private Rating _rating;
        public Rating Rating
        {
            get { return _rating; }
            set
            {
                if (value != Rating.Empty)
                {
                    base.SetProperty(ref _rating, value);
                    _settings.Rating = value;
                }
            }
        }

        public async Task ResetHistory()
        {
            _settings.Searches.Clear();
            await _settings.SaveAsync();
        }
    }
}
