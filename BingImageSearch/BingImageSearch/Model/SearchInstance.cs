using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace BingImageSearch.Model
{
    public class SearchInstance //: IGroupInfo
    {
        public SearchInstance()
        {
            SearchedOn = DateTime.Now;
            _randomImage = new Lazy<ImageResult>(GetRandomImage);
        }

        public object Key { get { return Query; } }

        public DateTime SearchedOn { get; set; }
        public string Query { get; set; }
        public List<ImageResult> Images { get; set; }

        public Uri QueryLink
        {
            get { return new Uri("http://www.bing.com/images/search?q=" + Query); }
        }

        private Lazy<ImageResult> _randomImage;
        public ImageResult RandomImage
        {
            get { return _randomImage.Value; }
        }

        private ImageResult _selectedImage;
        public ImageResult SelectedImage
        {
            get { return _selectedImage ?? RandomImage; }
            set { _selectedImage = value; }
        }

        public IEnumerator<object> GetEnumerator()
        {
            return Images.Cast<object>().GetEnumerator();
        }

        public ImageResult GetRandomImage()
        {
            return GetRandomImages(1).FirstOrDefault();
        }

        public IEnumerable<ImageResult> GetRandomImages(uint count)
        {
            if (Images == null || !Images.Any()) return new ImageResult[] { };

            var rand = new Random();
            return Images.OrderBy(i => rand.NextDouble()).Take((int)count);
        }
    }
}