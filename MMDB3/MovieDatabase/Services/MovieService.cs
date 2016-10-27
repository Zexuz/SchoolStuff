using System;
using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Repositories;
using System.Linq;
using System.Collections.Generic;

using MMDB3.MovieDatabase.Domain.Entities;

namespace MMDB.MovieDatabase.Services
{
    class MovieService
    {
        public void AddMovie(Movie movie)
        {
            Repository.AddMovie(movie);
        }
        public Movie FindMovieByTitle(string title)
        {
            return Repository.FindMovieByTitle(title);
        }
        private MovieRepository Repository
        {
            get { return MovieRepository.Instance; }
        }

        public IEnumerable<CastOrCrew> GetActors(Movie movie)
        {
            var result = movie.ActorIds.Join(CastOrCrewRepository.Instance.GetAllPeople(), g => g, a => a.Id, (g, a) => a).Distinct();
            return result;
        }

        public IEnumerable<CastOrCrew> GetDirectors(Movie movie)
        {
            var result = movie.DirectorIds.Join(CastOrCrewRepository.Instance.GetAllPeople(), g => g, a => a.Id, (g, a) => a).Distinct();
            return result;
        }
    }
}
