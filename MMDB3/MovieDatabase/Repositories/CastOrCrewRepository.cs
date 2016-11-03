using MMDB.MovieDatabase.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using MMDB3.MovieDatabase.Domain.Entities;

namespace MMDB.MovieDatabase.Repositories
{
    class CastOrCrewRepository
    {
        internal List<CastOrCrew> people;
        private static CastOrCrewRepository _instance;
        private string filename;

        public CastOrCrewRepository()
        {
            people = new List<CastOrCrew>();
            var path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).Parent.FullName;
            filename = new Uri(Path.Combine(path, "CastOrCrew.xml")).LocalPath;
            Load();
        }

        public void Add(CastOrCrew castOrCrew)
        {
            people.Add(castOrCrew);
            Save();
        }

        internal void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<CastOrCrew>));
            using (Stream stream = File.Open(filename, FileMode.Create))
            {
                serializer.Serialize(stream, people);
            }
        }

        private void Load()
        {
            using (Stream stream = File.Open(filename, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<CastOrCrew>));
                people = (List<CastOrCrew>)serializer.Deserialize(stream);
            }
        }
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

        public CastOrCrew FindBy(string name)
        {
            return people.Find(x => x.Name.ToLower() == name.ToLower());
        }

        internal IEnumerable<Movie> GetActedMovies(CastOrCrew castOrCrew)
        {
            var movies = MovieRepository.Instance.movies;
            var result = movies.Join(castOrCrew.ActedMovieIds, m => m.Id, i => i, (m,i)=>m);
            return result;
        }

        internal IEnumerable<Movie> GetDirectedMovies(CastOrCrew castOrCrew)
        {
            var movies = MovieRepository.Instance.movies;
            var result = movies.Join(castOrCrew.DirectedMovieIds, m => m.Id, i => i, (m, i) => m);
            return result;
        }

        public CastOrCrew FindBy(Guid id)
        {
            return people.Find(x=>x.Id == id);
        }

        public IEnumerable<CastOrCrew> GetAllPeople()
        {
            return (IEnumerable<CastOrCrew>)people;
        }
    }
}
