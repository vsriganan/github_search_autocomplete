using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_Application.Models
{
    public class GitCommitSearchResponse
    {
        public string sha { get; set; }
        public string node_id { get; set; }
        public string html_url { get; set; }
        public GitUserSearchResponse author { get; set; }
        public GitUserSearchResponse committer { get; set; }
        public GitRepositorySearchResponse repository { get; set; }
        public decimal? score { get; set; }
    }
}
