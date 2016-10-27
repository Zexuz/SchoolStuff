using MMDB.MovieDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MMDB3.MovieDatabase.Domain.Entities;

namespace MMDB.MovieDatabase.ValueObjects
{
    public enum SearchResultItemType
    {
        None, Movie, Actor, Director, ActorDirector
    }
    public class SearchResultItem
    {
        public SearchResultItem(object item)
        {
            ResultItem = item;
        }

        public object ResultItem { get; }
        public virtual SearchResultItemType Type { get { return SearchResultItemType.None; } }
    }

    class MovieSearchResultItem : SearchResultItem
    {
        public MovieSearchResultItem(Movie movie)
            :base(movie)
        {
            
        }

        public override SearchResultItemType Type
        {
            get
            {
                return SearchResultItemType.Movie;
            }
        }

        public override string ToString()
        {
            return $"[Movie]: {((Movie)ResultItem).Title} ({((Movie)ResultItem).ProductionYear.Value})";
        }
    }

    class CastOrCrewSearchResultItem : SearchResultItem
    {
        public CastOrCrewSearchResultItem(CastOrCrew castOrCrew)
            : base(castOrCrew)
        {
        }

        public override SearchResultItemType Type
        {
            get
            {
                var castOrCrew = (CastOrCrew)ResultItem;
                if (castOrCrew.IsActor && castOrCrew.IsDirector)
                {
                    return SearchResultItemType.ActorDirector;
                }
                else if (castOrCrew.IsDirector)
                {
                    return SearchResultItemType.Director;
                }
                else if (castOrCrew.IsActor)
                {
                    return SearchResultItemType.Actor;
                }
                else
                {
                    return SearchResultItemType.None;
                }
            }
        }

        public override string ToString()
        {
            var castOrCrew = (CastOrCrew)ResultItem;
            return $"[{castOrCrew.JobTitle}]: {castOrCrew.Name} ({castOrCrew.DateOfBirth:yyy-MM-dd})";
        }
    }
}
