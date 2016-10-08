using System.Collections.Generic;

using IMDBThingy.lib.ValueObjects;

namespace IMDBThingy.lib.Factory {

    public class PersonFactory {

        public static Actor CreateActor(string name, List<Movie> dirMovies, List<Movie> actorMovies) {
            var nameArray = name.Split(' ');
            return new Actor(nameArray[0], nameArray[1], dirMovies, actorMovies);
        }

        public static Director CreateDirector(string name, List<Movie> dirMovies, List<Movie> actorMovies) {
            var nameArray = name.Split(' ');
            return new Director(nameArray[0], nameArray[1], dirMovies, actorMovies);
        }

    }

}