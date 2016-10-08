using System;
using System.Collections.Generic;

using IMDBThingy.lib.ValueObjects;

namespace IMDBThingy.lib.Factory {

    public class PersonFactory {

        public static Person CreatePersonFromConsole() {
            Console.WriteLine("Person name:");

            var name = Console.ReadLine().Split(' ');

            Console.WriteLine("What year is 'it' born?");
            var year = int.Parse(Console.ReadLine());
            Console.WriteLine("What month is 'it' born?");
            var month = int.Parse(Console.ReadLine());
            Console.WriteLine("What day is 'it' born?");
            var day = int.Parse(Console.ReadLine());


            return new Person(name[0], name[1], new DateOfBirth(year, month, day));
        }

        public static Actor CreateActor(Person p, List<Movie> dirMovies, List<Movie> actorMovies) {
            return (Actor) CratePerson(p, dirMovies, actorMovies, true);
        }

        public static Director CreateDirector(Person p, List<Movie> dirMovies, List<Movie> actorMovies) {
            return (Director) CratePerson(p, dirMovies, actorMovies, false);
        }

        public static Person CreatePerson(string fullName, string dateOfBirt, char seporator) {
            var name = fullName.Split(' ');
            return new Person(name[0], name[1],
                new DateOfBirth(int.Parse(dateOfBirt.Split(seporator)[0]), int.Parse(dateOfBirt.Split(seporator)[1]),
                    int.Parse(dateOfBirt.Split(seporator)[2])));
        }

        private static Person CratePerson(Person p, List<Movie> dirMovies, List<Movie> actorMovies, bool isActor) {
            if (isActor)
                return new Actor(p.FirstName, p.LastName, p.DateOfBirth, dirMovies, actorMovies);

            return new Director(p.FirstName, p.LastName, p.DateOfBirth, dirMovies, actorMovies);
        }

    }

}