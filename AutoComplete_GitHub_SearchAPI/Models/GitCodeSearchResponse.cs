using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_SearchAPI.Models
{
    public class GitCodeSearchResponse
    {
        public string name { get; set; }
        public string path { get; set; }
        public string html_url { get; set; }
        public decimal? score { get; set; }
        public string sha { get; set; }
        public GitRepositorySearchResponse repository { get; set; }

    }
}
