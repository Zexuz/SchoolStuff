using System;

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

        public Movie Find(Guid guid) {
            return repository.FindBy(guid);
        }


        public IEnumerable<Movie> FindMoviesByTitle(string title)
        {
            return repository.FindMoviesByTitle(title);
        }

        public IEnumerable<Movie> GetAllMovies() => repository.GetAllMovies();
    }
}
