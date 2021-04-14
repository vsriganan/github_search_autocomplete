using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_Application.Models
{
    public class GitIssueSearchResponse
    {
        public string html_url { get; set; }
        public int? id { get; set; }
        public string node_id { get; set; }
        public int? number { get; set; }
        public string title { get; set; }
        public GitUserSearchResponse user { get; set; }
        public string state { get; set; }
        public int? comments { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? closed_at { get; set; }
        public string body { get; set; }
        public decimal? score { get; set; }
        public GitPullRequest pull_request { get; set; }
    }
}
