using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Standard.Domain
{
    [Serializable]
    public class MovieList
    {
        [JsonProperty("results")]
        public List<MovieResume> movies { get; set; }
        public int page { get; set; }
        public int total_results { get; set; }
        public Dates dates { get; set; }
        public int total_pages { get; set; }
    }
}
