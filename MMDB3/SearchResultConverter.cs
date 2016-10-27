using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Repositories;
using MMDB.MovieDatabase.ValueObjects;

using MMDB3.MovieDatabase.Domain.Entities;


namespace MMDB3 {

    public class SearchResultConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) return null;

            Console.WriteLine(parameter);

            if (value.GetType() == typeof(MovieSearchResultItem)) {
                var item = (Movie) ((MovieSearchResultItem) value).ResultItem;

                switch ((string) parameter) {
                    case "Name":
                        return item.Title;
                    case "Directors":
                        return string.Join("\n",
                            item.DirectorIds.Select(
                                directorId => CastOrCrewRepository.Instance.FindBy(directorId).Name).ToList()
                        );
                    case "Actors":
                        return string.Join("\n",
                            item.ActorIds.Select(
                                actorIds => CastOrCrewRepository.Instance.FindBy(actorIds).Name).ToList()
                        );
                    case "RealName":
                        return $"{item.Title} ({item.ProductionYear.Value})";
                    case "Image":
                        return "./Resources/movie.png";

                    default:
                        return item;
                }
            }

            if (value.GetType() == typeof(CastOrCrewSearchResultItem)) {
                var cast = (CastOrCrew) ((CastOrCrewSearchResultItem) value).ResultItem;

                switch ((string) parameter) {
                    case "Name":
                        return cast.Name;
                    case "Directors":
                        return string.Join("\n", (from movieId in cast.DirectedMovieIds
                            from movie in MovieRepository.Instance.movies
                            where movie.Id == movieId
                            select $"{movie.Title}({movie.ProductionYear})").ToList());
                    case "Actors":
                        return string.Join("\n", (from movieId in cast.ActedMovieIds
                            from movie in MovieRepository.Instance.movies
                            where movie.Id == movieId
                            select $"{movie.Title}({movie.ProductionYear})").ToList());
                    case "RealName":
                        return $"{cast.Name} ({cast.DateOfBirth:yyy-MM-dd})";

                    default:
                        return cast;
                }
            }

            if (value.GetType() == typeof(Movie)) {
                return typeof(Movie);
            }

            if (value.GetType() == typeof(CastOrCrew)) {
                return typeof(CastOrCrew);
            }


            throw new Exception($"Cannot convert from type {value.GetType().ToString()}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

    }

}