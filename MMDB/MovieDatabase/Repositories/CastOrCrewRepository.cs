using MMDB.MovieDatabase.Domain;

using System;
using System.Collections.Generic;

using MMDB.MovieDatabase.Helper;

namespace MMDB.MovieDatabase.Repositories {

    class CastOrCrewRepository {

        private readonly List<CastOrCrew> _people;
        private static CastOrCrewRepository _instance;

        public static CastOrCrewRepository Instance {
            get {
                if (_instance == null) {
                    _instance = new CastOrCrewRepository();
                }
                return _instance;
            }
        }

        private CastOrCrewRepository() {
            _people = new List<CastOrCrew>();
        }

        public void Add(CastOrCrew castOrCrew) {
            _people.Add(castOrCrew);
        }

        public void AddRange(List<CastOrCrew> castOrCrew) {
            _people.AddRange(castOrCrew);
        }


        public CastOrCrew FindBy(string name) {
            return _people.Find(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public CastOrCrew FindBy(Guid id) {
            return _people.Find(x => x.Id == id);
        }

        public IEnumerable<CastOrCrew> AllPeople() {
            return _people;
        }

        public void Save() {
            Serialiser<CastOrCrew>.SaveDataToFile(_people, "CastOrCrew.xml");
        }

        public void Load() {
            var castAndCrew = new List<CastOrCrew>(Serialiser<CastOrCrew>.GetDataFromFile("CastOrCrew.xml"));
            _people.AddRange(castAndCrew);
        }

    }

}