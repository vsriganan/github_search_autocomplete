using AutoComplete_GitHub_SearchAPI.Interfaces;
using AutoComplete_GitHub_SearchAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_SearchAPI.Services
{
    public class SearchService : ISearchService
    {
        IGithubHttpClient _githubClient;

        public SearchService(IGithubHttpClient gitClient)
        {
            _githubClient = gitClient;
        }
        public async Task<GitBaseResponse<GitCodeSearchResponse>> GetCodeSearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            try
            {
                return await _githubClient.GetSearchResponse<GitBaseResponse<GitCodeSearchResponse>>(GitSearchType.code, searchTerm + " org:github", sort, order, perPage, pageNumber);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public async Task<GitBaseResponse<GitCommitSearchResponse>> GetCommitSearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            try
            {
                return await _githubClient.GetSearchResponse<GitBaseResponse<GitCommitSearchResponse>>(GitSearchType.commits, searchTerm, sort, order, perPage, pageNumber);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public async Task<AutoCompleteSearchResponse> GetInitialResponse(string searchTerm)
        {
            try
            {
                if(string.IsNullOrEmpty(searchTerm))
                {
                    return null;
                }
                var response = new AutoCompleteSearchResponse
                {
                    search_term = searchTerm,
                    repository_search = GetRepositorySearchResponse(searchTerm, null, null, 1, 1).Result,
                    code_search = GetCodeSearchResponse(searchTerm, null, null, 1, 1).Result,
                    commit_search = GetCommitSearchResponse(searchTerm, null, null, 1, 1).Result,
                    issue_search = GetIssueSearchResponse(searchTerm, null, null, 1, 1).Result,
                    topic_search = GetTopicSearchResponse(searchTerm, null, null, 1, 1).Result,
                    user_search = GetUserSearchResponse(searchTerm, null, null, 1, 1).Result
                };

                return response;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public async Task<GitBaseResponse<GitIssueSearchResponse>> GetIssueSearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            try
            {
                return await _githubClient.GetSearchResponse<GitBaseResponse<GitIssueSearchResponse>>(GitSearchType.issues, searchTerm, sort, order, perPage, pageNumber);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public async Task<GitBaseResponse<GitRepositorySearchResponse>> GetRepositorySearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            try
            {
                return await _githubClient.GetSearchResponse<GitBaseResponse<GitRepositorySearchResponse>>(GitSearchType.repositories, searchTerm, sort, order, perPage, pageNumber);
            }
            catch(InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public async Task<GitBaseResponse<GitTopicSearchResponse>> GetTopicSearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            try
            {
                return await _githubClient.GetSearchResponse<GitBaseResponse<GitTopicSearchResponse>>(GitSearchType.topics, searchTerm, sort, order, perPage, pageNumber);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public async Task<GitBaseResponse<GitUserSearchResponse>> GetUserSearchResponse(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            try
            {
                return await _githubClient.GetSearchResponse<GitBaseResponse<GitUserSearchResponse>>(GitSearchType.users, searchTerm, sort, order, perPage, pageNumber);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }
    }
}
