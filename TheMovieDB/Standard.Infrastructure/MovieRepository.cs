using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Standard.Domain;
using Standard.Repositories;

namespace Standard.Infrastructure
{
    public class MovieRepository: IMovieRepository
    {
        public async Task<List<MovieList>> GetMovies()
        {
            var client = new RestClient("https://api.themoviedb.org/3/movie/upcoming?page=1&language=en-US&api_key=1f54bd990f1cdfb230adb312546d765d");
            var request = new RestRequest(Method.GET);
            request.AddParameter("undefined", "{}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

    }
}
