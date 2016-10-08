using System.Collections.Generic;

namespace IMDBThingy.lib.ValueObjects {

    public class Director : Person {

        public List<Movie> DirectedMovies { get; }
        public List<Movie> ActorInMovies { get; }

        public Director(string firstName, string lastName,DateOfBirth dateOfBirth, List<Movie> directedMovies,
            List<Movie> actorInMovies) : base(firstName, lastName,dateOfBirth) {
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