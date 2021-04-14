using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_SearchAPI.Models
{
    public class GitRepositorySearchResponse
    {
        public int? id { get; set; }
        public string node_id { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public bool @private { get; set; }
        public string html_url { get; set; }
        public string description { get; set; }
        public bool fork { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? pushed_at { get; set; }
        public string homepage { get; set; }
        public int? size { get; set; }
        public int? stargazers_count { get; set; }
        public int? watchers_count { get; set; }
        public string language { get; set; }
        public GitUserSearchResponse owner { get; set; }
    }
}
