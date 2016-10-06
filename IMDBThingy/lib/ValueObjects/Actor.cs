using System;
using System.Collections.Generic;

using IMDBThingy.ValueTypeClasses;

namespace IMDBThingy.lib.ValueObjects {

    public class Actor:Person {

        public List<Movie> DirectedMovies { get; }
        public List<Movie> ActorInMovies { get; }

        public Actor(string firstName, string lastName, List<Movie> directedMovies,
            List<Movie> actorInMovies) : base(firstName, lastName) {
            DirectedMovies = directedMovies;
            ActorInMovies = actorInMovies;
        }

        public override string ToString() {
            var prependString = "";
            if (DirectedMovies.Count > 0)
                prependString = $", and directed the following: {string.Concat(DirectedMovies)}";

            Console.WriteLine(string.Concat(DirectedMovies));

            var str = $" has stared in to following movies {string.Concat(ActorInMovies)} {prependString}";
            return base.ToString() + str;
        }

    }

}