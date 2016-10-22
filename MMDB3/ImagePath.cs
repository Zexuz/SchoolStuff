using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using MMDB.MovieDatabase.ValueObjects;

namespace MMDB3 {

    public class ImagePath : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) return null;

            if (value.GetType() == typeof(MovieSearchResultItem)) {
                return @"D:\programming\csharp\SchoolStuff\MMDB3\Resources\movie.png";
            }

            if (value.GetType() == typeof(CastOrCrewSearchResultItem)) {
                switch (((CastOrCrewSearchResultItem) value).Type) {
                    case SearchResultItemType.Actor:
                        return @"D:\programming\csharp\SchoolStuff\MMDB3\Resources\actor.png";
                    case SearchResultItemType.ActorDirector:
                        return @"D:\programming\csharp\SchoolStuff\MMDB3\Resources\actor_director.png";
                    case SearchResultItemType.Director:
                        return @"D:\programming\csharp\SchoolStuff\MMDB3\Resources\director.png";
                }

            }

            return @"D:\programming\csharp\SchoolStuff\MMDB3\Resources\unknown.png";
        }


        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture) {
            throw new NotImplementedException();
        }

    }

}