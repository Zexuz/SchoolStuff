using System.Collections.Generic;
using System.Linq;

namespace IMDBThingy.lib.ValueObjects {

    public class Movie {

        public string Title { get; }
        public ProductionYear Year { get; }
        public Person Dir { get; set; }
        public List<Person> Actors { get; }

        public Movie(string title, int year, Person dir, List<Person> actors) {
            Title = title;
            Year = new ProductionYear(year);
            Dir = dir;
            Actors = actors;
        }

        public Movie(string title, int year) {
            Title = title;
            Year = new ProductionYear(year);
            Actors = new List<Person>();
        }

        public override string ToString() {
            var x = from actorns in Actors
                select $"{actorns.FirstName} {actorns.LastName}";

            return $"{Title} ({Year}), Directed by, {Dir.FirstName} {Dir.LastName}. Staring, {string.Join(",", x)}";
        }

    }

}