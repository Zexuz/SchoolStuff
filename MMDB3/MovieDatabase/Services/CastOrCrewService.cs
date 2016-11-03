using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

using MMDB3.MovieDatabase.Domain.Entities;

namespace MMDB.MovieDatabase.Services
{
    class CastOrCrewService
    {
        public void Add(CastOrCrew castOrCrew)
        {
            Repository.Add(castOrCrew);
        }

        public CastOrCrew FindBy(Guid id)
        {
            return Repository.FindBy(id);
        }

        public CastOrCrew FindBy(string name)
        {
            return Repository.FindBy(name);
        }

        public IEnumerable<Movie> GetActedMovies(CastOrCrew castOrCrew)
        {
            return Repository.GetActedMovies(castOrCrew);
        }

        public IEnumerable<Movie> GetDirectedMovies(CastOrCrew castOrCrew)
        {
            return Repository.GetDirectedMovies(castOrCrew);
        }

         public Dictionary<CastOrCrew, IEnumerable<Movie>> GetCoActors(CastOrCrew actor)
        {
            var coactorDictionary = new Dictionary<CastOrCrew, IEnumerable<Movie>>();
            var actedMovies = GetActedMovies(actor);
            var allAcotors = Repository.GetAllPeople();

            var coactors = actedMovies.Select(m => m.ActorIds).Aggregate((l1, l2) => 
                        new HashSet<Guid>(l1.Concat(l2)))
                        .Distinct()
                        .Except(new List<Guid> { actor.Id })
                        .Join(allAcotors, g => g, a => a.Id, (g, a) => a);

            foreach (var coactor in coactors)
            {
                var movies = actedMovies.Where(m => m.ActorIds.Contains(coactor.Id)).Distinct();
                coactorDictionary.Add(coactor, movies);
            }
            return coactorDictionary;
        }

        private CastOrCrewRepository Repository
        {
            get { return CastOrCrewRepository.Instance; }
        }
    }
}
