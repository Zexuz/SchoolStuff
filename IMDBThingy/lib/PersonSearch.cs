using System.Collections.Generic;
using System.Linq;

using IMDBThingy.ValueTypeClasses;

namespace IMDBThingy.lib {

    public class PersonSearch {

        public static IEnumerable<Person> GetMovieWorkersByName(List<Person> persons, string name) {
            return persons.Where(perosn => perosn.FirstName.ToLower().Contains(name.ToLower()));
        }

    }

}