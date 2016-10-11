using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Domain.ValueObjects;

using System;
using System.Collections.Generic;

using MMDB.MovieDatabase.Helper;

namespace MMDB.MovieDatabase.Repositories {

    class MovieRepository {

        private List<Movie> movies;

        private static MovieRepository _instance;

        public MovieRepository() {
            movies = new List<Movie>();
            Load();
        }

        public static MovieRepository Instance {
            get {
                if (_instance == null) {
                    _instance = new MovieRepository();
                }
                return _instance;
            }
        }

        internal IEnumerable<Movie> GetAllMovies() {
            return movies;
        }

        public void AddMovie(Movie movie) {
            movies.Add(movie);
        }

        public Movie FindMovieByTitle(string title) {
            return movies.Find(x => x.Title.ToLower() == title.ToLower());
        }

        public void Load() {

            CastOrCrewRepository castOrCrewRepository = CastOrCrewRepository.Instance;

            var movies = MovieSerialiser.GetMoviesFromFile("Movies.xml");

            var georgeClooney = new CastOrCrew("George Clooney", new DateTime(1961, 5, 6));
            castOrCrewRepository.Add(georgeClooney);
            var sandraBullock = new CastOrCrew("Sandra Bullock", new DateTime(1964, 7, 26));
            castOrCrewRepository.Add(sandraBullock);
            var juliaRoberts = new CastOrCrew("Julia Roberts", new DateTime(1967, 10, 28));
            castOrCrewRepository.Add(juliaRoberts);
            var denzelWashington = new CastOrCrew("Denzel Washington", new DateTime(1954, 12, 28));
            castOrCrewRepository.Add(denzelWashington);
            var stevenSoderbergh = new CastOrCrew("Steven Soderbergh", new DateTime(1963, 1, 14));
            castOrCrewRepository.Add(stevenSoderbergh);
            var antoineFuqua = new CastOrCrew("Antoine Fuqua", new DateTime(1966, 1, 19));
            castOrCrewRepository.Add(antoineFuqua);
            var alfonsoCuaron = new CastOrCrew("Alfonso Cuarón", new DateTime(1961, 11, 28));
            castOrCrewRepository.Add(alfonsoCuaron);


            /*

       var trainingDay = new Movie("Training Day", new ProductionYear(2001));
            trainingDay.AddDirector(antoineFuqua);
            trainingDay.AddActor(denzelWashington);
            AddMovie(trainingDay);

            var oceansEleven = new Movie("Ocean's Eleven", new ProductionYear(2001));
            oceansEleven.AddDirector( stevenSoderbergh);
            oceansEleven.AddActor(juliaRoberts);
            oceansEleven.AddActor(georgeClooney);
            AddMovie(oceansEleven);

            var gravity = new Movie("Gravity", new ProductionYear(2013));
            gravity.AddDirector( alfonsoCuaron);
            gravity.AddActor(sandraBullock);
            gravity.AddActor(georgeClooney);
            AddMovie(gravity);

*/
        }

    }

}