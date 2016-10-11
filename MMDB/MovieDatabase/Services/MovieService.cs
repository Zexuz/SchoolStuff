using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Repositories;
using System.Collections.Generic;

namespace MMDB.MovieDatabase.Services
{
    class MovieService
    {
        private MovieRepository repository;

        public MovieService()
        {
            repository = MovieRepository.Instance;
        }

        public void AddMovie(Movie movie)
        {
            repository.AddMovie(movie);
        }

        public Movie FindMovieByTitle(string title)
        {
            return repository.FindMovieByTitle(title);
        }

        public IEnumerable<Movie> GetAllMovies() => repository.GetAllMovies();
    }
}
