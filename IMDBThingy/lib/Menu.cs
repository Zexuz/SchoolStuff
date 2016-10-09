using System;
using System.Collections.Generic;

using IMDBThingy.lib.Factory;
using IMDBThingy.lib.Servicies;
using IMDBThingy.lib.ValueObjects;

namespace IMDBThingy.lib {

    public class Menu {

        private readonly PersonService _ps;

        public Menu(PersonService ps) {
            _ps = ps;
        }

        public void AskForPersonName(string text) {}

        public Movie CreateMovieFromConsole() {
            var movie = CreateMovieShell();

            var director = PersonFactory.CreateDirector(
                PersonFactory.CreatePersonFromConsole("Directors"),
                new List<Movie> {movie},
                new List<Movie>()
            );


            var actors = GetAllActorsFromConsole(movie);
            movie.Dir = director;
            movie.Actors.AddRange(actors);

            //last minute code...
            foreach (var actor in actors) {
                _ps.Add(actor);
            }

            _ps.Add(director);

            return movie;
        }

        private IEnumerable<Person> GetAllActorsFromConsole(Movie movie ) {
            var actors = new List<Actor>();


            do {
                var actor = PersonFactory.CreateActor(
                    PersonFactory.CreatePersonFromConsole(),
                    new List<Movie> (),
                    new List<Movie>{movie}
                );

                actors.Add(actor);
                Console.WriteLine("If there is no more actors, press enter, else type 'y'");
            } while (Console.ReadLine().ToLower().Equals("y"));

            return actors;
        }

        private static Movie CreateMovieShell() {
            Console.WriteLine("Title of the movie");
            var title = Console.ReadLine();

            Console.WriteLine("Year produced");
            var inputString = Console.ReadLine();
            int year;
            var boolean = int.TryParse(inputString, out year);

            while (!boolean) {
                Console.WriteLine("The input was invalid, try again or type quit to return to the menu");
                inputString = Console.ReadLine();
                boolean = int.TryParse(inputString, out year);

                if (inputString.Equals("quit")) {
                    return null;
                }
            }

            return new Movie(title, year);
        }

    }

}