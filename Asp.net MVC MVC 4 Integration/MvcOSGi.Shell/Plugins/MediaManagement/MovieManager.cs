using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer;

namespace MediaManagement
{
    public class MovieManager:IMoviceManager
    {
        private static System.Collections.Concurrent.ConcurrentDictionary<string,Movie> _movies;
        static MovieManager()
        {
            _movies = new ConcurrentDictionary<string, Movie>();
            var movice = new Movie() {Name = "The Breaking Bad", Rating = 5, Id = "1"};
            _movies[movice.Id] = movice;
            movice = new Movie() { Name = "The Avatar", Rating = 5, Id = "2" };
            _movies[movice.Id] = movice;
            movice = new Movie() { Name = "The Walking Dead", Rating = 4, Id = "3" };
            _movies[movice.Id] = movice;
        }

        public List<Movie> GetMovies()
        {
            return _movies.Values.ToList();
        }


        public bool DeleteMovie(string id)
        {
            Movie temp;
           return  _movies.TryRemove(id,out temp);
        }
    }
}