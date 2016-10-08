using System;
using System.Collections.Generic;

namespace IMDBThingy.lib.ValueObjects {

    public class Actor:Person {

        public List<Movie> DirectedMovies { get; }
        public List<Movie> ActorInMovies { get; }

        public Actor(string firstName, string lastName,DateOfBirth dateOfBirth, List<Movie> directedMovies,
            List<Movie> actorInMovies) : base(firstName, lastName,dateOfBirth) {
            DirectedMovies = directedMovies;
            ActorInMovies = actorInMovies;
        }

        public override string ToString() {
            var prependString = "";
            if (DirectedMovies.Count > 0)
                prependString = $", and directed the following: {string.Concat(DirectedMovies)}";

            var str = $" has stared in to following movies {string.Concat(ActorInMovies)} {prependString}";
            return base.ToString() + str;
        }

    }

}