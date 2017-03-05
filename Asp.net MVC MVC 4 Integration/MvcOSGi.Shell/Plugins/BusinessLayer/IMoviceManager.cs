using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IMoviceManager
    {
        List<Movie> GetMovies();
        bool DeleteMovie(string id);
    }

    public class Movie
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
    }
}
