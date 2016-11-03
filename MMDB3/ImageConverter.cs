using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

using MMDB.MovieDatabase.ValueObjects;

namespace MMDB3 {

    public class ImageConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) return null;

            var dic = new Dictionary<SearchResultItemType, string> {
                {SearchResultItemType.Movie, "movie.png"},
                {SearchResultItemType.Actor, "actor.png"},
                {SearchResultItemType.ActorDirector, "actor_director.png"},
                {SearchResultItemType.Director, "director.png"},
                {SearchResultItemType.None, "unknown.png"}
            };

            BitmapImage bitmap;

            var resourceName = $"MMDB3.Resources.{dic[((SearchResultItem)value).Type]}";
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName)) {
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
            }

            return bitmap;
        }


        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) {
            throw new NotImplementedException();
        }

    }

}