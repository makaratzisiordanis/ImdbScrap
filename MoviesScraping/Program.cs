
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MoviesScraping.Library;
using Movies;
using ClassLibrary1;

//   var url = "https://www.imdb.com/title/tt00137523";


namespace MoviesScraping
{
    class Program
    {
        public static Context context;
        public const bool SaveInDB = true;
        public const bool DontSaveInDB = false;
        public static MovieRepository movies = new MovieRepository();
        public static long counter = 1790809;


        static async Task Main(string[] args)
        {
            context = new Context();

          //  fetchmovies();
            fetchmovie(1790809); 
            Console.ReadLine();
        }

        public static async Task fetchmovies()
        {
            var start = 2957760;
                Parallel.For(start, start + 1000000,
                index =>
                {
                    fetchmovie(index);
                });
        }

        public static void fetchmovie(long index)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var html = httpClient.GetStringAsync(GetUrls(index));
                    var htmlResult = html.Result;
                    var movie = Functions.GetHtmlAsyncSingleMovie(htmlResult, index);
                    if (movie.MovieId != 1)
                    {
                        if (!context.Movie.Any(c => c.MovieId == movie.MovieId) && !movie.Name.Contains("Episode"))
                        {
                            context.Movie.Add(movie);
                            context.SaveChanges();
                        }
                        Console.WriteLine(movie.ToString());

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Main " + index);

            }
        }


        public static async Task GetAllMovies(int start,int get)
        {

            List<Task> tasks = new List<Task>();
                Parallel.For(start, start + get,
                   index => {
                       Console.WriteLine(index);
                       tasks.Add(GetMovie(GetUrls(index), SaveInDB));
                   });

            /*
                        for (int i = start; i < start + get; i++)
                        {
                            Console.WriteLine(i);
                            tasks.Add(GetMovie(GetUrls(i),SaveInDB)); 
                        }
 */
            await Task.WhenAny(Task.WhenAll(tasks), Task.Delay(40000));

  /*          var completedResults =tasks
              .Where(t => t.Status == TaskStatus.RanToCompletion)
              .ToList();
             
            Console.WriteLine($"Success {completedResults.Count}  out of "+ get); */  
        }


        public static string GetUrls(long number)
        { 
            var url = "https://www.imdb.com/title/tt";
            var ourUrl = url  + number+"/";
            return ourUrl;
        }
        public static async Task<Movie> GetMovie(string url,bool saveInDB)
        {
            var movie =  Functions.GetHtmlAsyncSingleMovie(url,1);
            Console.WriteLine($"\nName: {movie.Name} \nYear of publish:  {movie.Year} \nGenre:  {movie.Genre} \nRating: {movie.rating}\n");
            if (saveInDB)
            {
                if (!movie.Genre.Contains("Episode") && !movie.Name.Contains("Episode") && !movie.rating.Contains("Unknown"))
                { 
                   using (var context = new Context())
                          {
                        if (!context.Movie.Any(c => c.Url == movie.Url))
                        { 
                              context.Movie.Add(movie);
                              context.SaveChanges();
                        }
                    }
                }
            }
            return movie;

        } 




    }
}
