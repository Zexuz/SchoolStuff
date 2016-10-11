using MMDB.MovieDatabase.Domain.ValueObjects;

using System;
using System.Collections.Generic;

using MMDB.MovieDatabase.Repositories;

namespace MMDB.MovieDatabase.Domain {

    public class Movie {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public ProductionYear ProductionYear { get; set; }
        public HashSet<Guid> ActorIds { get; set; }
        public HashSet<Guid> DirectorIds { get; set; }


        public Movie(string title, ProductionYear productionYear) {
            ActorIds = new HashSet<Guid>();
            DirectorIds = new HashSet<Guid>();
            Id = Guid.NewGuid();
            ProductionYear = productionYear;
            Title = title;
        }

        public Movie() {}

        public void AddActor(CastOrCrew actor) {
            AddCastOrCrew(actor, true);
        }

        public void AddDirector(CastOrCrew director) {
            AddCastOrCrew(director, false);
        }

        private void AddCastOrCrew(CastOrCrew castOrCrew, bool actor) {
            if (castOrCrew == null) return;

            CastOrCrewRepository.Instance.Add(castOrCrew);


            if (actor) {
                castOrCrew.ActedMovies.Add(this);
                ActorIds.Add(castOrCrew.Id);
            }
            else {
                castOrCrew.DirectedMovies.Add(this);
                DirectorIds.Add(castOrCrew.Id);
            }
        }

    }

}