using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_SearchAPI.Models
{
    public class GitBaseResponse<T>
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public IEnumerable<T> items { get; set; }
        public string sort { get; set; }
        public string order { get; set; }
        public int? perPage { get; set; }
        public int? pageNumber { get; set; }
    }
}
