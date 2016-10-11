using MMDB.MovieDatabase.Domain.ValueObjects;

using System;
using System.Collections.Generic;

using MMDB.MovieDatabase.Repositories;

namespace MMDB.MovieDatabase.Domain {

    public class Movie {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public ProductionYear ProductionYear { get; set; }
        public HashSet<Guid> Actors { get; set; }
        public HashSet<Guid> Director { get; set; }


        public Movie(string title, ProductionYear productionYear) {
            Actors = new HashSet<Guid>();
            Director = new HashSet<Guid>();
            Id = Guid.NewGuid();
            ProductionYear = productionYear;
            Title = title;
        }

        public Movie() {}

        public void AddActor(Guid actorId) {
            AddCastOrCrew(actorId, true);
        }

        public void AddDirector(Guid directorId) {
            AddCastOrCrew(directorId, false);
        }

        private void AddCastOrCrew(Guid castOrCrewId, bool actor) {

            if (actor) {
                CastOrCrewRepository.Instance.FindBy(castOrCrewId).ActedMovies.Add(this);
                Actors.Add(castOrCrewId);
            }
            else {
                CastOrCrewRepository.Instance.FindBy(castOrCrewId).DirectedMovies.Add(this);
                Director.Add(castOrCrewId);
            }
        }


    }

}