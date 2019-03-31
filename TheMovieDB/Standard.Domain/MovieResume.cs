using System;
using System.Collections.Generic;
using System.Text;

namespace Standard.Domain
{
    [Serializable]
    public class MovieResume
    {
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public string title { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public List<int> genre_ids { get; set; }
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }

        public virtual string ImageUrl => "https://image.tmdb.org/t/p/w500" + poster_path;

        public virtual string genres
        {
            get
            {
                string result = null;
                int count = 0;
                foreach (int id in genre_ids)
                {
                    if (count >= 3)
                    {
                        int more;
                        more = genre_ids.Count - count;
                        return result=result+"+"+ more;

                    }


                    switch (id)
                    {
                        case 28:
                            result = result + "Action" +"\n";
                            break;
                        case 12:
                            result = result  + "Adventure" +"\n";
                            break;
                        case 16:
                            result = result  + "Animation" +"\n";
                            break;
                        case 35:
                            result = result  + "Comedy" +"\n";
                            break;
                        case 80:
                            result = result  + "Crime" +"\n";
                            break;
                        case 99:
                            result = result  + "Documentary" +"\n";
                            break;
                        case 18:
                            result = result  + "Drama" +"\n";
                            break;
                        case 10751:
                            result = result  + "Family" +"\n";
                            break;
                        case 14:
                            result = result  + "Fantasy" +"\n";
                            break;
                        case 36:
                            result = result  + "History" +"\n";
                            break;
                        case 27:
                            result = result  + "Horror" +"\n";
                            break;
                        case 10402:
                            result = result  + "Music" +"\n";
                            break;
                        case 10749:
                            result = result  + "Romance" +"\n";
                            break;
                        case 878:
                            result = result  + "Science Fiction" +"\n";
                            break;
                        case 10770:
                            result = result  + "Tv Movie" +"\n";
                            break;
                        case 53:
                            result = result  + "Thriller" +"\n";
                            break;
                        case 10752:
                            result = result  + "War" +"\n";
                            break;
                        case 37:
                            result = result  + "Western" +"\n";
                            break;
                        default:
                            result = result  + "Others" +"\n";
                            break;

                    }

                    count++;
                }

                return result.Substring(0,result.Length-1);
            }
        }

    }
}
