using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MMDB.MovieDatabase.Services {

    class CastOrCrewService {

        private CastOrCrewRepository repository;

        public CastOrCrewService() {
            repository = CastOrCrewRepository.Instance;
        }

        public void Add(CastOrCrew castOrCrew) {
            repository.Add(castOrCrew);
        }

        public CastOrCrew FindBy(Guid id) {
            return repository.FindBy(id);
        }

        public List<CastOrCrew> FindBy(string name) {
            return repository.FindBy(name);
        }

        public IEnumerable<IGrouping<HashSet<Guid>, CastOrCrew>> GetAllActingFriends(CastOrCrew actor) {
            if (!actor.IsActor) throw new Exception("Actor needs to be actor!!");

            //get acted movies
            var actedMovies = actor.ActedMovieIds.Select(movieId => MovieRepository.Instance.FindBy(movieId)).ToList();

            //get all actors in acted movies

            var actors = new List<CastOrCrew>();
            foreach (var movie in actedMovies) {
                actors.AddRange(
                    movie.ActorIds.Select(actorId => CastOrCrewRepository.Instance.FindBy(actorId))
                        .Where(a => a.IsActor && a.Id != actor.Id));
            }
            //(from movie in actedMovies from actorId in movie.ActorIds select CastOrCrewRepository.Instance.FindBy(actorId) into a where a.IsActor && a.Id != actor.Id select a).ToList();

            IEnumerable<CastOrCrew> allActors = actors;

            return allActors.GroupBy(a => a.ActedMovieIds);
        }

    }

}