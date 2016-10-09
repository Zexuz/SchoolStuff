using System.Collections.Generic;
using System.Linq;

namespace IMDBThingy.lib.ValueObjects {

    public class Director : Person {

        public List<Movie> DirectedMovies { get; }
        public List<Movie> ActorInMovies { get; }

        public Director(string firstName, string lastName, DateOfBirth dateOfBirth, List<Movie> directedMovies,
            List<Movie> actorInMovies) : base(firstName, lastName, dateOfBirth) {
            DirectedMovies = directedMovies;
            ActorInMovies = actorInMovies;
        }

        public override string ToString() {
            var prependString = "";

            if (ActorInMovies.Count > 0) {
                var directedMovies = ActorInMovies.Select(movie => movie.Title + " (" + movie.Year + ")").ToList();
                prependString =
                    $", and stared in the following : {string.Join(",", directedMovies)}";
            }


            var list = DirectedMovies.Select(movie => movie.Title + " (" + movie.Year + ")").ToList();
            var str = $" has directed the following movies {string.Join(",", list)} {prependString}";
            return base.ToString() + str;
        }

    }

}
