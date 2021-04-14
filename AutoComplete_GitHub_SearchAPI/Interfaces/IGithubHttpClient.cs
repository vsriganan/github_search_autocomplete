using AutoComplete_GitHub_SearchAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_SearchAPI.Interfaces
{
    public interface IGithubHttpClient
    {
        HttpClient GetHttpClient(GitSearchType searchtype);
        string GetUriWithQueryStrings(GitSearchType searchtype, string searchTerm, string sort, string order, int? perPage, int? pageNumber);
        Task<T> GetSearchResponse<T>(GitSearchType searchType, string searchTerm, string sort, string order, int? perPage, int? pageNumber);
    }
}
