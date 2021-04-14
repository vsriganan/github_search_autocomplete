using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_SearchAPI.Models
{
    public class GitPullRequest
    {
        public string html_url { get; set; }
        public string diff_url { get; set; }
        public string patch_url { get; set; }
    }
}
