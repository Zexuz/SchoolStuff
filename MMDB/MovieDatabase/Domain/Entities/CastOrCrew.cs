using System;
using System.Collections.Generic;
using System.Text;

namespace MMDB.MovieDatabase.Domain
{

    public class CastOrCrew
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsDirector => DirectedMoviesIds.Count > 0;

        public bool IsActor => ActedMovieIds.Count > 0;


        public HashSet<Guid> ActedMovieIds{ get; set; }
        public HashSet<Guid> DirectedMoviesIds { get; set; }
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
                        builder.Append(", ");
                    }
                    builder.Append("Director");
                    needsComma = true;
                }
                return builder.ToString();
            }
        }

        public CastOrCrew(string name, DateTime dateOfBirth)
        {
            ActedMovieIds = new HashSet<Guid>();
            DirectedMoviesIds = new HashSet<Guid>();
            Id = Guid.NewGuid();
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public CastOrCrew() {}

    }
}
