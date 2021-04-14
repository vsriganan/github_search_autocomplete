using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_Application.Models
{
    public class GitBaseResponse<T>
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public IEnumerable<T> items { get; set; }
    }
}
