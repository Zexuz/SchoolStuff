using System;
using System.Collections.Generic;

using IMDBThingy.lib.ValueObjects;

namespace IMDBThingy.lib.Factory {

    public class PersonFactory {

        public static Person CreatePersonFromConsole(string actorOrDirector = "Actors") {
            Console.WriteLine($"{actorOrDirector} name:");

            var name = Console.ReadLine().Split(' ');

            Console.WriteLine("Born year? Eg:1990");
            var year = int.Parse(Console.ReadLine());
            Console.WriteLine("Born month? Eg:7");
            var month = int.Parse(Console.ReadLine());
            Console.WriteLine("Born day? Eg:30");
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