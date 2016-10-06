using System.Collections.Generic;

using IMDBThingy.ValueTypeClasses;

namespace IMDBThingy.lib.ValueObjects {

    public class Director : Person {

        public List<Movie> DirectedMovies { get; }
        public List<Movie> ActorInMovies { get; }

        public Director(string firstName, string lastName, List<Movie> directedMovies,
            List<Movie> actorInMovies) : base(firstName, lastName) {
            DirectedMovies = directedMovies;
            ActorInMovies = actorInMovies;
        }

        public override string ToString() {
            var prependString = "";
            if (ActorInMovies.Count > 0)
                prependString = $", and stared in the following: {string.Join(", ", ActorInMovies)}";

            return base.ToString() +
                   $" has directed the following movie(s) {string.Join(", ", DirectedMovies)} {prependString}";
        }

    }

}