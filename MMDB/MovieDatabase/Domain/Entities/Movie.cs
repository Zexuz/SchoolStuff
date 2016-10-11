using MMDB.MovieDatabase.Domain.ValueObjects;

using System;
using System.Collections.Generic;

namespace MMDB.MovieDatabase.Domain {

    class Movie {

        private CastOrCrew _director;
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ProductionYear ProductionYear { get; set; }
        public HashSet<CastOrCrew> Actors { get; set; }

        public CastOrCrew Director {
            get { return _director; }
            set {
                _director = value;
                _director.DirectedMovies.Add(this);
            }
        }

        public Movie(string title, ProductionYear productionYear) {
            Actors = new HashSet<CastOrCrew>();
            Id = Guid.NewGuid();
            ProductionYear = productionYear;
            Title = title;
        }

        public void AddActor(CastOrCrew actor) {
            if (actor == null) {
                return;
            }

            actor.ActedMovies.Add(this);
            Actors.Add(actor);
        }

    }

}