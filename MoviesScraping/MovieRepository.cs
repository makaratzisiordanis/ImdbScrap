using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Movies;


namespace MoviesScraping
{
    public class MovieRepository
    {


        internal List<Movie> movies;

        public MovieRepository()
        {
            movies = new List<Movie>();
        }

        public void Add(Movie movie)
        {
            this.movies.Add(movie);
        }

        public string ToString(int id)
        {
            throw new NotImplementedException();
        }
        public List<Movie> getListOfMovies()
        {
            return movies;
        }


    }
}
