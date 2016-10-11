using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using MMDB.MovieDatabase.Domain;

namespace MMDB.MovieDatabase.Helper {

    public class MovieSerialiser {

        public static IEnumerable<Movie> GetMoviesFromFile(string fileName) {
            var list = new List<Movie>();

            using (var stream = File.Open(fileName, FileMode.Open)) {
                var xmlSerializer = new XmlSerializer(list.GetType());
                list.AddRange((IEnumerable<Movie>) xmlSerializer.Deserialize(stream));
            }

            return list;
        }

        public static void SaveMoviesToFile(List<Movie> movies, string fileName) {
            var xmlSerializer = new XmlSerializer(movies.GetType());

            using (var stream = File.Open(fileName, FileMode.Create)) {
                xmlSerializer.Serialize(stream, movies);
            }
        }

    }

}