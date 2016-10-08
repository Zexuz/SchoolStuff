using System.Collections.Generic;

using IMDBThingy.lib.Factory;
using IMDBThingy.lib.ValueObjects;
using IMDBThingy.ValueTypeClasses;

namespace IMDBThingy.lib.Reposotory {

    public class MovieRespositor {

        private static MovieRespositor _instace;

        private List<Movie> _movies;
        private List<Person> _movieWorkers;

        private MovieRespositor() {
            //movies
            var trainingDay = new Movie("Training day", 2001);
            var gravity = new Movie("Gravity", 2013);
            var oceanEleven = new Movie("Ocean´s eleven", 2001);


            //MoveWorkers
            var denzelWashington = PersonFactory.CreateActor(
                "Denzel Washington", new List<Movie>(), new List<Movie> {trainingDay}
            );

            var sandraBullock = PersonFactory.CreateActor(
                "Sandra Bullock", new List<Movie>(), new List<Movie> {gravity}
            );

            var george = PersonFactory.CreateActor(
                "George Clooney", new List<Movie>(), new List<Movie> {gravity, oceanEleven}
            );

            var julia = PersonFactory.CreateActor(
                "Julia Roberts", new List<Movie>(), new List<Movie> {oceanEleven}
            );

            var antoine = PersonFactory.CreateDirector("Antoine Fuqua", new List<Movie> {trainingDay}, new List<Movie>());
            var steven = PersonFactory.CreateDirector("Steven Soderbergh", new List<Movie> {oceanEleven},
                new List<Movie>());
            var alfonso = PersonFactory.CreateDirector("Alfonso Curón", new List<Movie> {gravity}, new List<Movie>());

            trainingDay.Dir = antoine;
            gravity.Dir = alfonso;
            oceanEleven.Dir= steven;

            trainingDay.Actors.Add(denzelWashington);
            gravity.Actors.Add(sandraBullock);

            oceanEleven.Actors.Add(george);
            oceanEleven.Actors.Add(julia);

            _movieWorkers = new List<Person> {denzelWashington, sandraBullock, george, julia, antoine, steven, alfonso};
            _movies = new List<Movie> {trainingDay, gravity, oceanEleven};
        }

        public static MovieRespositor GetInstace() {
            if (_instace == null) {
                _instace = new MovieRespositor();
            }

            return _instace;
        }

        public IList<Movie> GetMovies() {
            return _movies;
        }

        public IList<Person> GetMovieWorkers() {
            return _movieWorkers;
        }

    }

}