using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_SearchAPI.Models
{
    public class GitUserSearchResponse
    {
        public string login { get; set; }
        public int? id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string html_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }
}
