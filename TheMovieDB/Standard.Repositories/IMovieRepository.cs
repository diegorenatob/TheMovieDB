using System.Threading.Tasks;
using Standard.Domain;

namespace Standard.Repositories
{
    public interface IMovieRepository
    {
        Task<MovieList> GetMovies(int page, string language);
        Task<Movie> GetMovie(int id, string language);
        Task<MovieList> GetSimilarMovies(int id, string language);
        Task<MovieList> SearchMovies(string search, int page, string language);
    }
}
