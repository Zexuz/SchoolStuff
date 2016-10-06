using System;
using System.Collections.Generic;

using IMDBThingy.Factory;
using IMDBThingy.lib.ValueObjects;

namespace IMDBThingy.lib {

    public class Menu {

        public Menu() {}

        public void AskForPersonName(string text) {}

        public Movie CreateMovieFromConsole() {
            var movie = CreateMovieShell();
            Console.WriteLine("Director name:");
            var director = PersonFactory.CreateDirector(Console.ReadLine(), new List<Movie> {movie}, new List<Movie>());

            var actors = new List<Actor>();

            do {
                Console.WriteLine("Actors name:");
                var actor = PersonFactory.CreateActor(Console.ReadLine(), new List<Movie>(), new List<Movie> {movie});
                actors.Add(actor);
                Console.WriteLine("If there is no more actors, press enter, else type 'y'");
            } while (Console.ReadLine().ToLower().Equals("y"));


            movie.Dir = director;
            movie.Actors.AddRange(actors);

            return movie;
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