﻿using System;
using System.Collections.Generic;
using System.Linq;

using MMDB.MovieDatabase.Services;
using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Domain.ValueObjects;
using MMDB.MovieDatabase.Repositories;

namespace MMDB {

    internal class Program {

        private readonly MovieService _movieService;
        private readonly CastOrCrewService _castOrCrewService;
        private bool _stopRuuning;
        private readonly Dictionary<char, Action> _commandMapper;

        private static void Main(string[] args) {
            new Program().MainLoop();
        }

        public Program() {
            CastOrCrewRepository.Instance.Load();
            MovieRepository.Instance.Load();

            _movieService = new MovieService();
            _castOrCrewService = new CastOrCrewService();
            _stopRuuning = false;
            _commandMapper = new Dictionary<char, Action> {
                {'1', NewMovie},
                {'2', FindMovies},
                {'3', FindActorOrDirector},
                {'5', GodLookUp},
                {'0', () => _stopRuuning = true},
                {'p', PrintAllMovies}
            };
        }

        private void MainLoop() {
            do {
                PrintMenu();
                var command = GetCommand();
                ExecuteCommand(command);
            } while (!_stopRuuning);
        }

        private void PrintMenu() {
            Console.WriteLine();
            Console.WriteLine(
                "+-----------------------------------------------------------------------------------------+");
            Console.WriteLine(
                "|   1='New movie', 2='Find movie', 3='Find cast/crew', 5='Search for everyting' 0='Quit'  |");
            Console.WriteLine(
                "+-----------------------------------------------------------------------------------------+");
            Console.WriteLine();
        }

        private char GetCommand() {
            return Console.ReadKey().KeyChar;
        }

        private void ExecuteCommand(char command) {
            try {
                var method = _commandMapper[command];
                method();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                Console.WriteLine("Unknown command");
            }
        }

        private void GodLookUp() {
            Console.Clear();
            Console.WriteLine("Type anything to search for ");
            var searchString = Console.ReadLine();

            var castAndCrewMatches = new List<CastOrCrew>(_castOrCrewService.FindBy(searchString));
            var movieMaches = new List<Movie>(_movieService.FindMoviesByTitle(searchString));

            Console.WriteLine("--------CAST-----------");
            foreach (var cast in castAndCrewMatches) {
                PrintCastOrCrew(cast);
            }

            Console.WriteLine("--------Movies-----------");
            PrintMovies(movieMaches);
        }

        private void GetAllActoringFriends(string actorName) {
            var str = actorName;
            Console.WriteLine();
            Console.WriteLine("Co stars");
            Console.WriteLine("-----------------------");
            var actor = _castOrCrewService.FindBy(str).First();
            foreach (var item  in _castOrCrewService.GetAllActingFriends(actor)) {
                var moviesString = item.Value.Aggregate("", (current, movie) => current + movie.Title + ", ");
                Console.WriteLine($" {item.Key.Name} :{moviesString.Substring(0, moviesString.Length - 2)}");
            }
        }

        private void FindActorOrDirector() {
            Console.Clear();
            PrintFindActorDirectorHeader();
            string name;
            AskFor(out name, "Enter the name of the actor or direcotr you're searching for: ");
            var castOrCrew = _castOrCrewService.FindBy(name).First();
            Console.Clear();
            PrintFindActorDirectorHeader();
            if (castOrCrew == null) {
                Console.WriteLine($"Cannot find {name}");
                return;
            }

            PrintCastOrCrew(castOrCrew);
            if (castOrCrew.IsActor) {
                GetAllActoringFriends(castOrCrew.Name);
            }
        }

        private void PrintNewMovieHeader() {
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine("|                       Add a new movie                           |");
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine();
        }

        private void PrintFindActorDirectorHeader() {
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine("|             Search for an actor or director                     |");
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine();
        }

        private void PrintFindMovieHeader() {
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine("|                       Search for a movie                        |");
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine();
        }

        private void FindMovies() {
            Console.Clear();
            PrintFindMovieHeader();

            string title;
            AskFor(out title, "Enter the title of the movie you are searching for: ");

            var movie = _movieService.FindMoviesByTitle(title);
            Console.Clear();
            PrintFindMovieHeader();
            if (movie == null) {
                Console.WriteLine($"Cannot find a movie called {title}");
            }
            else {
                PrintMovies(movie);
            }
        }


        private void NewMovie() {
            Console.Clear();
            PrintNewMovieHeader();

            string title;
            AskFor(out title, "Enter the title of the movie: ");

            ProductionYear productionYear;
            AskFor(out productionYear, "Enter the year the movie was produced: ");

            Movie movie = new Movie(title, productionYear);
            _movieService.AddMovie(movie);

            string name;
            bool keepAskingForNewDirector = false;
            do {
                AskFor(out name, "Enter the name of an director: ");
                CastOrCrew director = _castOrCrewService.FindBy(name).First();
                if (director == null) {
                    director = NewCastOrCrew(name);
                }
                _castOrCrewService.Add(director);
                movie.AddDirector(director);
                Console.Write("Do you want to add a new director to the movie? (y/N)");
                char c = Console.ReadKey().KeyChar;
                keepAskingForNewDirector = char.ToLower(c) == 'y';
                Console.WriteLine();
            } while (keepAskingForNewDirector);

            bool keepAskingForNewActor = false;
            do {
                AskFor(out name, "Enter the name of an actor: ");
                CastOrCrew actor = _castOrCrewService.FindBy(name).First();
                if (actor == null) {
                    actor = NewCastOrCrew(name);
                }
                _castOrCrewService.Add(actor);
                movie.AddActor(actor);
                Console.Write("Do you want to add a new actor to the movie? (y/N)");
                char c = Console.ReadKey().KeyChar;
                keepAskingForNewActor = (char.ToLower(c) == 'y');
                Console.WriteLine();
            } while (keepAskingForNewActor);

            MovieRepository.Instance.Save();
            CastOrCrewRepository.Instance.Save();
        }

        private CastOrCrew NewCastOrCrew(string name) {
            DateTime dateOfBirth;
            AskFor(out dateOfBirth, "Enter the date of birth: ");
            return new CastOrCrew(name, dateOfBirth);
        }

        private void PrintMovies(IEnumerable<Movie> movies) {
            foreach (var movie in movies) {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"{movie.Title} ({movie.ProductionYear})");
                Console.WriteLine();
                Console.WriteLine("List of Directors:");
                Console.WriteLine("---------------");
                foreach (var director in movie.DirectorIds) {
                    Console.WriteLine($"{_castOrCrewService.FindBy(director).Name}");
                }

                Console.WriteLine("---------------");
                Console.WriteLine();
                Console.WriteLine("List of actors:");
                Console.WriteLine("---------------");
                foreach (var actor in movie.ActorIds) {
                    Console.WriteLine($"{_castOrCrewService.FindBy(actor).Name}");
                }

                Console.WriteLine("---------------------------------------------");
            }
        }

        private void PrintCastOrCrew(CastOrCrew castOrCrew) {
            Console.WriteLine($"{castOrCrew.Name}");
            Console.WriteLine($"{castOrCrew.JobTitle}");
            Console.WriteLine($"Born {castOrCrew.DateOfBirth:yyyy-MM-dd}");
            if (castOrCrew.IsActor) {
                Console.WriteLine();
                Console.WriteLine("Actor in the following movies:");
                Console.WriteLine("------------------------------");
                foreach (var movie in castOrCrew.ActedMovieIds) {
                    Console.WriteLine($"{_movieService.Find(movie).Title}");
                }
            }

            if (castOrCrew.IsDirector) {
                Console.WriteLine();
                Console.WriteLine("Director for the following movies:");
                Console.WriteLine("----------------------------------");
                foreach (var movie in castOrCrew.DirectedMoviesIds) {
                    Console.WriteLine($"{_movieService.Find(movie).Title}");
                }
            }
        }

        private void PrintAllMovies() {
            Console.Clear();
            var movies = _movieService.GetAllMovies();
            foreach (var movie in movies) {
                Console.WriteLine($"{movie.Title} ({movie.ProductionYear.Value})");
            }
        }

        private void AskFor(out string result, string askingPfrase) {
            Console.Write(askingPfrase);
            result = Console.ReadLine();
        }

        private void AskFor(out DateTime result, string askingPfrase) {
            Console.Write(askingPfrase);
            var line = Console.ReadLine();
            while (!DateTime.TryParse(line, out result)) {
                Console.Write("Please enter a valid date: ");
                line = Console.ReadLine();
            }
        }

        private void AskFor(out ProductionYear result, string askingPfrase) {
            Console.Write(askingPfrase);
            var line = Console.ReadLine();
            while (!ProductionYear.TryParse(line, out result)) {
                Console.Write("Please enter a valid production year: ");
                line = Console.ReadLine();
            }
        }

    }

}