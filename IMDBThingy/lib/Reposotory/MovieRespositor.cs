using System.Collections.Generic;

using IMDBThingy.lib.Factory;
using IMDBThingy.lib.ValueObjects;

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
                PersonFactory.CreatePerson("Denzel Washington", "1996-11-07", '-'), new List<Movie>(),
                new List<Movie> {trainingDay}
            );

            var sandraBullock = PersonFactory.CreateActor(
                PersonFactory.CreatePerson("Sandra Bullock", "1990-05-30", '-'), new List<Movie>(),
                new List<Movie> {gravity}
            );

            var george = PersonFactory.CreateActor(
                PersonFactory.CreatePerson("George Clooney", "1950-10-22", '-'), new List<Movie>(),
                new List<Movie> {gravity, oceanEleven}
            );

            var julia = PersonFactory.CreateActor(
                PersonFactory.CreatePerson("Julia Roberts", "1980-06-04", '-'), new List<Movie>(),
                new List<Movie> {oceanEleven}
            );

            var antoine = PersonFactory.CreateDirector(PersonFactory.CreatePerson("Antoine Fuqua", "1996-11-07", '-'),
                new List<Movie> {trainingDay}, new List<Movie>());

            var steven = PersonFactory.CreateDirector(PersonFactory.CreatePerson("Steven Soderbergh","1988-07-05",'-'), new List<Movie> {oceanEleven},
                new List<Movie>());
            var alfonso = PersonFactory.CreateDirector(PersonFactory.CreatePerson("Alfonso Curón","1950-06-24",'-'), new List<Movie> {gravity}, new List<Movie>());

            trainingDay.Dir = antoine;
            gravity.Dir = alfonso;
            oceanEleven.Dir = steven;

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