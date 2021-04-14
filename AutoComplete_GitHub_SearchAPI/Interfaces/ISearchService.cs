using AutoComplete_GitHub_SearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_SearchAPI.Interfaces
{
    public interface ISearchService
    {
        Task<AutoCompleteSearchResponse> GetInitialResponse(string searchTerm);
        Task<GitBaseResponse<GitRepositorySearchResponse>> GetRepositorySearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber);
        Task<GitBaseResponse<GitCodeSearchResponse>> GetCodeSearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber);
        Task<GitBaseResponse<GitCommitSearchResponse>> GetCommitSearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber);
        Task<GitBaseResponse<GitIssueSearchResponse>> GetIssueSearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber);
        Task<GitBaseResponse<GitTopicSearchResponse>> GetTopicSearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber);
        Task<GitBaseResponse<GitUserSearchResponse>> GetUserSearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber);
    }
}
