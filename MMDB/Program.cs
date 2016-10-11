using System;
using System.Collections.Generic;
using MMDB.MovieDatabase.Services;
using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Domain.ValueObjects;

namespace MMDB
{
    class Program
    {
       private MovieService movieService;
        private CastOrCrewService castOrCrewService;
        private bool stopRuuning;
        private Dictionary<char, Action> commandMapper;

        static void Main(string[] args)
        {
            new Program().MainLoop();
        }

        public Program()
        {
            movieService = new MovieService();
            castOrCrewService = new CastOrCrewService();
            stopRuuning = false;
            commandMapper = new Dictionary<char, Action>();
            commandMapper.Add('1', NewMovie);
            commandMapper.Add('2', FindMovie);
            commandMapper.Add('3', FindActorOrDirector);
            commandMapper.Add('0', () => stopRuuning = true);
            commandMapper.Add('p', PrintAllMovies);
        }

        private void MainLoop()
        {
            do
            {
                PrintMenu();
                var command = GetCommand();
                ExecuteCommand(command);

            } while (!stopRuuning);
        }

        private void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine("|   1='New movie', 2='Find movie', 3='Find cast/crew', 0='Quit'   |");
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine();
        }

        private char GetCommand()
        {
            return Console.ReadKey().KeyChar;
        }

        private void ExecuteCommand(char command)
        {
            try
            {
                var method = commandMapper[command];
                method();
            }
            catch
            {
                Console.WriteLine("Unknown command");
            }
        }


        private void FindActorOrDirector()
        {
            Console.Clear();
            PrintFindActorDirectorHeader();
            string name;
            AskFor(out name, "Enter the name of the actor or direcotr you're searching for: ");
            var castOrCrew = castOrCrewService.FindBy(name);
            Console.Clear();
            PrintFindActorDirectorHeader();
            if (castOrCrew == null)
            {
                Console.WriteLine($"Cannot find {name}");
            }
            else
            {
                PrintCastOrCrew(castOrCrew);
            }
        }

        private void PrintNewMovieHeader()
        {
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine("|                       Add a new movie                           |");
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine();
        }

        private void PrintFindActorDirectorHeader()
        {
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine("|             Search for an actor or director                     |");
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine();
        }

        private void PrintFindMovieHeader()
        {
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine("|                       Search for a movie                        |");
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine();
        }

        private void FindMovie()
        {
            Console.Clear();
            PrintFindMovieHeader();

            string title;
            AskFor(out title, "Enter the title of the movie you are searching for: ");

            var movie = movieService.FindMovieByTitle(title);
            Console.Clear();
            PrintFindMovieHeader();
            if (movie == null)
            {
                Console.WriteLine($"Cannot find a movie called {title}");
            }
            else
            {
                PrintMovie(movie);
            }
        }


        private void NewMovie()
        {
            Console.Clear();
            PrintNewMovieHeader();

            string title;
            AskFor(out title, "Enter the title of the movie: ");

            ProductionYear productionYear;
            AskFor(out productionYear, "Enter the year the movie was produced: ");

            Movie movie = new Movie(title, productionYear);
            movieService.AddMovie(movie);

            string name;
            AskFor(out name, "Enter the name of the director: ");

            CastOrCrew director = castOrCrewService.FindBy(name);
            if (director == null)
            {
                director = NewCastOrCrew(name);
            }
            castOrCrewService.Add(director);
            movie.Director = director;
            bool keepAskingForNewActor = false;
            do
            {
                AskFor(out name, "Enter the name of an actor: ");
                CastOrCrew actor = castOrCrewService.FindBy(name);
                if (actor == null)
                {
                    actor = NewCastOrCrew(name);
                }
                castOrCrewService.Add(actor);
                movie.AddActor(actor);
                Console.Write("Do you want to add a new actor to the movie? (y/n)");
                char c = Console.ReadKey().KeyChar;
                keepAskingForNewActor = (char.ToLower(c) == 'y');
                Console.WriteLine();
            } while (keepAskingForNewActor);
        }

        private CastOrCrew NewCastOrCrew(string name)
        {
            DateTime dateOfBirth;
            AskFor(out dateOfBirth, "Enter the date of birth: ");
            return new CastOrCrew(name, dateOfBirth);
        }

        private void PrintMovie(Movie movie)
        {
            Console.WriteLine($"{movie.Title} ({movie.ProductionYear})");
            Console.WriteLine($"Director: {movie.Director.Name}");
            Console.WriteLine("List of actors:");
            Console.WriteLine("---------------");
            foreach (var actor in movie.Actors)
            {
                Console.WriteLine($"{actor.Name}");
            }
        }

        private void PrintCastOrCrew(CastOrCrew castOrCrew)
        {
            Console.WriteLine($"{castOrCrew.Name}");
            Console.WriteLine($"{castOrCrew.JobTitle}");
            Console.WriteLine($"Born {castOrCrew.DateOfBirth:yyyy-MM-dd}");
            if (castOrCrew.IsActor)
            {
                Console.WriteLine();
                Console.WriteLine("Actor in the ollowing movies:");
                Console.WriteLine("------------------------------");
                foreach (var movie in castOrCrew.ActedMovies)
                {
                    Console.WriteLine($"{movie.Title}");
                }
            }

            if (castOrCrew.IsDirector)
            {
                Console.WriteLine();
                Console.WriteLine("Director for the following movies:");
                Console.WriteLine("----------------------------------");
                foreach (var movie in castOrCrew.DirectedMovies)
                {
                    Console.WriteLine($"{movie.Title}");
                }
            }
        }
        private void PrintAllMovies()
        {
            Console.Clear();
            var movies = movieService.GetAllMovies();
            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.Title} ({movie.ProductionYear.Value})");
            }
        }

        private void AskFor(out string result, string askingPfrase)
        {
            Console.Write(askingPfrase);
            result = Console.ReadLine();
        }

        private void AskFor(out DateTime result, string askingPfrase)
        {
            Console.Write(askingPfrase);
            var line = Console.ReadLine();
            while (!DateTime.TryParse(line, out result))
            {
                Console.Write("Please enter a valid date: ");
                line = Console.ReadLine();
            }
        }

        private void AskFor(out ProductionYear result, string askingPfrase)
        {
            Console.Write(askingPfrase);
            var line = Console.ReadLine();
            while (!ProductionYear.TryParse(line, out result))
            {
                Console.Write("Please enter a valid production year: ");
                line = Console.ReadLine();
            }
        }
    }
}
