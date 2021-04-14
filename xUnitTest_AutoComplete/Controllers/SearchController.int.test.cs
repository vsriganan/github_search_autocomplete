using AutoComplete_GitHub_SearchAPI.Controllers;
using AutoComplete_GitHub_SearchAPI.Interfaces;
using AutoComplete_GitHub_SearchAPI.Models;
using AutoComplete_GitHub_SearchAPI.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xUnitTest_AutoComplete.Controllers
{
    public class SearchControllerIntegrationTest
    {
        SearchController subject;
        ISearchService searchService;
        IGithubHttpClient gitHubClient;

        public SearchControllerIntegrationTest()
        {
            var memoryConfig = new Dictionary<string, string>
            {
                {"GitHubBaseSearchAddress", StaticVariables.searchBaseAddress},
                {"GitHubUserAgent", StaticVariables.userAgent},
                {"GitHubAuthorization", StaticVariables.githubAuthorisation}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(memoryConfig).Build();

            gitHubClient = new GithubHttpClient(configuration);
            searchService = new SearchService(gitHubClient);
            subject = new SearchController(searchService);
        }

        [Fact]
        public void SearchRepositories__gets_response_from_github_search_api()
        {
            //Arrange

            //Act
            var response = subject.SearchRepositories("autocomplete", null, null, 10, 1);
            var objResponse = (GitBaseResponse<GitRepositorySearchResponse>)response.Result.Value;

            //Assert
            response.Should().NotBeNull();
            response.Should().NotBe("");
            objResponse.total_count.Should().BeGreaterThan(0);
            objResponse.items.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void SearchCode__gets_response_from_github_search_api()
        {
            //Arrange

            //Act
            var response = subject.SearchCode("autocomplete", null, null, 10, 1);
            var objResponse = (GitBaseResponse<GitCodeSearchResponse>)response.Result.Value;

            //Assert
            response.Should().NotBeNull();
            response.Should().NotBe("");
            objResponse.total_count.Should().BeGreaterThan(0);
            objResponse.items.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void SearchCommit__gets_response_from_github_search_api()
        {
            //Arrange

            //Act
            var response = subject.SearchCommit("autocomplete", null, null, 10, 1);
            var objResponse = (GitBaseResponse<GitCommitSearchResponse>)response.Result.Value;

            //Assert
            response.Should().NotBeNull();
            response.Should().NotBe("");
            objResponse.total_count.Should().BeGreaterThan(0);
            objResponse.items.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void SearchIssue__gets_response_from_github_search_api()
        {
            //Arrange

            //Act
            var response = subject.SearchIssue("autocomplete", null, null, 10, 1);
            var objResponse = (GitBaseResponse<GitIssueSearchResponse>)response.Result.Value;

            //Assert
            response.Should().NotBeNull();
            response.Should().NotBe("");
            objResponse.total_count.Should().BeGreaterThan(0);
            objResponse.items.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void SearchTopic__gets_response_from_github_search_api()
        {
            //Arrange

            //Act
            var response = subject.SearchTopic("autocomplete", null, null, 10, 1);
            var objResponse = (GitBaseResponse<GitTopicSearchResponse>)response.Result.Value;

            //Assert
            response.Should().NotBeNull();
            response.Should().NotBe("");
            objResponse.total_count.Should().BeGreaterThan(0);
            objResponse.items.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void SearchUser__gets_response_from_github_search_api()
        {
            //Arrange

            //Act
            var response = subject.SearchUser("autocomplete", null, null, 10, 1);
            var objResponse = (GitBaseResponse<GitUserSearchResponse>)response.Result.Value;

            //Assert
            response.Should().NotBeNull();
            response.Should().NotBe("");
            objResponse.total_count.Should().BeGreaterThan(0);
            objResponse.items.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void SearchAll__gets_response_from_github_search_api()
        {
            //Arrange

            //Act
            var response = subject.SearchAllAPI("autocomplete");
            var objResponse = (AutoCompleteSearchResponse)response.Result.Value;

            //Assert
            response.Should().NotBeNull();
            response.Should().NotBe("");
            objResponse.search_term.Should().Be("autocomplete");
            objResponse.repository_search.Should().NotBeNull();
            objResponse.code_search.Should().NotBeNull();
            objResponse.commit_search.Should().NotBeNull();
            objResponse.issue_search.Should().NotBeNull();
            objResponse.topic_search.Should().NotBeNull();
            objResponse.user_search.Should().NotBeNull();
        }
    }
}
