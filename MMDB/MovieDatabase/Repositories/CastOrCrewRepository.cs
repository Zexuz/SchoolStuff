using MMDB.MovieDatabase.Domain;
using System;
using System.Collections.Generic;

namespace MMDB.MovieDatabase.Repositories
{
    class CastOrCrewRepository
    {
        private List<CastOrCrew> people;
        private static CastOrCrewRepository _instance;

        public static CastOrCrewRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CastOrCrewRepository();
                }
                return _instance;
            }
        }

        private CastOrCrewRepository()
        {
            people = new List<CastOrCrew>();
        }

        public void Add(CastOrCrew castOrCrew)
        {
            people.Add(castOrCrew);
        }

 
        public CastOrCrew FindBy(string name)
        {
            return people.Find(x => x.Name.ToLower() == name.ToLower());
        }

        public CastOrCrew FindBy(Guid id)
        {
            return people.Find(x=>x.Id == id);
        }

        public IEnumerable<CastOrCrew> AllPeople()
        {
            return (IEnumerable<CastOrCrew>)people;
        }
    }
}
