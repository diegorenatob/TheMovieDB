using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Standard.Domain;
using Standard.Infrastructure.Utils;
using Standard.Repositories;

namespace Standard.Infrastructure
{
    public class MovieRepository : IMovieRepository
    {

        public async Task<MovieList> GetMovies(int page, string language)
        {
            var url = LoadConfigSingleton.GetLoadConfigSingleton().URL + "movie/upcoming?api_key=" + LoadConfigSingleton.GetLoadConfigSingleton().APIKey + "&language=" + language + "&page=" + page;
            var jsonResult = await GetClient.GetData(url);
            if (!string.IsNullOrEmpty(jsonResult))
            {
                MovieList result;
                result = JsonConvert.DeserializeObject<MovieList>(jsonResult);
                return result;
            }
            else
            {
                return null;
            }


        }

        public async Task<Movie> GetMovie(int id, string language)
        {
            var url = LoadConfigSingleton.GetLoadConfigSingleton().URL + "movie/" + id + "?api_key=" + LoadConfigSingleton.GetLoadConfigSingleton().APIKey + "&language=" + language;
            var jsonResult = await GetClient.GetData(url);
            if (!string.IsNullOrEmpty(jsonResult))
            {
                Movie result;
                result = JsonConvert.DeserializeObject<Movie>(jsonResult);
                return result;
            }
            else
            {
                return null;
            }


        }

        public async Task<MovieList> GetSimilarMovies(int id, string language)
        {
            var url = LoadConfigSingleton.GetLoadConfigSingleton().URL + "movie/400157/similar?api_key=" + id + "&language=" + language + "&page=1";
            var jsonResult = await GetClient.GetData(url);
            if (!string.IsNullOrEmpty(jsonResult))
            {
                MovieList result;
                result = JsonConvert.DeserializeObject<MovieList>(jsonResult);
                return result;
            }
            else
            {
                return null;
            }


        }


        public async Task<MovieList> SearchMovies(string search, int page, string language)
        {
            var url = LoadConfigSingleton.GetLoadConfigSingleton().URL + "search/movie?api_key=" + LoadConfigSingleton.GetLoadConfigSingleton().APIKey + "&language=" + language + "&query=" + search + "&page=" + page + "&include_adult=false";
            var jsonResult = await GetClient.GetData(url);
            if (!string.IsNullOrEmpty(jsonResult))
            {
                MovieList result;
                result = JsonConvert.DeserializeObject<MovieList>(jsonResult);
                return result;
            }
            else
            {
                return null;
            }


        }

    }
}
