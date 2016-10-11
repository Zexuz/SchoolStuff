using MMDB.MovieDatabase.Domain;
using MMDB.MovieDatabase.Repositories;
using System;

namespace MMDB.MovieDatabase.Services
{
    class CastOrCrewService
    {
        private CastOrCrewRepository repository;

        public CastOrCrewService()
        {
            repository = CastOrCrewRepository.Instance;
        }

        public void Add(CastOrCrew castOrCrew)
        {
            repository.Add(castOrCrew);
        }

        public CastOrCrew FindBy(Guid id)
        {
            return repository.FindBy(id);
        }

        public CastOrCrew FindBy(string name)
        {
            return repository.FindBy(name);
        }
    }
}
