using System.Data.Entity;
using Movies;

namespace ClassLibrary1
{
    public class Context:DbContext
    {
        public DbSet<Movie> Movie { get; set; }
    }
}
