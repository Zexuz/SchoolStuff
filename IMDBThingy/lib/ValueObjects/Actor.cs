using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDBThingy.lib.ValueObjects {

    public class Actor : Person {

        public List<Movie> DirectedMovies { get; }
        public List<Movie> ActorInMovies { get; }

        public Actor(string firstName, string lastName, DateOfBirth dateOfBirth, List<Movie> directedMovies,
            List<Movie> actorInMovies) : base(firstName, lastName, dateOfBirth) {
            DirectedMovies = directedMovies;
            ActorInMovies = actorInMovies;
        }

        public override string ToString() {
            var prependString = "";

            if (DirectedMovies.Count > 0) {
                var directedMovies = DirectedMovies.Select(movie => movie.Title + " (" + movie.Year + ")").ToList();
                prependString =
                    $", and directed the following: {string.Join(",", directedMovies)}";
            }


            var list = ActorInMovies.Select(movie => movie.Title + " (" + movie.Year + ")").ToList();
            var str = $" has stared in to following movies {string.Join(",", list)} {prependString}";
            return base.ToString() + str;
        }

    }

}