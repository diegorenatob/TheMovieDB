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
    public class MovieRepository: IMovieRepository
    {

        public async Task<MovieList> GetMovies(int page, string language)
        {
            var url = "https://api.themoviedb.org/3/movie/upcoming?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US&page=1";
           var jsonResult = await GetClient.GetData(url);
           if(!string.IsNullOrEmpty(jsonResult))
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
            var url = "https://api.themoviedb.org/3/movie/400157?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US";
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

        public async Task<MovieList> GetSimilarMovies(int id,string language)
        {
            var url = "https://api.themoviedb.org/3/movie/400157/similar?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US&page=1";
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


        public async Task<MovieList> SearchMovies(string search, int page ,string language)
        {
            var url = "https://api.themoviedb.org/3/search/movie?api_key=1f54bd990f1cdfb230adb312546d765d&language=en-US&query=matrix&page=1&include_adult=false";
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
