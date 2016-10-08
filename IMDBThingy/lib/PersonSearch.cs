using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

using IMDBThingy.lib.ValueObjects;

namespace IMDBThingy.lib {

    public class PersonSearch {

        public static IEnumerable<Person> GetMovieWorkersByName(List<Person> persons, string name) {
            return persons
                .Select(person => new {Person = person, fullName = person.FirstName + " " + person.LastName})
                .Where(prop => prop.fullName.ToLower().Contains(name.ToLower()))
                .Select(prop => prop.Person);
        }

    }

}