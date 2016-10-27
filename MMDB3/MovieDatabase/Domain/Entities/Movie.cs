using MMDB.MovieDatabase.Domain.ValueObjects;
using System;
using System.Collections.Generic;

using MMDB3.MovieDatabase.Domain.Entities;

namespace MMDB.MovieDatabase.Domain
{
    public class Movie
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ProductionYear ProductionYear { get; set; }

        public HashSet<Guid> ActorIds { get; set; }

        public HashSet<Guid> DirectorIds { get; set; }

        public Movie() { }
        public Movie(string title, ProductionYear productionYear)
        {
            ActorIds = new HashSet<Guid>();
            DirectorIds = new HashSet<Guid>();
            Id = Guid.NewGuid();
            ProductionYear = productionYear;
            Title = title;
        }

        public void AddActor(CastOrCrew actor)
        {
            if (actor==null)
            {
                return;
            }

            actor.AddActedMovie(this);
            ActorIds.Add(actor.Id);          
        }
        public void AddDirector(CastOrCrew director)
        {
            if (director == null)
            {
                return;
            }

            director.AddDirectedMovie(this);
            DirectorIds.Add(director.Id);
        }

    }
}
