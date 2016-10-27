using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

using MMDB3.MovieDatabase.Domain.Entities;

namespace MMDB.MovieDatabase.Repositories
{
    class MovieRepository
    {
        internal List<Movie> movies;

        private static MovieRepository _instance;
        private string filename;
        public MovieRepository()
        {
            movies = new List<Movie>();
            var path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).Parent.FullName;
            filename = new Uri(Path.Combine(path, "Movies.xml")).LocalPath;
            Load();
        }

        private void Load()
        {
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Movie>));
                movies = (List<Movie>)serializer.Deserialize(stream);
            }
        }

        internal static MovieRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MovieRepository();
                }
                return _instance;
            }
        }

        public void AddMovie(Movie movie)
        {
            movies.Add(movie);
            Save();
        }

        internal void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Movie>));
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                serializer.Serialize(stream, movies);
            }
        }

        public Movie FindMovieByTitle(string title)
        {
            return movies.Find(x=>x.Title.ToLower()==title.ToLower());
        }
        public IEnumerable<Movie> GetAllMovies()
        {
            return movies;
        }
        public void LoadDataByCode()
        {
            CastOrCrewRepository castOrCrewRepository = CastOrCrewRepository.Instance;

            var denzelwashington = new CastOrCrew("Denzel Washington", new DateTime(1954, 12, 28));
            var robertdeniro = new CastOrCrew("Robert De Niro", new DateTime(1943, 8, 17));
            var alpacino = new CastOrCrew("Al Pacino", new DateTime(1940, 4, 25));
            var tomhanks = new CastOrCrew("Tom Hanks", new DateTime(1956, 7, 9));
            var natalieportman = new CastOrCrew("Natalie Portman", new DateTime(1981, 6, 9));
            var markruffalo = new CastOrCrew("Mark Ruffalo", new DateTime(1967, 11, 22));
            var leonardodicaprio = new CastOrCrew("Leonardo DiCaprio", new DateTime(1974, 11, 11));
            var johnnydepp = new CastOrCrew("Johnny Depp", new DateTime(1963, 6, 9));
            var edwardnorton = new CastOrCrew("Edward Norton", new DateTime(1969, 8, 18));
            var bradpitt = new CastOrCrew("Brad Pitt", new DateTime(1963, 12, 18));
            var keiraknightley = new CastOrCrew("Keira Knightley", new DateTime(1985, 3, 26));
            var joepesci = new CastOrCrew("Joe Pesci", new DateTime(1943, 2, 9));
            var morhanfreeman = new CastOrCrew("Morhan Freeman", new DateTime(1937, 6, 1));
            var keanureeves = new CastOrCrew("Keanu Reeves", new DateTime(1964, 9, 2));
            var laurencefishburne = new CastOrCrew("Laurence Fishburne", new DateTime(1961, 7, 30));
            var samuelljackson = new CastOrCrew("Samuel L Jackson", new DateTime(1948, 12, 21));
            var johntravolta = new CastOrCrew("John Travolta", new DateTime(1954, 2, 18));
            var brucewillis = new CastOrCrew("Bruce Willis", new DateTime(1955, 3, 19));
            var antoinefuqua = new CastOrCrew("Antoine Fuqua", new DateTime(1966, 1, 19));
            var ronhoward = new CastOrCrew("Ron Howard", new DateTime(1954, 3, 1));
            var martinscorsese = new CastOrCrew("Martin Scorsese", new DateTime(1942, 11, 17));
            var johncarney = new CastOrCrew("John Carney", new DateTime(1972, 1, 1));
            var davidfincher = new CastOrCrew("David Fincher", new DateTime(1962, 8, 28));
            var lanawachowski = new CastOrCrew("Lana Wachowski", new DateTime(1965, 6, 21));
            var lillywachowski = new CastOrCrew("Lilly Wachowski", new DateTime(1969, 12, 29));
            var quentintarantino = new CastOrCrew("Quentin Tarantino", new DateTime(1963, 3, 27));
            var johnmctiernan = new CastOrCrew("John McTiernan", new DateTime(1951, 1, 8));

            castOrCrewRepository.Add(denzelwashington);
            castOrCrewRepository.Add(robertdeniro);
            castOrCrewRepository.Add(alpacino);
            castOrCrewRepository.Add(tomhanks);
            castOrCrewRepository.Add(natalieportman);
            castOrCrewRepository.Add(markruffalo);
            castOrCrewRepository.Add(leonardodicaprio);
            castOrCrewRepository.Add(johnnydepp);
            castOrCrewRepository.Add(edwardnorton);
            castOrCrewRepository.Add(bradpitt);
            castOrCrewRepository.Add(keiraknightley);
            castOrCrewRepository.Add(joepesci);
            castOrCrewRepository.Add(morhanfreeman);
            castOrCrewRepository.Add(keanureeves);
            castOrCrewRepository.Add(laurencefishburne);
            castOrCrewRepository.Add(samuelljackson);
            castOrCrewRepository.Add(johntravolta);
            castOrCrewRepository.Add(brucewillis);
            castOrCrewRepository.Add(antoinefuqua);
            castOrCrewRepository.Add(ronhoward);
            castOrCrewRepository.Add(martinscorsese);
            castOrCrewRepository.Add(johncarney);
            castOrCrewRepository.Add(davidfincher);
            castOrCrewRepository.Add(lanawachowski);
            castOrCrewRepository.Add(lillywachowski);
            castOrCrewRepository.Add(quentintarantino);
            castOrCrewRepository.Add(johnmctiernan);

            var trainingday = new Movie("Training Day", new ProductionYear(2001));
            var inferno = new Movie("Inferno", new ProductionYear(2016));
            var shutterisland = new Movie("Shutter Island", new ProductionYear(2010));
            var goodfellas = new Movie("Goodfellas", new ProductionYear(1990));
            var beginagain = new Movie("Begin Again", new ProductionYear(2013));
            var fightclub = new Movie("Fight Club", new ProductionYear(1999));
            var matrix = new Movie("Matrix", new ProductionYear(1999));
            var pulpfiction = new Movie("Pulp Fiction", new ProductionYear(1994));
            var diehard = new Movie("Die Hard", new ProductionYear(1988));

            AddMovie(trainingday);
            AddMovie(inferno);
            AddMovie(shutterisland);
            AddMovie(goodfellas);
            AddMovie(beginagain);
            AddMovie(fightclub);
            AddMovie(matrix);
            AddMovie(pulpfiction);
            AddMovie(diehard);

            trainingday.AddDirector(antoinefuqua);
            trainingday.AddActor(denzelwashington);
            inferno.AddDirector(ronhoward);
            inferno.AddActor(tomhanks);
            shutterisland.AddDirector(martinscorsese);
            shutterisland.AddActor(markruffalo);
            shutterisland.AddActor(leonardodicaprio);
            goodfellas.AddDirector(martinscorsese);
            goodfellas.AddActor(robertdeniro);
            goodfellas.AddActor(joepesci);
            beginagain.AddDirector(johncarney);
            beginagain.AddActor(markruffalo);
            beginagain.AddActor(keiraknightley);
            fightclub.AddDirector(davidfincher);
            fightclub.AddActor(edwardnorton);
            fightclub.AddActor(bradpitt);
            matrix.AddDirector(lanawachowski);
            matrix.AddDirector(lillywachowski);
            matrix.AddActor(keanureeves);
            matrix.AddActor(laurencefishburne);
            pulpfiction.AddDirector(quentintarantino);
            pulpfiction.AddActor(samuelljackson);
            pulpfiction.AddActor(johntravolta);
            pulpfiction.AddActor(brucewillis);
            diehard.AddDirector(johnmctiernan);
            diehard.AddActor(brucewillis);
        }
    }
}
