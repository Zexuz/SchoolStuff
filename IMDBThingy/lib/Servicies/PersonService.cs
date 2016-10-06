using System.Collections.Generic;

using IMDBThingy.lib.Reposotory;
using IMDBThingy.ValueTypeClasses;

namespace IMDBThingy.lib.Servicies {

    public class PersonService {

        private readonly List<Person> _movieWorkers;

        public PersonService() {
            _movieWorkers = new List<Person>(MovieRespositor.GetInstace().GetMovieWorkers());
        }

        public void Add(Person p) {
            _movieWorkers.Add(p);
        }

        public void Remove(Person p) {
            _movieWorkers.Remove(p);
        }

        public void RemoveAt(int i) {
            _movieWorkers.RemoveAt(i);
        }

        public IEnumerable<Person> GetAllMovieWorkers() {
            return _movieWorkers;
        }

        public IEnumerable<Person> SearchByName(string name) {
            return PersonSearch.GetMovieWorkersByName(_movieWorkers, name);
        }

    }

}