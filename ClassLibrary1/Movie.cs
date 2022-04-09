namespace Movies
{
    public class Movie
    {

        public int Id { get; set; }
        public long MovieId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string rating { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public override string ToString()
        {
            return $"\nId:{this.MovieId}\nName: {this.Name} \nYear of publish:  {this.Year} \nGenre:  {this.Genre} \nRating: {this.rating}\n";
        }


        public Movie()
        {
            this.MovieId = 1;
        }
        public Movie(long MovieId, string name, string year, string url, string genre = "shit", string rating = "unknown")
        {
            this.MovieId = MovieId;
            this.Url = url;
            this.Name = name;
            this.Year = year;
            this.Genre = genre;
            this.rating = rating;
        }
            
        

    }
}
