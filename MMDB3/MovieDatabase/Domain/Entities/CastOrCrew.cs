using System;
using System.Collections.Generic;
using System.Text;

using MMDB.MovieDatabase.Domain;

namespace MMDB3.MovieDatabase.Domain.Entities
{
    public class CastOrCrew
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsDirector { get { return DirectedMovieIds.Count > 0; }  }

        public bool IsActor { get { return ActedMovieIds.Count > 0; } }

        public HashSet<Guid> ActedMovieIds { get; set; }

        public HashSet<Guid> DirectedMovieIds { get; set; }

        public string JobTitle
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                bool needsComma = false;
                if (IsActor)
                {
                    builder.Append("Actor");
                    needsComma = true;
                }
                if (IsDirector)
                {
                    if (needsComma)
                    {
                        builder.Append("/");
                    }
                    builder.Append("Director");
                    needsComma = true;
                }
                return builder.ToString();
            }
        }

        internal void AddActedMovie(Movie movie)
        {
            ActedMovieIds.Add(movie.Id);
        }

        internal void AddDirectedMovie(Movie movie)
        {
            DirectedMovieIds.Add(movie.Id);
        }
        public CastOrCrew()
        {

        }
        public CastOrCrew(string name, DateTime dateOfBirth)
        {
            ActedMovieIds = new HashSet<Guid>();
            DirectedMovieIds = new HashSet<Guid>();
            Id = Guid.NewGuid();
            Name = name;
            DateOfBirth = dateOfBirth;
        }
    }
}
