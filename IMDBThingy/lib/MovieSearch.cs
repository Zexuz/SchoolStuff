using System.Collections.Generic;
using System.Linq;

using IMDBThingy.lib.ValueObjects;

namespace IMDBThingy.lib {

    public class MovieSearch {


        public static IEnumerable<Movie> GetMoviesWithTitle(List<Movie> movies, string title) {
            return movies.Where(movie => movie.Title.ToLower().Contains(title.ToLower())).ToList();
        }

    }

}