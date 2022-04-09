using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Movies;

namespace MoviesScraping.Library
{
    static class Functions
    {
       
  

              
        public static  Movie GetHtmlAsyncSingleMovie(string html,long Id)
        {
             
            try
            {

           
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);


            var moviesHtml = htmlDocument.DocumentNode.Descendants("div")
              .Where(node => node.GetAttributeValue("class", "")
              .Contains("cMYixt")).ToList();

                /*          var moviesHtmlEpisodeParent = htmlDocument.DocumentNode.Descendants("div")
                              .Where(node => node.GetAttributeValue("class", "")
                              .Equals("titleParent")).ToList();*/



                var Name1 = moviesHtml[0].Descendants("h1");
                var Name2 = moviesHtml[0].Descendants("h1").ToList();
                var Name = moviesHtml[0].Descendants("h1").ToList().First().InnerText;
            var Year = moviesHtml[0].Descendants("a").ToList().First().InnerText;


                //var genreHtml = htmlDocument.DocumentNode.Descendants("div")
                //  .Where(node => node.GetAttributeValue("class", "")
                //  .StartsWith("GenresAndPlot__GenresChipList")).ToList();
                var Genre = "";
                //foreach(var gene in genreHtml[0].Descendants("a").ToList())
                //    {
                //        Genre += gene.InnerText + ",";
                //    }
            var RatingHtml = htmlDocument.DocumentNode.Descendants("div")
                      .Where(node => node.GetAttributeValue("class", "")
                      .Contains("sc-7ab21ed2-2")).ToList();



            string Rating = RatingHtml[0].Descendants("span").ToList()[0].InnerText;


            if (Name.Contains("&nbsp;"))
            {
                int index = Name.IndexOf("&nbsp;");
                Name = Name.Remove(index, 6);
            }


            var movie = new Movie(Id, Name is null? "Unknown":Name, Year is null ? "Unknown" : Year, "", Genre is null ? "Unknown" : Genre, Rating);
                return movie;

                // Console.WriteLine($"Name: {movie.Name} \nYear of publish:  {movie.Year} \nGenre:  {movie.Genre} \nRating: {movie.rating}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Function" + 1790809);

            }
            return new Movie();
        }


        private static async void GetHtmlAsync()
        {

            var url = "https://www.imdb.com/chart/top/";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);



            var moviesHtml = htmlDocument.DocumentNode.Descendants("tbody")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("lister-list")).ToList();

            var Items = moviesHtml[0].Descendants("td")
                .Where(node => node.GetAttributeValue("class", "")
                .Contains("titleColumn")).ToList();


            var moviesRepository = new MovieRepository();


            Movie movie;
            foreach (var item in Items)
            {
                movie = new Movie(1,item.Descendants("a").FirstOrDefault().InnerText, item.Descendants("span").FirstOrDefault().InnerText, url);
            }


            foreach (var item in moviesRepository.getListOfMovies())
            {
                Console.WriteLine(item.Name + item.Year);
            }

            //   var movieList = moviesHtml[0].Descendants();
            //   Console.WriteLine(moviesHtml);


        }


    }
}
