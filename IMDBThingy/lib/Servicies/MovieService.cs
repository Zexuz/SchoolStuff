using System.Collections.Generic;

using IMDBThingy.lib.Reposotory;
using IMDBThingy.lib.ValueObjects;

namespace IMDBThingy.lib.Servicies {

    public class MovieService {

        private readonly List<Movie> _movies;

        public MovieService() {
            _movies = new List<Movie>(MovieRespositor.GetInstace().GetMovies());
        }

        public void Add(Movie movie) {
            _movies.Add(movie);
        }

        public void Remove(Movie movie) {
            _movies.Remove(movie);
        }

        public void RemoveAt(int i) {
            _movies.RemoveAt(i);
        }

        public IEnumerable<Movie> GetAllMovies() {
            return _movies;
        }

        public IEnumerable<Movie> SearchByTitle(string title) {
            return MovieSearch.GetMoviesWithTitle(_movies, title);
        }

    }

}