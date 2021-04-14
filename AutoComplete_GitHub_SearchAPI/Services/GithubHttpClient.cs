using AutoComplete_GitHub_SearchAPI.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace AutoComplete_GitHub_SearchAPI.Services
{
    public class GithubHttpClient : IGithubHttpClient
    {
        private IConfiguration _config;

        public GithubHttpClient(IConfiguration configuration)
        {
            _config = configuration;
        }

        public virtual HttpClient GetHttpClient(GitSearchType searchtype)
        {            
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetSection("GitHubBaseSearchAddress").Value);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (searchtype == GitSearchType.commits)
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.cloak-preview")); // for commits
            }
            else if (searchtype == GitSearchType.topics)
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.mercy-preview+json")); // for topics
            }
            else
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")); // for rest
            }
            httpClient.DefaultRequestHeaders.Add("User-Agent", _config.GetSection("GitHubUserAgent").Value);
            httpClient.DefaultRequestHeaders.Add("Authorization", _config.GetSection("GitHubAuthorization").Value);

            return httpClient;
        }

        public async Task<T> GetSearchResponse<T>(GitSearchType searchType, string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            try
            {
                var httpClient = GetHttpClient(searchType);
                var uriParams = GetUriWithQueryStrings(searchType, searchTerm, sort, order, perPage, pageNumber);

                var response = await httpClient.GetAsync(uriParams);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual string GetUriWithQueryStrings(GitSearchType searchtype, string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            var expectedString = "";

            expectedString += $"{searchtype}?q={HttpUtility.UrlEncode(searchTerm)}";
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                expectedString += $"&sort={sort}&order={order}";
            }
            if (perPage != null)
            {
                expectedString += $"&per_page={perPage}";
            }
            if (pageNumber != null)
            {
                expectedString += $"&page={pageNumber}";
            }

            return expectedString;
        }
    }
}
