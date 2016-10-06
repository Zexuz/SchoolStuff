using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using IMDBThingy.lib;
using IMDBThingy.lib.Servicies;
using IMDBThingy.lib.ValueObjects;
using IMDBThingy.ValueTypeClasses;

namespace IMDBThingy {

    internal class Program {

        private MovieService _movieService;
        private PersonService _personService;
        private Menu _menu;

        private Dictionary<char, Action> _dic;

        public void Start() {
            _movieService = new MovieService();
            _personService = new PersonService();
            _menu = new Menu();

            _dic = new Dictionary<char, Action> {
                {'a', AddMovieFromConsole},
                {'s', SearchAndPrintMovies},
                {'d', SearchAndPrintPersons}
            };

            var movies = new List<Movie>(_movieService.GetAllMovies());
            var persons = new List<Person>(_personService.GetAllMovieWorkers());

            Loop();
        }

        private void Loop() {
            while (true) {
                Console.WriteLine("a: add movie");
                Console.WriteLine("s: search movie");
                Console.WriteLine("d: search Person");
                Console.WriteLine("q: exit program");
                var key = Console.ReadKey().KeyChar;
                Console.Clear();

                if (key == 'q') break;
                _dic[key]();
            }
            Exit();
        }

        public void SearchAndPrintMovies() {
            var searchString = Console.ReadLine();
            foreach (var movie in _movieService.SearchByTitle(searchString)) {
                Console.WriteLine(movie);
            }
        }

        public void SearchAndPrintPersons() {
            var searchString = Console.ReadLine();
            foreach (var actors in _personService.SearchByName(searchString)) {
                Console.WriteLine(actors);
            }
        }

        public void AddMovieFromConsole() {
            _movieService.Add(_menu.CreateMovieFromConsole());
        }

        public static void Exit() {
            Environment.Exit(Environment.ExitCode);
        }


        private static void Main(string[] args) {
            new Program().Start();
        }

    }

}