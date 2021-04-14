using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_Application.Models
{
    public class GitTopicSearchResponse
    {
        public string name { get; set; }
        public string display_name { get; set; }
        public string short_description { get; set; }
        public string description { get; set; }
        public string created_by { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public bool featured { get; set; }
        public bool curated { get; set; }
        public decimal? score { get; set; }
    }
}
