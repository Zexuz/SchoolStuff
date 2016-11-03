using MMDB.MovieDatabase.Repositories;
using System.Collections.Generic;
using System.Linq;
using MMDB.MovieDatabase.Helper;
using MMDB.MovieDatabase.ValueObjects;

namespace MMDB.MovieDatabase.Services
{
    class FreeTextSearchService
    {
        public IEnumerable<SearchResultItem> Search(string freeText, bool ignoreCase)
        {
            IEnumerable<SearchResultItem> movieResult = MovieRepo.GetAllMovies().Where(m => m.Title.Contains(freeText, ignoreCase) ||
                                                     m.ProductionYear.Value.ToString().Contains(freeText, ignoreCase)
                                                     ).Select(m => new MovieSearchResultItem(m));

            IEnumerable<SearchResultItem> castOrCrewResult = CastOrCrewRepo.GetAllPeople().Where(p => p.Name.Contains(freeText, ignoreCase) ||
                                                                            p.DateOfBirth.Year.ToString().Contains(freeText, ignoreCase)
                                                                            ).Select(p => new CastOrCrewSearchResultItem(p));
            var result = movieResult.Concat(castOrCrewResult);
            return result;
        }
        private MovieRepository MovieRepo
        {
            get { return MovieRepository.Instance; }
        }

        private CastOrCrewRepository CastOrCrewRepo
        {
            get { return CastOrCrewRepository.Instance; }
        }
    }
}
