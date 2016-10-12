using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Domain.ValueObjects;

using System;
using System.Collections.Generic;

using MMDB.MovieDatabase.Helper;

namespace MMDB.MovieDatabase.Repositories {

    class MovieRepository {

        private List<Movie> movies;

        private static MovieRepository _instance;

        public MovieRepository() {
            movies = new List<Movie>();
            Load();
        }

        public static MovieRepository Instance {
            get {
                if (_instance == null) {
                    _instance = new MovieRepository();
                }
                return _instance;
            }
        }

        internal IEnumerable<Movie> GetAllMovies() {
            return movies;
        }

        public void AddMovie(Movie movie) {
            movies.Add(movie);
        }

        public IEnumerable<Movie> FindMoviesByTitle(string title) {
            return movies.FindAll(x => x.Title.ToLower().Contains(title.ToLower()));
        }

        public Movie FindBy(Guid id) {
            return movies.Find(x => x.Id == id);
        }

        public void Load() {
            movies = new List<Movie>(Serialiser<Movie>.GetDataFromFile("Movies.xml"));
        }

        public void Save() {
            Serialiser<Movie>.SaveDataToFile(movies, "Movies.xml");
        }

    }

}