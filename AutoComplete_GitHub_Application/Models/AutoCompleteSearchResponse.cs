using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_Application.Models
{
    public class AutoCompleteSearchResponse
    {
        public string search_term { get; set; }
        public GitBaseResponse<GitRepositorySearchResponse> repository_search { get; set; }
        public GitBaseResponse<GitCodeSearchResponse> code_search { get; set; }
        public GitBaseResponse<GitCommitSearchResponse> commit_search { get; set; }
        public GitBaseResponse<GitIssueSearchResponse> issue_search { get; set; }
        public GitBaseResponse<GitUserSearchResponse> user_search { get; set; }
        public GitBaseResponse<GitTopicSearchResponse> topic_search { get; set; }
    }
}
