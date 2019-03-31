using System;
using System.Collections.Generic;
using System.Text;

namespace Standard.Domain
{
    [Serializable]
    public class RootObject
    {
        public List<MovieList> results { get; set; }
        public int page { get; set; }
        public int total_results { get; set; }
        public Dates dates { get; set; }
        public int total_pages { get; set; }
    }
}
